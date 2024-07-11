using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class CityListController : AbstractFormListController<City>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<City>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(City model)
        {
            throw new NotImplementedException();
        }
    }
}