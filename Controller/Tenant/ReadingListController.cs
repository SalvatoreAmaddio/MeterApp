using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using Backend.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class ReadingListController : AbstractFormListController<Reading>
    {
        private bool _enabled = false;
        private DateTime? _from;
        private DateTime? _to;

        public bool Enabled { get => _enabled; set => UpdateProperty(ref value, ref _enabled); }
        public DateTime? From { get => _from; set => UpdateProperty(ref value, ref _from); }
        public DateTime? To { get => _to; set => UpdateProperty(ref value, ref _to); }

        internal ReadingListController() 
        {
            OpenWindowOnNew = false;
            AfterRecordNavigation += OnAfterRecordNavigation;
        }

        public override async void OnSubFormFilter()
        {
            AfterUpdate -= OnAfterUpdate;
            ReloadSearchQry();
            IEnumerable<Reading> records = await SearchRecordAsync();
            RecordSource.ReplaceRange(records);
            CurrentRecord = RecordSource.FirstOrDefault();
            Enabled = (ParentRecord == null) ? false : true;
            To = RecordSource.FirstOrDefault()?.DOR;
            From = RecordSource.LastOrDefault()?.DOR;
            AfterUpdate += OnAfterUpdate;
        }

        private async void OnAfterUpdate(object? sender, AfterUpdateArgs e)
        {
            if (e.Is(nameof(From)) || e.Is(nameof(To)))
            {
                SearchQry.AddParameter("date1", From);
                SearchQry.AddParameter("date2", To);

                SearchQry.GetClause<WhereClause>()?.AND().Between("DOR", "@date1", "@date2");

                IEnumerable<Reading> records = await SearchRecordAsync();
                RecordSource.ReplaceRange(records);
            }
        }

        public override void OnOptionFilterClicked(FilterEventArgs e) { }
        protected override void Open(Reading model) { }
        public override async Task<IEnumerable<Reading>> SearchRecordAsync()
        {
            SearchQry.AddParameter("tenantAddressId", ((TenantAddress?)ParentRecord)?.TenantAddressID);
            return await CreateFromAsyncList(SearchQry.Statement(), SearchQry.Params());
        }

        private void OnAfterRecordNavigation(object? sender, AllowRecordMovementArgs e)
        {
            if (CurrentRecord != null)
            {
                CurrentRecord.TenantAddress = (TenantAddress?)ParentRecord;
                CurrentRecord.Clean();
            }
        }

        public override AbstractClause InstantiateSearchQry() =>
        new Reading().Select().Fields("Reading.ReadingID", "Reading.ReadValue", "Reading.DOR", "TenantAddress.*", "Tenant.FirstName", "Tenant.LastName", "Address.StreetNum", "Address.StreetName", "Address.OtherInfo", "Address.MeterNumber", "PostCode.*", "CityName")
                        .From()
                        .InnerJoin(new TenantAddress())
                        .InnerJoin(nameof(TenantAddress), nameof(Tenant), "TenantID")
                        .InnerJoin(nameof(TenantAddress), nameof(Address), "AddressID")
                        .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
                        .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Where()
                            .OpenBracket()
                                .EqualsTo("TenantAddress.TenantAddressID", "@tenantAddressId")
                            .CloseBracket()
                        .OrderBy().Field("DOR DESC");        
    }
}