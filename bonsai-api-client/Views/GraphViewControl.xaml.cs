using Bonsai.Expressions;
using bonsai_api_client.Models.GraphModel;

namespace bonsai_api_client.Views;

// TODO - can this be replaced by a proper ViewModel?
public partial class GraphViewControl : ContentView, IGraphView
{
    WorkflowEditor WorkflowEditor;
    public GraphViewControlCanvas GraphViewControlCanvas { get; set; }

    public GraphViewControl()
    {
        InitializeComponent();

        WorkflowEditor = new WorkflowEditor(ServiceProvider.Current, this);
        WorkflowEditor.Workflow = new ExpressionBuilderGraph();

        GraphViewControlCanvas = new GraphViewControlCanvas();

        // Add a test node
        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);
    }

    IEnumerable<GraphNodeGrouping> IGraphView.Nodes => new GraphNodeGrouping[0];

    IEnumerable<GraphNode> IGraphView.SelectedNodes => new GraphNode[0];
}

public class GraphViewControlCanvas : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;
        canvas.DrawLine(10, 10, 90, 100);
        canvas.DrawLine(20, 20, 100, 110);
    }
}