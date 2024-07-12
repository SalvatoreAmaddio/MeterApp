using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Source;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class TenantAddressController : AbstractFormController<TenantAddress>
    {
        internal TenantAddressController() 
        { 
            AllowNewRecord = false;
        }
        public override async void OnSubFormFilter()
        {
            SearchQry.AddParameter("tenantID", ((Tenant?)ParentRecord)?.TenantID);
            RecordSource<TenantAddress> records = await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
            RecordSource.ReplaceRange(records);
            CurrentRecord = RecordSource.FirstOrDefault();
            ReadOnly = (CurrentRecord == null) ? false : true;
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