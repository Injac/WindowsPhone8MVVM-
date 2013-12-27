using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MVVMWindowsPhone.Core.Portable.DAL;
using Ninject;

namespace MVVMWindowsPhone.DAL
{
    public class SQLiteDriver : IUnitOfWork<SQLiteAsyncConnection>
    {
        private SQLiteAsyncConnection context;
        
        [Inject]
        public SQLiteAsyncConnection Context
        {
            get
            {
                return this.context;
            }
            set
            {
                this.context = value;
            }
        }

      
        public void SetContext(SQLiteAsyncConnection context)
        {
            if(context == null)
            {
                throw new ArgumentException("Parameter cannot be null.", "context");
            }

            this.Context = context;
        }
    }
}
