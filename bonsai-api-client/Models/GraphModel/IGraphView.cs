using System.Collections.Generic;

namespace bonsai_api_client.Models.GraphModel
{
    interface IGraphView
    {
        IEnumerable<GraphNodeGrouping> Nodes { get; }

        IEnumerable<GraphNode> SelectedNodes { get; }
    }
}
