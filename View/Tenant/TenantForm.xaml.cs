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
        public TenantForm(TenantController controller) : this() => this.SetController(controller);
    }

    public class StringAddressConcat : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
        (values.Length == 2) ? $"{Nz(values[0])}, {Nz(values[1])}" : $"{Nz(values[0])}, {Nz(values[1])} - {Nz(values[2])}";

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();

        private string Nz(object value)
        {
            if ($"{value}".Equals("{DependencyProperty.UnsetValue}")) return string.Empty;
            return $"{value}";
        }
    }
}