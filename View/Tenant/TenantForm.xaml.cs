using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
using System.Windows;

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
}