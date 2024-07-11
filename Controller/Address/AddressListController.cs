using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class AddressListController : AbstractFormListController<Address>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Address>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Address model)
        {
            throw new NotImplementedException();
        }
    }
}