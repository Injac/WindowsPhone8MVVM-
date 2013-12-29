using Microsoft.Phone.Controls;
using MVVMWindowsPhone.Core.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMWindowsPhone.Services
{
    /// <summary>
    /// Simple navigation service.
    /// Very simple.
    /// </summary>
    public class NavigationService:INavigationService
    {
        /// <summary>
        /// Navigate to a specific page.
        /// Used for Windows phone.
        /// </summary>
        /// <param name="page">The absolute uri to the page to navigate to.</param>
        public void NavigateTo(Uri page)
        {
            PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;
            
            if (frame == null)
            {
                return;
            }

            frame.Navigate(page);

        }

        /// <summary>
        /// Used for Windows 8.
        /// DO NOT USE IN WP 8!
        /// </summary>
        /// <param name="pageToNavigateTo"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void NavigateTo(Type pageToNavigateTo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Go back to
        /// the previous page.
        /// Used for Windows Phone and Windows 8.
        /// </summary>
        public void GoBack()
        {
            PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (frame == null)
            {
                return;
            }

            if(frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}
