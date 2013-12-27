using GalaSoft.MvvmLight;
using MVVMWindowsPhone.Core.Portable.Bootstrapper;
using MVVMWindowsPhone.Core.Portable.DAL;
using MVVMWindowsPhone.Core.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using MVVMWindowsPhone.Core.Portable.Bootstrapping;

namespace MVVMWindowsPhone.Core.Portable.ViewModel
{
    /// <summary>
    /// The ViewModel locator.
    /// </summary>
    public class ViewModelLocator:IViewModelLocator
    {
        /// <summary>
        /// Our view models.
        /// </summary>
        Dictionary<string, ViewModelBase> viewModels;

        /// <summary>
        /// Our view models.
        /// </summary>
        public Dictionary<string, ViewModelBase> ViewModels
        {
            get { return viewModels; }
            set { viewModels = value; }
        }
        
        /// <summary>
        /// Standard constructor.
        /// </summary>
        public ViewModelLocator()
        {
            
            ViewModels = new Dictionary<string,ViewModelBase>();
                       
        }
        
        /// <summary>
        /// Set and get your ViewModels
        /// here.
        /// </summary>
        /// <param name="viewModelName">The name of the viewmodel to get or set.</param>
        /// <returns>The viewmodel selected.</returns>
        public dynamic this[string viewModelName]
        {
            get
            {
                if(ViewModels.ContainsKey(viewModelName))
                {
                    return this.ViewModels[viewModelName];
                }
                else
                {
                    return null;  
                }
            }
           
        }
        
    }

}
