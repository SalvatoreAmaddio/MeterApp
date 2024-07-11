using Backend.Model;
using FrontEnd.Controller;
using FrontEnd.Events;
using MeterApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterApp.Controller
{
    public class TenantListController : AbstractFormListController<Tenant>
    {
        public override AbstractClause InstantiateSearchQry()
        {
            throw new NotImplementedException();
        }

        public override void OnOptionFilterClicked(FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Tenant>> SearchRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected override void Open(Tenant model)
        {
            throw new NotImplementedException();
        }
    }
}
