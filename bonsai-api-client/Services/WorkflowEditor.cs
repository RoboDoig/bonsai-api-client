using Bonsai.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_api_client.Services
{
    public class WorkflowEditor : IWorkflowEditor
    {
        private ExpressionBuilderGraph workflow;
        ExpressionBuilderGraph IWorkflowEditor.Workflow
        {
            get { return workflow; }
            set { workflow = value; }
        }

        public void AddNode()
        {
            System.Diagnostics.Debug.WriteLine("AddNode");
        }

        public void DeleteNode()
        {
            System.Diagnostics.Debug.WriteLine("DeleteNode");
        }
    }
}
