using Bonsai.Expressions;
using bonsai_api_client.Models.GraphModel;
using System.Collections.Generic;

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

        WorkflowEditor.InsertGraphNode("Bonsai.Reactive.Timer, Bonsai.Core, Version=2.7.0.0, Culture=neutral, PublicKeyToken=null", Bonsai.ElementCategory.Source, CreateGraphNodeType.Successor, false, false);
    }

    IEnumerable<GraphNodeGrouping> IGraphView.Nodes { get { return WorkflowEditor.Workflow.ConnectedComponentLayering(); } }
    IEnumerable<GraphNode> IGraphView.SelectedNodes { get { return new GraphNode[0]; } }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;
        canvas.DrawLine(10, 10, 90, 100);
        canvas.DrawLine(20, 20, 100, 110);

        var t = (IGraphView)this;
        foreach (GraphNodeGrouping g in t.Nodes)
        {
            System.Diagnostics.Debug.WriteLine(g);
        }
    }
}