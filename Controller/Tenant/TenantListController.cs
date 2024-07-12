using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;
using MeterApp.View;

namespace MeterApp.Controller
{
    public class TenantListController : AbstractFormListController<Tenant>
    {
        public TenantListController() 
        {
            AfterUpdate += OnAfterUpdate;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (!e.Is(nameof(Search))) return;
            await OnSearchPropertyRequeryAsync(sender);
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
        }

        public async override Task<IEnumerable<Tenant>> SearchRecordAsync()
        {
            SearchQry.AddParameter("name", Search.ToLower() + "%");
            SearchQry.AddParameter("name", Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Tenant model)
        {
            TenantForm tenantForm = new(new TenantController(model));
            tenantForm.ShowDialog();
        }

        public override AbstractClause InstantiateSearchQry() =>
        new Tenant().Select().All().From().Where().Like("LOWER(FirstName)", "@name").OR().Like("LOWER(LastName)", "@name");
    }
}