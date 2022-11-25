using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Editor.GraphModel;

namespace bonsai_api_client.ViewModels
{
    public class MainPageViewModel
    {
        public Command StartWorkflowCommand { get; private set; }

        public MainPageViewModel()
        {
            System.Diagnostics.Debug.WriteLine(DeviceInfo.Platform);

            // Create a (currently) test workflow.
            CreateWorkflow();

            StartWorkflowCommand = new Command(obj =>
            {
                StartWorkflow();
            });
        }

        void CreateWorkflow()
        {

        }

        public void StartWorkflow()
        {
            System.Diagnostics.Debug.WriteLine("StartWorkflow");
        }
    }
}
