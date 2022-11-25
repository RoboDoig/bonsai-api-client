using bonsai_api_client.Models.GraphModel;

namespace bonsai_api_client.Views;

public partial class GraphViewControl : ContentView, IGraphView
{
    WorkflowEditor WorkflowEditor;
	public GraphViewControlCanvas GraphViewControlCanvas { get; set; }

    public GraphViewControl()
	{
		InitializeComponent();

		WorkflowEditor = new WorkflowEditor(ServiceProvider.Current, this);
		GraphViewControlCanvas = new GraphViewControlCanvas();

		BindingContext = this;
	}

	IEnumerable<GraphNodeGrouping> IGraphView.Nodes => throw new NotImplementedException();

	IEnumerable<GraphNode> IGraphView.SelectedNodes => throw new NotImplementedException();


}

public class GraphViewControlCanvas : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;
        canvas.DrawLine(10, 10, 90, 100);
    }
}