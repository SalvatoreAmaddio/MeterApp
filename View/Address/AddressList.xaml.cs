using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
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
    }
}