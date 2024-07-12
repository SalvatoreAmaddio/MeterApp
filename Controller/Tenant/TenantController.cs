using FrontEnd.Controller;
using FrontEnd.Utils;
using MeterApp.Model;
using MeterApp.View;
using System.Windows.Input;

namespace MeterApp.Controller
{
    public class TenantController : AbstractFormController<Tenant>
    {
        public TenantAddressController TenantAddressController { get; } = new();
        public ICommand OpenAddressCMD { get; }
        internal TenantController()
        {
            OpenAddressCMD = new CMD(OpenAddress);
            AddSubControllers(TenantAddressController);
        }

        public TenantController(Tenant tenant) : this()
        {
            GoAt(tenant);
        }

        private void OpenAddress() 
        {
            if (CurrentRecord == null) return;
            if (CurrentRecord.IsNewRecord()) 
                if (!PerformUpdate()) 
                    return;

            Helper.OpenWindowDialog("Addresses", new AddressList(CurrentRecord));
        }
    }
}