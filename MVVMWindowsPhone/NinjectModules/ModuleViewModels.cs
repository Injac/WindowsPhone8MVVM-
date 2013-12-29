using MVVMWindowsPhone.Core.Portable.Bootstrapping;
using MVVMWindowsPhone.Core.Portable.DAL;
using MVVMWindowsPhone.Core.Portable.ViewModel;
using MVVMWindowsPhone.Model;
using MVVMWindowsPhone.Services;
using Ninject.Modules;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVMWindowsPhone.ViewModel;

namespace MVVMWindowsPhone.NinjectModules
{
    public class ModuleViewModels : NinjectModule
    {
        public override void Load()
        {
            var  viewModels = new Dictionary<string, dynamic>();

            var repo = Kernel.GetService(typeof(ITAPRepository<SQLiteUser, SQLiteAsyncConnection>)) as ITAPRepository<SQLiteUser, SQLiteAsyncConnection>;

            var navigationService = new NavigationService();

            var userViewModel = new UserViewModel(repo,navigationService);
            
            viewModels.Add("UserViewModel", userViewModel);
            
            this.Bind<Dictionary<string, dynamic>>().ToConstant(viewModels).InSingletonScope();

            this.Bind<IViewModelLocator>().To<ViewModelLocator>().InSingletonScope();

            
        }
    }
}
