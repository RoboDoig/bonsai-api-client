using bonsai_api_client.Models.GraphModel;

namespace bonsai_api_client.Views;

public partial class GraphViewControl : ContentView, IGraphView
{
    WorkflowEditor WorkflowEditor;

    public GraphViewControl()
	{
		InitializeComponent();

		WorkflowEditor = new WorkflowEditor(ServiceProvider.Current, this);
	}

	IEnumerable<GraphNodeGrouping> IGraphView.Nodes => throw new NotImplementedException();

	IEnumerable<GraphNode> IGraphView.SelectedNodes => throw new NotImplementedException();
}