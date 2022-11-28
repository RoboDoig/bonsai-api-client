using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_api_client.Models.GraphModel
{
    public class TestClass : IGraphView
    {
        IEnumerable<GraphNodeGrouping> IGraphView.Nodes => throw new NotImplementedException();

        IEnumerable<GraphNode> IGraphView.SelectedNodes => throw new NotImplementedException();
    }

    public class TestClass2
    {
        TestClass testClass;

        void Do()
        {
            var t = (IGraphView)testClass;

        }
    }
}
