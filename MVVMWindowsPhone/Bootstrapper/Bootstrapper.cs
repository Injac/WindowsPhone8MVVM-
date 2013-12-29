using MVVMWindowsPhone.Core.Portable.Bootstrapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using MVVMWindowsPhone.NinjectModules;

namespace MVVMWindowsPhone.Bootstrapper
{
    public class Bootstrapper:SimpleBootstrapper
    {
        public override void ConfigureBootstrapper()
        {
            //Initialize the modules
            this.Modules = new List<INinjectModule>();

            this.Modules.Add(new ModuleRuntime());
            this.Modules.Add(new ModuleViewModels());
            
            //Add the modules we need
            //One debug module, and one runtime module
            //for our repository
            this.Container = new StandardKernel(this.Modules.ToArray<INinjectModule>());
            
        }
    }
}
