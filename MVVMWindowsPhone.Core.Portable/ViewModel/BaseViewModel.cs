using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MVVMWindowsPhone.Core.Portable.DAL;
using System.Collections.ObjectModel;
using MVVMWindowsPhone.Core.Portable.Services;
using Ninject;

namespace MVVMWindowsPhone.Core.Portable.ViewModel
{
    /// <summary>
    /// Our base-view model
    /// based on the ViewModelBase
    /// of MVVM-Light portable, with
    /// two generic type parameters
    /// to support our IRepository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class BaseViewModel<T,U>:ViewModelBase where U:class where T:class
    {

        /// <summary>
        /// The data we need for our ViewModel.
        /// </summary>
        private  ObservableCollection<T> data;

        /// <summary>
        /// Our navigation service we need.
        /// </summary>
        private readonly INavigationService navigationService;

        /// <summary>
        /// The data we need for our ViewModel.
        /// </summary>
        public ObservableCollection<T> Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// The repository we use.
        /// </summary>
        protected readonly ITAPRepository<T, U> repository;

        /// <summary>
        /// Our constructor.
        /// This one will be used
        /// to inject our repository.
        /// </summary>
        /// <param name="repo"></param>
          public BaseViewModel(ITAPRepository<T, U> repo, INavigationService navService)
        {
            this.repository = repo;
            this.navigationService = navService;
            this.Data = new ObservableCollection<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel{T, U}"/> class.
        /// </summary>
        public BaseViewModel()
        {

        }
    }
}
