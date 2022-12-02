using Bonsai.Expressions;
using bonsai_api_client.Models.Graphics;
using bonsai_api_client.Models.GraphModel;
using System.Collections.ObjectModel;

namespace bonsai_api_client.Views;

// TODO - can we generalize the canvas drawing part of this? Have some IntereactiveCanvas ContentView? 
public partial class GraphViewControl : ContentView, IGraphView, IDrawable
{
    WorkflowEditor WorkflowEditor;
    Dictionary<ExpressionBuilder, DrawnTransform> transformMapping = new Dictionary<ExpressionBuilder, DrawnTransform>();
    IGraphView graphView;

    Dictionary<ExpressionBuilder, DrawnTransform> TransformMapping
    {
        get
        {
            var nodes = GetAllNodes();
            foreach (var node in nodes)
            {
                if (!transformMapping.ContainsKey(node.Value))
                {
                    transformMapping.Add(node.Value, new DrawnRectangle(node, new PointF(NodeMargin + (node.Layer * NodeSpacing), NodeMargin + (node.LayerIndex * NodeSpacing)), NodeSize, NodeSize, NodeDefaultColor));
                }
            }

            return transformMapping;
        }
    }

    ExpressionBuilder CurrentSelectedNode;
    ExpressionBuilder PreviousSelectedNode;

    public static readonly BindableProperty NodeSpacingProperty =
        BindableProperty.Create("NodeSpacing", typeof(float), typeof(GraphViewControl), 120f);

    public static readonly BindableProperty NodeSizeProperty =
        BindableProperty.Create("NodeSize", typeof(float), typeof(GraphViewControl), 100f);

    public static readonly BindableProperty NodeMarginProperty =
        BindableProperty.Create("NodeMargin", typeof(float), typeof(GraphViewControl), 20f);

    public static readonly BindableProperty NodeDefaultColorProperty =
        BindableProperty.Create("NodeDefaultColor", typeof(Color), typeof(GraphViewControl), Colors.Blue);

    public static readonly BindableProperty NodeSelectedColorProperty =
        BindableProperty.Create("NodeSelectedColor", typeof(Color), typeof(GraphViewControl), Colors.Red);

    public float NodeSpacing
    {
        get => (float)GetValue(NodeSpacingProperty);
        set => SetValue(NodeSpacingProperty, value);
    }

    public float NodeSize
    {
        get => (float)GetValue(NodeSizeProperty);
        set => SetValue(NodeSizeProperty, value);
    }

    public float NodeMargin
    {
        get => (float)GetValue(NodeMarginProperty);
        set => SetValue(NodeMarginProperty, value);
    }

    public Color NodeDefaultColor
    {
        get => (Color)GetValue(NodeDefaultColorProperty);
        set => SetValue(NodeDefaultColorProperty, value);
    }

    public Color NodeSelectedColor
    {
        get => (Color)GetValue(NodeSelectedColorProperty);
        set => SetValue(NodeSelectedColorProperty, value);
    }

    public GraphViewControl()
    {
        InitializeComponent();

        WorkflowEditor = new WorkflowEditor(ServiceProvider.Current, this);
        WorkflowEditor.Workflow = new ExpressionBuilderGraph();

        graphView = (IGraphView)this;

        WorkflowEditor.UpdateLayout.Subscribe(validateWorkflow => { 
            System.Diagnostics.Debug.WriteLine("Update layout");
        });

        // Add a test node
        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);
        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);
    }

    List<GraphNode> GetAllNodes()
    {
        // Check - GraphNodes are reinstantiated as new instances each time? Only ExpressionBuilder retains static reference.
        List<GraphNode> allNodes = new List<GraphNode>();
        foreach (GraphNodeGrouping group in graphView.Nodes)
        {
            foreach (GraphNode node in group)
            {
                allNodes.Add(node);
            }
        }
        return allNodes;
    }

    IEnumerable<GraphNodeGrouping> IGraphView.Nodes { 
        get {
            IEnumerable<GraphNodeGrouping> nodeGroupings = WorkflowEditor.Workflow.ConnectedComponentLayering();
            return nodeGroupings; 
        } 
    }
    IEnumerable<GraphNode> IGraphView.SelectedNodes { get { return new GraphNode[0]; } }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (var mapping in TransformMapping)
        {
            mapping.Value.Draw(canvas, dirtyRect);
        }
    }

    public void Redraw()
    {
        graphViewControl.Invalidate();
    }

    private void graphViewControl_StartInteraction(object sender, TouchEventArgs e)
    {
        var point = e.Touches[0];
        var selectedTransform = TransformMapping.Where(x => x.Value.ContainsPoint(point)).FirstOrDefault().Value;
        if (selectedTransform != null)
        {
            selectedTransform.SetColor(NodeSelectedColor);
        }

        CurrentSelectedNode = TransformMapping.Where(x => x.Value == selectedTransform).FirstOrDefault().Key;
        PreviousSelectedNode = CurrentSelectedNode;

        Redraw();
    }
}