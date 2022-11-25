using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Editor.GraphModel;
using bonsai_api_client.Models.GraphModel;

namespace bonsai_api_client.ViewModels
{
    public class MainPageViewModel
    {
        public Command StartWorkflowCommand { get; private set; }

        public MainPageViewModel()
        {
            System.Diagnostics.Debug.WriteLine(DeviceInfo.Platform);

            StartWorkflowCommand = new Command(obj =>
            {
                StartWorkflow();
            });
        }

        public void StartWorkflow()
        {
            System.Diagnostics.Debug.WriteLine("StartWorkflow");
        }
    }
}
