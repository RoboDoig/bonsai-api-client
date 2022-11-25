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
        WorkflowEditor WorkflowEditor;

        public Command StartWorkflowCommand { get; private set; }

        public MainPageViewModel()
        {
            System.Diagnostics.Debug.WriteLine(DeviceInfo.Platform);

            var serviceProvider = 
                #if WINDOWS10_0_17763_0_OR_GREATER
			                MauiWinUIApplication.Current.Services;
                #elif ANDROID
                            MauiApplication.Current.Services;
                #elif IOS || MACCATALYST
			                MauiUIApplicationDelegate.Current.Services;
                #else
			                null;
                #endif

            WorkflowEditor = new WorkflowEditor(serviceProvider, null);

            //// Create a (currently) test workflow.
            //CreateWorkflow();

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
