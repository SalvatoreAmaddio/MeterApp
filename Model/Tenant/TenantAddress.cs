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
        [FK]
        public Tenant? Tenant { get => _tenant; set => UpdateProperty(ref value, ref _tenant); }
        [FK]
        public Address? Address { get => _address; set => UpdateProperty(ref value, ref _address); }
        [Field]
        public DateTime? MovedIn { get => _movedIn; set => UpdateProperty(ref value, ref _movedIn); }
        [Field]
        public DateTime? MovedOut { get => _movedOut; set => UpdateProperty(ref value, ref _movedOut); }
        [Field]
        public bool Active { get => _active; set => UpdateProperty(ref value, ref _active); }
        #endregion

        #region Constructors
        public TenantAddress() { }
        public TenantAddress(long id) => _tenantAddressId = id;
        public TenantAddress(DbDataReader reader) 
        {
            _tenantAddressId = reader.GetInt64(0);
            _tenant = new(reader.GetInt64(1));
            _address = new(reader.GetInt64(2));
            _movedIn = reader.TryFetchDate(3);
            _movedOut = reader.TryFetchDate(4);
            _active = reader.GetBoolean(5);
        }
        #endregion

        public override string ToString() => $"{Tenant} moved in {Address} on {MovedIn}, moved out on {MovedOut}";
    }
}