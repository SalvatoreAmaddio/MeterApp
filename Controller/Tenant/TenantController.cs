using Backend.ExtensionMethods;
using Backend.Model;
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
        public ICommand OpenTenantAddressCMD { get; }
        internal TenantController()
        {
            OpenAddressCMD = new CMD(OpenAddress);
            OpenTenantAddressCMD = new CMD(OpenTenantAddress);
            AddSubControllers(TenantAddressController);
        }

        public TenantController(Tenant tenant) : this()
        {
            GoAt(tenant);
        }

        private void OpenTenantAddress() 
        {
            new TenantAddressForm(CurrentRecord).ShowDialog();
        }

        private void OpenAddress() 
        {
            if (CurrentRecord == null) return;

            if (CurrentRecord.IsNewRecord())
                if (!PerformUpdate())
                    return;

            AbstractClause selectNotIn = new Address().Select().All().Fields("Code").Fields("City.*")
                                                      .From().InnerJoin(new PostCode()).InnerJoin(nameof(PostCode), nameof(City), "CityID")
                                                      .Where()
                                                        .OpenBracket()
                                                            .Like("LOWER(StreetNum)", "@search").OR().Like("LOWER(StreetName)", "@search").OR().Like("LOWER(OtherInfo)", "@search").OR().Like("LOWER(Code)", "@search").OR().Like("LOWER(CityName)", "@search")
                                                        .CloseBracket()
                                                        .AND()
                                                        .OpenBracket()
                                                            .In("AddressID NOT", "SELECT AddressID FROM TenantAddress WHERE TenantAddress.Active = 1")
                                                        .CloseBracket();

            Helper.OpenWindowDialog("Available Addresses", new AddressList(CurrentRecord, selectNotIn));
        }
    }
}