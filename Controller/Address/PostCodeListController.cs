using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class PostCodeListController : AbstractFormListController<PostCode>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<PostCode>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(PostCode model)
        {
            throw new NotImplementedException();
        }
    }
}