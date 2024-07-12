using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
using System.Windows.Controls;

namespace MeterApp.View
{
    public partial class TenantList : Page
    {
        public TenantList()
        {
            InitializeComponent();
            this.SetController(new TenantListController());
        }
    }
}