using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_api_client.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            System.Diagnostics.Debug.WriteLine(DeviceInfo.Platform);
        }
    }
}
