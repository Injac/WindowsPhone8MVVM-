using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace MVVMWindowsPhone.Core.Portable.Bootstrapping
{
    /// <summary>
    /// The base for our Viewmodel locator.
    /// </summary>
    public interface IViewModelLocator
    {
        /// <summary>
        /// The indexer to get the 
        /// right ViewModel.
        /// </summary>
        /// <param name="viewModelName"></param>
        /// <returns></returns>
        dynamic this[string viewModelName] { get; }

        /// <summary>
        /// The dictionary to 
        /// save the ViewModels to.
        /// </summary>
        Dictionary<string, ViewModelBase> ViewModels { get; set; }
    }
}
