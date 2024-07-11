using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(Tenant))]
    public class Tenant : AbstractModel<Tenant>
    {
        #region backing fields
        private long _tenantId;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private DateTime? _dob;
        private string _email = string.Empty;
        private string _notes = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long TenantID { get => _tenantId; set => UpdateProperty(ref value, ref _tenantId); }
        [Field]
        public string FirstName { get => _firstName; set => UpdateProperty(ref value, ref _firstName); }
        [Field]
        public string LastName { get => _lastName; set => UpdateProperty(ref value, ref _lastName); }
        [Field]
        public DateTime? DOB { get => _dob; set => UpdateProperty(ref value, ref _dob); }
        [Field]
        public string Email { get => _email; set => UpdateProperty(ref value, ref _email); }
        [Field]
        public string Notes { get => _notes; set => UpdateProperty(ref value, ref _notes); }
        #endregion

        #region Constructors
        public Tenant() { }
        public Tenant(long id) => _tenantId = id;
        public Tenant(DbDataReader reader) 
        {
            _tenantId = reader.GetInt64(0);
            _firstName = reader.GetString(1);
            _lastName = reader.GetString(2);
            _dob = reader.TryFetchDate(3);
            _email = reader.GetString(4);
            _notes = reader.GetString(5);
        }
        #endregion

        public override string ToString() => $"{FirstName} {LastName}";
    }
}