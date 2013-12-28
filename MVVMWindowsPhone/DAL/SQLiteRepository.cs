using MVVMWindowsPhone.Core.Portable.DAL;
using MVVMWindowsPhone.Core.Portable.Model;
using MVVMWindowsPhone.Model;
using Ninject;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMWindowsPhone.DAL
{
    /// <summary>
    /// The SQLiteRepository
    /// </summary>
    public class SQLiteRepository : ITAPRepository<SQLiteUser, SQLiteAsyncConnection>
    {

        IUnitOfWork<SQLiteAsyncConnection> driver;

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        [Inject]
        public IUnitOfWork<SQLiteAsyncConnection> Driver
        {
            get
            {
                return this.driver;
            }
            set
            {
                this.driver = value;
            }
        }

       
        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<SQLiteUser>> GetAllEntries()
        {
            var list = await Driver.Context.Table<SQLiteUser>().ToListAsync();

            if (list != null)
            {
                return list.AsQueryable<SQLiteUser>();
            }

            return null;

        }


        /// <summary>
        /// Gets the filtered entries.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Parameter cannot be null or empty.;filter</exception>
        public async Task<IQueryable<SQLiteUser>> GetFilteredEntries(System.Linq.Expressions.Expression<Func<SQLiteUser, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("Parameter cannot be null or empty.", "filter");
            }

            var list = await Driver.Context.Table<SQLiteUser>().ToListAsync();

            if (list != null)
            {
                return list.AsQueryable<SQLiteUser>().Where(filter);
            }

            return null;
        }


        /// <summary>
        /// Deletes the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Parameter cannot be null or empty.;entry</exception>
        public async Task<SQLiteUser> DeleteEntry(SQLiteUser entry)
        {

            if (entry == null)
            {
                throw new ArgumentException("Parameter cannot be null or empty.", "entry");
            }

            var deleted = await Driver.Context.DeleteAsync(entry);

            if (deleted != 0)
            {
                return entry;
            }

            return null;
        }

        /// <summary>
        /// Updates the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="updateValue">The update value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Parameter cannot be null or empty.;entry
        /// or
        /// Parameter cannot be null or empty;updateValue
        /// </exception>
        public async Task<SQLiteUser> UpdateEntry(SQLiteUser entry, SQLiteUser updateValue)
        {
            if (entry == null)
            {
                throw new ArgumentException("Parameter cannot be null or empty.", "entry");
            }

            if (updateValue == null)
            {
                throw new ArgumentException("Parameter cannot be null or empty", "updateValue");
            }

            entry.Image = updateValue.Image;
            entry.Url = updateValue.Url;
            entry.UserName = updateValue.UserName;

            int updateSuccess = await Driver.Context.UpdateAsync(entry);

            if (updateSuccess != 0)
            {
                return entry;
            }

            return null;
        }


        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Parameter cannot be null or empty.;entry</exception>
        public async Task<SQLiteUser> AddEntry(SQLiteUser entry)
        {
            if (entry == null)
            {
                throw new ArgumentException("Parameter cannot be null or empty.", "entry");
            }

            var addSuccess = await Driver.Context.InsertAsync(entry);

            if(addSuccess != 0)
            {
                return entry;
            }

            return null;
        }
    }
}
