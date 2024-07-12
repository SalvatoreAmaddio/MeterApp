using Backend.Database;
using Backend.Enums;
using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Dialogs;
using FrontEnd.Events;
using FrontEnd.FilterSource;
using FrontEnd.ExtensionMethods;
using FrontEnd.Source;
using FrontEnd.Utils;
using MeterApp.Model;
using System.Windows;
using System.Windows.Input;

namespace MeterApp.Controller
{
    public class AddressListController : AbstractFormListController<Address>
    {
        private Visibility _bindTenantVisibility = Visibility.Hidden;
        private readonly Tenant? _tenant;
        private readonly AbstractClause? _selectNotIn;
        public RecordSource<City> Cities { get; private set; } = new(DatabaseManager.Find<City>()!);
        public RecordSource<PostCode> PostCodes { get; private set; } = new(DatabaseManager.Find<PostCode>()!);
        public SourceOption StreetNumOptions { get; }
        public SourceOption PostCodeOptions { get; }
        public SourceOption CityOptions { get; }
        public ICommand BindTenantCMD { get; }
        public Visibility BindTenantVisibility { get => _bindTenantVisibility; set => UpdateProperty(ref value, ref _bindTenantVisibility); }
        internal AddressListController()
        {
            OpenWindowOnNew = false;
            BindTenantCMD = new CMD<Address>(BindTenant);
            StreetNumOptions = new PrimitiveSourceOption(this, "StreetNum");
            PostCodeOptions = new SourceOption(PostCodes, "Code");
            CityOptions = new SourceOption(Cities, "CityName");
            AfterUpdate += OnAfterUpdate;
        }

        public AddressListController(Tenant tenant, AbstractClause selectNotIn) : this()
        {
            _tenant = tenant;
            _selectNotIn = selectNotIn;
            BindTenantVisibility = Visibility.Visible;
            AllowNewRecord = false;
            WindowLoaded += OnWindowLoaded;
            WindowClosed += OnWindowClosed;
        }

        private void BindTenant(Address address)
        {
            if (_tenant == null)
            {
                Failure.Allert("Tenant not set");
                return;
            }

            if (address == null)
            {
                Failure.Allert("Address not set");
                return;
            }

            IAbstractDatabase? db = DatabaseManager.Find<TenantAddress>() ?? throw new NullReferenceException();

            List<QueryParameter> para = [new("active", 0), new("tenantId", _tenant.TenantID)];
            db.Crud(CRUD.UPDATE, $"UPDATE TenantAddress SET Active = @active, MovedOut = CASE WHEN MovedOut IS NULL THEN CURRENT_DATE ELSE MovedOut END WHERE TenantID = @tenantId;", para);

            db.Model = new TenantAddress(_tenant, address);
            db.Crud(CRUD.INSERT);

            SuccessDialog.Display("Address changed");

            ((Window?)UI)?.Close();
        }

        private async void OnWindowLoaded(object? sender, RoutedEventArgs e)
        {
            ReloadSearchQry();
            RecordSource.ReplaceRange(await SearchRecordAsync());
            GoFirst();
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            WindowClosed -= OnWindowClosed;
            Helper.GetActiveWindow()?.GetController<TenantController>()?.TenantAddressController?.OnSubFormFilter();
        }
        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(Search))) 
                await OnSearchPropertyRequeryAsync(sender);
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            ReloadSearchQry();
            StreetNumOptions.Conditions<WhereClause>(SearchQry);
            PostCodeOptions.Conditions<WhereClause>(SearchQry);
            CityOptions.Conditions<WhereClause>(SearchQry);
            OnAfterUpdate(e, new(null, null, nameof(Search)));
        }

        public async override Task<IEnumerable<Address>> SearchRecordAsync()
        {
            SearchQry.AddParameter("search", Search.ToLower() + "%");
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        protected override void Open(Address model) { }

        public override AbstractClause InstantiateSearchQry() => (_selectNotIn != null) ? _selectNotIn :
        new Address().Select().All().Fields("Code").Fields("City.*")
             .From().InnerJoin(new PostCode())
             .InnerJoin(nameof(PostCode), nameof(City), "CityID")
             .Where()
             .OpenBracket()
                .Like("LOWER(StreetNum)", "@search").OR().Like("LOWER(StreetName)", "@search").OR().Like("LOWER(OtherInfo)", "@search").OR().Like("LOWER(Code)", "@search").OR().Like("LOWER(CityName)", "@search")
             .CloseBracket();
    }
}