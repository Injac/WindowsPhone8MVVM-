using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMWindowsPhone.Core.Portable.Model;

namespace UnitTesting.Fakes
{
    public class FakeDBContext : IFakeDBContext<User>
    {
        /// <summary>
        /// Our fake table
        /// </summary>
        IEnumerable<User> fakeTable;

        /// <summary>
        /// Our fake table.
        /// </summary>
        public virtual IEnumerable<User> FakeTable
        {
            get
            {
                return fakeTable;
            }
            set
            {
                this.fakeTable = value;
            }
        }

        /// <summary>
        /// Constructor to add table data
        /// </summary>
        /// <param name="fakeTable"></param>
        public FakeDBContext(IEnumerable<User> fakeTable)
        {
            this.fakeTable = fakeTable;
        }

        /// <summary>
        /// Get all entries.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAllEntries()
        {
            return this.fakeTable.AsQueryable<User>();
        }

        /// <summary>
        /// Get filtered entries.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<User> GetFilteredEntries(System.Linq.Expressions.Expression<Func<User, bool>> filter)
        {
            return fakeTable.AsQueryable<User>().Where(filter);
        }

        /// <summary>
        /// Delete a sepecific entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public User DeleteEntry(User entry)
        {
             if(this.fakeTable.Contains<User>(entry))
             {
                 var list = this.fakeTable.ToList<User>();
                 list.Remove(entry);
                 this.fakeTable = list;
                 return entry;
             }
             else
             {
                 return null;
             }
        }

        /// <summary>
        /// Update a specific entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="updateValue"></param>
        /// <returns></returns>
        public User UpdateEntry(User entry, User updateValue)
        {
            if (this.fakeTable.Contains<User>(entry))
            {
                var list = this.fakeTable.ToList<User>();
                list[list.IndexOf(entry)] = updateValue;
                this.fakeTable = list;

                return updateValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Add a new entry.
        /// </summary>
        /// <param name="Entry"></param>
        /// <returns></returns>
        public User AddEntry(User Entry)
        {
           if(!this.fakeTable.Contains<User>(Entry))
           {
               var list = this.fakeTable.ToList<User>();
               list.Add(Entry);
               this.fakeTable = list;

               return Entry;
           }
           else
           {
               return null;
           }
        }

       
    }
}
