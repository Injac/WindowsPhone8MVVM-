using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using MVVMWindowsPhone.Core.Portable.Bootstrapping;

namespace MVVMWindowsPhone.Core.Portable.Bootstrapper
{
    /// <summary>
    /// Defines the basics for our
    /// simple bootstrapper.
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// The Ninject kernel we 
        /// need to load what needs
        /// to be injected.
        /// </summary>
        IKernel Container {get;set;}

        /// <summary>
        /// The ViewModel-Locator
        /// we need later for data-binding.
        /// </summary>
        IViewModelLocator ViewModelLocator {get;set;}

        /// <summary>
        /// The Ninject modules to load.
        /// </summary>
        IList<INinjectModule> Modules {get;set;}

        /// <summary>
        /// Configure the "parts"
        /// we want to use in
        /// our project.
        /// </summary>
        void ConfigureBootstrapper();

    }
}
