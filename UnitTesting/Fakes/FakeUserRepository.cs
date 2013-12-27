using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMWindowsPhone.Core.Portable.DAL;
using MVVMWindowsPhone.Core.Portable.Model;

namespace UnitTesting.Fakes
{
    public class FakeUserRepository : IRepository<User,FakeDBContext>
    {
        /// <summary>
        /// Our fake database context.
        /// </summary>
        private IUnitOfWork<FakeDBContext> context;

        /// <summary>
        /// The fake database context.
        /// </summary>
        public virtual IUnitOfWork<FakeDBContext> Driver
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

        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAllEntries()
        {
           return this.Driver.Context.GetAllEntries();
        }

        /// <summary>
        /// Gets the filtered entries.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IQueryable<User> GetFilteredEntries(System.Linq.Expressions.Expression<Func<User, bool>> filter)
        {
            return this.Driver.Context.GetFilteredEntries(filter);
        }

        /// <summary>
        /// Deletes the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public User DeleteEntry(User entry)
        {
            return this.Driver.Context.DeleteEntry(entry);
        }

        /// <summary>
        /// Updates the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="updateValue">The update value.</param>
        /// <returns></returns>
        public User UpdateEntry(User entry, User updateValue)
        {
            return this.Driver.Context.UpdateEntry(entry,updateValue);
        }

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="Entry">The entry.</param>
        /// <returns></returns>
        public User AddEntry(User Entry)
        {
            return this.Driver.Context.AddEntry(Entry);
        }
    }
}
