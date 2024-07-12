using Backend.Database;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using FrontEnd.FilterSource;
using FrontEnd.Source;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class TenantAddressControllerList : AbstractFormListController<TenantAddress>
    {
        private readonly Tenant? _tenant;
        public RecordSource<City> Cities { get; private set; } = new(DatabaseManager.Find<City>()!);
        public RecordSource<PostCode> PostCodes { get; private set; } = new(DatabaseManager.Find<PostCode>()!);
        public SourceOption PostCodeOptions { get; }
        public SourceOption CityOptions { get; }
        public SourceOption MovedInOptions { get; }
        public SourceOption MovedOutOptions { get; }

        internal TenantAddressControllerList()
        {
            AllowNewRecord = false;
            AfterUpdate += OnAfterUpdate;
            PostCodeOptions = new SourceOption(PostCodes, "Code");
            CityOptions = new SourceOption(Cities, "CityName");
            MovedInOptions = new PrimitiveSourceOption(this, "MovedIn");
            MovedOutOptions = new PrimitiveSourceOption(this, "MovedOut");
        }

        public TenantAddressControllerList(Tenant? tenant) : this()
        {
            _tenant = tenant;
            WindowLoaded += OnWindowLoaded;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(Search))) 
                await OnSearchPropertyRequeryAsync(sender);
        }

        private async void OnWindowLoaded(object? sender, System.Windows.RoutedEventArgs e)
        {
            RecordSource.ReplaceRange(await SearchRecordAsync());
            GoFirst();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            ReloadSearchQry();
            PostCodeOptions.Conditions<WhereClause>(SearchQry);
            CityOptions.Conditions<WhereClause>(SearchQry);
            MovedInOptions.Conditions<WhereClause>(SearchQry);
            MovedOutOptions.Conditions<WhereClause>(SearchQry);
            OnAfterUpdate(e, new(null, null, nameof(Search)));
        }

        public override async Task<IEnumerable<TenantAddress>> SearchRecordAsync()
        {
            SearchQry.AddParameter("search", Search.ToLower() + "%");
            SearchQry.AddParameter("tenantID", _tenant?.TenantID);
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(TenantAddress model) { }
        public override AbstractClause InstantiateSearchQry()
        {
            return new TenantAddress().Select().All()
                        .Fields("Tenant.FirstName", "Tenant.LastName", "Address.StreetNum", "Address.StreetName", "Address.OtherInfo", "Address.MeterNumber", "PostCode.*", "City.CityName")
                        .From().InnerJoin(new Tenant())
                               .InnerJoin(new Address())
                               .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
                               .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Where()
                            .OpenBracket()
                                .EqualsTo("TenantAddress.TenantID", "@tenantID")
                            .CloseBracket()
                            .AND()
                            .OpenBracket()
                                .Like("LOWER(StreetNum)", "@search").OR().Like("LOWER(StreetName)", "@search").OR().Like("LOWER(OtherInfo)", "@search").OR().Like("LOWER(Code)", "@search").OR().Like("LOWER(CityName)", "@search")
                            .CloseBracket()
                        .OrderBy().Field("MovedIn DESC").Field("Active DESC");
        }

    }
}