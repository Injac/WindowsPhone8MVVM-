using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MVVMWindowsPhone.Core.Portable.DAL
{
    public interface IRepository<T,U> where T:class where U:class
    {

        /// <summary>
        /// The unit of work to use.
        /// Like SQLite, or file driver.
        /// </summary>
        IUnitOfWork<U> Driver {get; set;}

        /// <summary>
        /// Get all entries.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllEntries();

        /// <summary>
        /// Get filtered entries.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<T> GetFilteredEntries(Expression<Func<T, bool>> filter);

        /// <summary>
        /// DeleteEntry
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        T DeleteEntry(T entry);

        /// <summary>
        /// Update Entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="updateValue"></param>
        /// <returns></returns>
        T UpdateEntry(T entry, T updateValue);

        /// <summary>
        /// Add a new entry.
        /// </summary>
        /// <param name="Entry"></param>
        /// <returns></returns>
        T AddEntry(T Entry);
    }
}
