using Bonsai.Expressions;
using bonsai_api_client.Models.Graphics;
using bonsai_api_client.Models.GraphModel;

namespace bonsai_api_client.Views;

// TODO - can we generalize the canvas drawing part of this? Have some IntereactiveCanvas ContentView? 
public partial class GraphViewControl : ContentView, IGraphView, IDrawable
{
    WorkflowEditor WorkflowEditor;
    Dictionary<GraphNode, DrawnTransform> TransformMapping
    {
        get
        {
            Dictionary<GraphNode, DrawnTransform> mapping = new Dictionary<GraphNode, DrawnTransform>();
            var graphView = (IGraphView)this;
            foreach (GraphNode node in graphView.Nodes.SelectMany(x => x))
            {
                mapping.Add(node, new DrawnRectangle(
                        new PointF(NodeMargin + (node.Layer * NodeSpacing), NodeMargin + (node.LayerIndex * NodeSpacing)),
                        NodeSize,
                        NodeSize,
                        Colors.Blue
                    )
                );
            }
            return mapping;
        }
    }

    public static readonly BindableProperty NodeSpacingProperty =
        BindableProperty.Create("NodeSpacing", typeof(float), typeof(GraphViewControl), 120f);

    public static readonly BindableProperty NodeSizeProperty =
        BindableProperty.Create("NodeSize", typeof(float), typeof(GraphViewControl), 100f);

    public static readonly BindableProperty NodeMarginProperty =
        BindableProperty.Create("NodeMargin", typeof(float), typeof(GraphViewControl), 20f);

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

    public GraphViewControl()
    {
        InitializeComponent();

        WorkflowEditor = new WorkflowEditor(ServiceProvider.Current, this);
        WorkflowEditor.Workflow = new ExpressionBuilderGraph();
        WorkflowEditor.UpdateLayout.Subscribe(validateWorkflow => { System.Diagnostics.Debug.WriteLine("Update layout"); });

        // Add a test node
        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);

        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);
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
}