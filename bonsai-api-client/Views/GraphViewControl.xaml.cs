using Bonsai.Expressions;
using bonsai_api_client.Models.GraphModel;
using System.Collections.Generic;

namespace bonsai_api_client.Views;

// TODO - can we generalize the canvas drawing part of this? Have some IntereactiveCanvas ContentView? 
public partial class GraphViewControl : ContentView, IGraphView, IDrawable
{
    WorkflowEditor WorkflowEditor;

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

    IEnumerable<GraphNodeGrouping> IGraphView.Nodes { get { return WorkflowEditor.Workflow.ConnectedComponentLayering(); } }
    IEnumerable<GraphNode> IGraphView.SelectedNodes { get { return new GraphNode[0]; } }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;

        var graphView = (IGraphView)this;
        foreach (GraphNodeGrouping graphNodeGrouping in graphView.Nodes)
        {
            foreach (GraphNode graphNode in graphNodeGrouping)
            {
                int layer = graphNode.Layer;
                int layerIndex = graphNode.LayerIndex;

                canvas.DrawRectangle(new Rect(NodeMargin + (layer * NodeSpacing), NodeMargin + (layerIndex * NodeSpacing), NodeSize, NodeSize));
            }
        }
    }
}