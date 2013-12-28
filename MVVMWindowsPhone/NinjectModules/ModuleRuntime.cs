using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMWindowsPhone.Core.Portable.DAL;
using MVVMWindowsPhone.Core.Portable.Model;
using MVVMWindowsPhone.DAL;
using SQLite;
using System.IO;
using Windows.Storage;
using MVVMWindowsPhone.Model;

namespace MVVMWindowsPhone.NinjectModules
{
    /// <summary>
    /// The runtime module.
    /// </summary>
    public class ModuleRuntime:NinjectModule
    {

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
           string dbPath = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "users.db"));
           
           SQLiteAsyncConnection con = new SQLiteAsyncConnection(dbPath, true);

             var exists = con.ExecuteScalarAsync<int>("select count(type) from sqlite_master where type='table' and name='SQLiteUser';");
                
              exists.Wait();

              if(!(exists.Result == 1))
              {
                  var created = con.CreateTableAsync<SQLiteUser>();
                    
                  created.Wait();
                               
              }
          
           this.Bind<SQLiteAsyncConnection>().ToConstant(con).InSingletonScope();
           
           this.Bind<IUnitOfWork<SQLiteAsyncConnection>>().To<SQLiteDriver>().InSingletonScope();

           this.Bind<ITAPRepository<SQLiteUser, SQLiteAsyncConnection>>().To<SQLiteRepository>().InSingletonScope();

        }
    }
}
