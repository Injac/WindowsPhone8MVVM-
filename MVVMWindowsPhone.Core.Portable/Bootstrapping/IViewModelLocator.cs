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
        Dictionary<string, dynamic> ViewModels { get; set; }

        /// <summary>
        /// Casts the specified view model to the desired type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns></returns>
        T Cast<T>(string viewModelName);
          
    }
}
