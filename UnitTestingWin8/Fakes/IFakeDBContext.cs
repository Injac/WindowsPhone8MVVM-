using MVVMWindowsPhone.Core.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Fakes
{
    public interface IFakeDBContext<T>
    {

        /// <summary>
        /// A fake table for our users.
        /// </summary>
        IEnumerable<T> FakeTable {get;set;}

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
        User DeleteEntry(T entry);

        /// <summary>
        /// Update Entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="updateValue"></param>
        /// <returns></returns>
        User UpdateEntry(T entry, T updateValue);

        /// <summary>
        /// Add a new entry.
        /// </summary>
        /// <param name="Entry"></param>
        /// <returns></returns>
        User AddEntry(T Entry);
    }
}
