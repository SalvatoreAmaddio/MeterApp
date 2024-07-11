using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class InvoicedReadingListController : AbstractFormListController<InvoicedReading>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<InvoicedReading>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(InvoicedReading model)
        {
            throw new NotImplementedException();
        }
    }
}