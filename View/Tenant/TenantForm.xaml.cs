using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MeterApp.View
{
    public partial class TenantForm : Window
    {
        public TenantForm() => InitializeComponent();

        public TenantForm(TenantController controller) : this()
        {
            this.SetController(controller);
        }
    }

    public class StringConcat : IMultiValueConverter 
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
                return $"{values[0]}, {values[1]}"; 
            else
                return $"{values[0]}, {values[1]} - {values[2]}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}