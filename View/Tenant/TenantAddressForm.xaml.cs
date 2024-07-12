using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
using MeterApp.Model;
using System.Windows;

namespace MeterApp.View
{
    public partial class TenantAddressForm : Window
    {
        public TenantAddressForm()
        {
            InitializeComponent();
        }

        public TenantAddressForm(Tenant? tenant) : this()
        {
            this.SetController(new TenantAddressControllerList(tenant));
        }
    }
}