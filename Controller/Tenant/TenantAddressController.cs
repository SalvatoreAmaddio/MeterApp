using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Source;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class TenantAddressController : AbstractFormController<TenantAddress>
    {
        private bool _enabled = false;
        public bool Enabled { get => _enabled; set => UpdateProperty(ref value, ref _enabled); }
        public ReadingListController ReadingListController { get; } = new();
        internal TenantAddressController() 
        {
            AllowNewRecord = false;
            AddSubControllers(ReadingListController);
        }

        public override async void OnSubFormFilter()
        {
            SearchQry.AddParameter("tenantID", ((Tenant?)ParentRecord)?.TenantID);
            RecordSource<TenantAddress> records = await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
            RecordSource.ReplaceRange(records);
            CurrentRecord = RecordSource.FirstOrDefault();
            Enabled = (CurrentRecord == null) ? false : true;
            ReadingListController.OnSubFormFilter();
        }

        public override AbstractClause InstantiateSearchQry() =>
        new TenantAddress().Select().All()
                        .Fields("Tenant.FirstName", "Tenant.LastName", "Address.StreetNum", "Address.StreetName", "Address.OtherInfo", "Address.MeterNumber", "PostCode.*", "City.CityName")
                        .From().InnerJoin(new Tenant())
                               .InnerJoin(new Address())
                               .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
                               .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Where().EqualsTo("TenantAddress.TenantID", "@tenantID").AND().EqualsTo("TenantAddress.Active", "True").Limit();
    }
}