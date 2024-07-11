using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class InvoiceListController : AbstractFormListController<Invoice>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Invoice>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Invoice model)
        {
            throw new NotImplementedException();
        }
    }
}