using MVVMWindowsPhone.Core.Portable.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MVVMWindowsPhone.Converter
{
    /// <summary>
    /// Nice solution to access static members
    /// and bind them.
    /// Found the solution here:
    /// http://shineasilverlight.wordpress.com/2010/01/01/accessing-members-in-app-as-a-xaml-resource/
    /// </summary>
    public class ViewModelConverter:IValueConverter
    {
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

           App current = (App)Application.Current;

           var locator = current.GetType().GetProperty("ModelLocator").GetValue(current, null) as ViewModelLocator;

           
           if(value == null)
           {
               return null;
           }

           if(value is string)
           {
               var viewModelName = (string) value;
               var viewModel = locator[viewModelName];

               if(viewModel == null)
               {
                   return null;
               }

               return viewModel;
           }

           return null;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay" /> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
