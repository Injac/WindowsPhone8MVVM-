using MVVMWindowsPhone.Core.Portable.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;


namespace MVVMWindowsPhone.Core.Portable.Bootstrapping
{
    /// <summary>
    /// This is a simple implementation of our
    /// bootstrapper for Ninject. This needs to 
    /// be
    /// </summary>
    public abstract class SimpleBootstrapper:IBootstrapper
    {
        
        /// <summary>
        /// The Ninject-Kernel,
        /// our Container in terms
        /// of 
        /// </summary>
        private IKernel container;

        /// <summary>
        /// The list of modules to import.
        /// </summary>
        private IList<INinjectModule> modules;

        /// <summary>
        /// 
        /// </summary>
        private IViewModelLocator viewModelLocator;

        /// <summary>
        /// The container (Ninject Kernel)
        /// used to bind the types to
        /// the interfaces.
        /// </summary>
        public IKernel Container
        {
            get
            {
                return this.container;
            }
            set
            {
                this.container = value;
            }
        }

        /// <summary>
        /// The ninject modules
        /// to be loaded by the 
        /// container (Ninject Kernel)
        /// </summary>
        public IList<INinjectModule> Modules
        {
            get
            {
                return this.modules;
            }
            set
            {
                this.modules = value;
            }
        }

        /// <summary>
        /// The ViewModel-Locator
        /// that holds the instantiated
        /// ViewModels to bind the
        /// XAML against.
        /// </summary>
        public IViewModelLocator ViewModelLocator
        {
            get
            {
                return this.viewModelLocator;
            }
            set
            {
                this.viewModelLocator = value;
            }
        }

        /// <summary>
        /// The standard constructor.
        /// </summary>
        public SimpleBootstrapper()
        {
           //Nothing here curretnly.
        }

        /// <summary>
        /// This method is defined
        /// as virtual, to enable
        /// an entry point for Ninject
        /// like stated by Owen on Twitter.
        /// </summary>
        public virtual void ConfigureBootstrapper()
        {
           //Not implemented yet.
        }

     
    }
}
