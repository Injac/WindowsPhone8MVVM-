using MVVMWindowsPhone.Core.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMWindowsPhone.Model
{
    public class SQLiteUser:User
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement] 
        public override int Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
    }
}
