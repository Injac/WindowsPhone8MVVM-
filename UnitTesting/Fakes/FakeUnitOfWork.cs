using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMWindowsPhone.Core.Portable.DAL;

namespace UnitTesting.Fakes
{
    /// <summary>
    /// Our fake unit of work.
    /// </summary>
    public class FakeUnitOfWork:IUnitOfWork<FakeDBContext>
    {
        /// <summary>
        /// The context
        /// </summary>
        private FakeDBContext context;


        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public FakeDBContext Context
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
        /// Sets the context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void SetContext(FakeDBContext context)
        {
            this.Context = context;
        }
    }
}
