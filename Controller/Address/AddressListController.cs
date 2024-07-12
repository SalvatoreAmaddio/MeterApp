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
    public class AddressListController : AbstractFormListController<Address>
    {
        public RecordSource<City> Cities { get; private set; } = new (DatabaseManager.Find<City>()!);
        public RecordSource<PostCode> PostCodes { get; private set; } = new (DatabaseManager.Find<PostCode>()!);
        public SourceOption StreetNumOptions { get; }
        public SourceOption PostCodeOptions { get; }
        public SourceOption CityOptions { get; }

        public AddressListController()
        {
            OpenWindowOnNew = false;
            StreetNumOptions = new PrimitiveSourceOption(this, "StreetNum");
            PostCodeOptions = new SourceOption(PostCodes, "Code");
            CityOptions = new SourceOption(Cities, "CityName");
            AfterUpdate += OnAfterUpdate;
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

        public override AbstractClause InstantiateSearchQry() =>
        new Address().Select().All().Fields("Code").Fields("City.*")
             .From().InnerJoin(new PostCode())
             .InnerJoin(nameof(PostCode), nameof(City), "CityID")
             .Where()
             .OpenBracket()
                .Like("LOWER(StreetNum)", "@search").OR().Like("LOWER(StreetName)", "@search").OR().Like("LOWER(OtherInfo)", "@search").OR().Like("LOWER(Code)", "@search").OR().Like("LOWER(CityName)", "@search")
             .CloseBracket();
    }
}