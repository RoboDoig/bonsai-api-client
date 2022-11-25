using Bonsai.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_api_client.Services
{
    internal interface IWorkflowEditor
    {
        public ExpressionBuilderGraph Workflow { get; protected set; }

        public void AddNode();

        public void DeleteNode();
    }
}
