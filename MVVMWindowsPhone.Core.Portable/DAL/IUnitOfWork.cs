using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MVVMWindowsPhone.Core.Portable.DAL
{
    /// <summary>
    /// Unit of work to use as abstraction.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUnitOfWork<T> where T:class
    {
              
       /// <summary>
       /// The context to work with
       /// the specific database, file,
       /// whatever.
       /// </summary>
       T Context {get; set;}
       
        /// <summary>
        /// Set the current context.
        /// Since we cannot use constructors
        /// in interfaces.
        /// </summary>
        /// <param name="context"></param>
       void SetContext(T context);              
    }
}
