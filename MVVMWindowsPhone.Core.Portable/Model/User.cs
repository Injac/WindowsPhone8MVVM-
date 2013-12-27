using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMWindowsPhone.Core.Portable.Model
{
    /// <summary>
    /// Our model that
    /// we will use
    /// for demonstration purposes.
    /// </summary>
    public class User : MVVMWindowsPhone.Core.Portable.Model.IUser
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Url to the homepage.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An image.
        /// </summary>
        public string Image {get;set;}
               
      
    }
}
