using FrontEnd.Model;
using Backend.Model;
using System.Data.Common;
using Backend.ExtensionMethods;

namespace MeterApp.Model
{
    [Table(nameof(TenantAddress))]
    public class TenantAddress : AbstractModel<TenantAddress>
    {
        #region backing fields
        private long _tenantAddressId;
        private Tenant? _tenant;
        private Address? _address;
        private DateTime? _movedIn;
        private DateTime? _movedOut;
        private bool _active;
        #endregion

        #region Properties
        [PK]
        public long TenantAddressID { get => _tenantAddressId; set => UpdateProperty(ref value, ref _tenantAddressId); }
        [Field]
        public DateTime? MovedIn { get => _movedIn; set => UpdateProperty(ref value, ref _movedIn); }
        [Field]
        public DateTime? MovedOut { get => _movedOut; set => UpdateProperty(ref value, ref _movedOut); }
        [Field]
        public bool Active { get => _active; set => UpdateProperty(ref value, ref _active); }
        [FK]
        public Tenant? Tenant { get => _tenant; set => UpdateProperty(ref value, ref _tenant); }
        [FK]
        public Address? Address { get => _address; set => UpdateProperty(ref value, ref _address); }
        #endregion

        #region Constructors
        public TenantAddress()
        {
            SelectQry = this.Select().All()
                        .Fields("Tenant.FirstName", "Tenant.LastName", "Address.StreetNum", "Address.StreetName", "Address.OtherInfo", "Address.MeterNumber", "PostCode.*", "City.CityName")
                        .From().InnerJoin(new Tenant())
                               .InnerJoin(new Address())
                               .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
                               .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Statement();
        }
        public TenantAddress(long id) : this() => _tenantAddressId = id;
        public TenantAddress(long id, DateTime? movedIn, DateTime? movedOut, bool active, Tenant tenant, Address address) : this(id) 
        {
            _movedIn = movedIn;
            _movedOut = movedOut;
            _active = active;
            _tenant = tenant;
            _address = address;
        }
        public TenantAddress(DbDataReader reader) : this()
        {
            _tenantAddressId = reader.GetInt64(0);
            _movedIn = reader.TryFetchDate(1);
            _movedOut = reader.TryFetchDate(2);
            _active = reader.GetBoolean(3);
            _tenant = new(reader.GetInt64(4), reader.GetString(6), reader.GetString(7));
            _address = new(reader.GetInt64(5), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), new PostCode(reader.GetInt64(12), reader.GetString(13), new City(reader.GetInt64(14), reader.GetString(15))));
        }

        public TenantAddress(Tenant tenant, Address address) 
        { 
            _tenant = tenant;
            _address = address;
            _movedIn = DateTime.Today;
            _active = true;
        }
        #endregion

        public override string ToString() => $"{Tenant} moved in {Address} on {MovedIn}, moved out on {MovedOut}";
    }
}