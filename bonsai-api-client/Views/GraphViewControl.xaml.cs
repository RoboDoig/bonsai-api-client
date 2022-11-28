using Bonsai.Expressions;
using bonsai_api_client.Models.GraphModel;

namespace bonsai_api_client.Views;

// TODO - can this be replaced by a proper ViewModel?
public partial class GraphViewControl : ContentView, IGraphView, IDrawable
{
    WorkflowEditor WorkflowEditor;

    public GraphViewControl()
    {
        InitializeComponent();

        WorkflowEditor = new WorkflowEditor(ServiceProvider.Current, this);
        WorkflowEditor.Workflow = new ExpressionBuilderGraph();
        WorkflowEditor.UpdateLayout.Subscribe(validateWorkflow => { System.Diagnostics.Debug.WriteLine("Update layout"); });

        // Add a test node
        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);
    }

    IEnumerable<GraphNodeGrouping> IGraphView.Nodes => WorkflowEditor.Workflow.ConnectedComponentLayering();

    IEnumerable<GraphNode> IGraphView.SelectedNodes => new GraphNode[0];

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;
        canvas.DrawLine(10, 10, 90, 100);
        canvas.DrawLine(20, 20, 100, 110);
    }
}