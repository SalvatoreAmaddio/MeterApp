using FrontEnd.Controller;
using MeterApp.Model;

namespace MeterApp.Controller
{
    public class TenantController : AbstractFormController<Tenant>
    {
        internal TenantController() { }

        public TenantController(Tenant tenant) : this()
        {
            GoAt(tenant);
        }
    }
}