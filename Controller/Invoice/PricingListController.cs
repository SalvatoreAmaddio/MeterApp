using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class PricingListController : AbstractFormListController<Pricing>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Pricing>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Pricing model)
        {
            throw new NotImplementedException();
        }
    }
}