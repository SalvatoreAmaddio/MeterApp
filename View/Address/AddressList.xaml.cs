using Backend.Model;
using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
using MeterApp.Model;
using System.Windows.Controls;

namespace MeterApp.View
{
    public partial class AddressList : Page
    {
        public AddressList()
        {
            InitializeComponent();
            this.SetController(new AddressListController());
        }

        public AddressList(Tenant tenant, AbstractClause selectNotIn)
        {
            InitializeComponent();
            this.SetController(new AddressListController(tenant, selectNotIn));
        }
    }
}