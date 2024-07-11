using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(Reading))]
    public class Reading : AbstractModel<Reading>
    {
        #region backing fields
        private long _readingId;
        private TenantAddress? _tenantAddress;
        private string _readValue = string.Empty;
        private DateTime? _dor;
        #endregion

        #region Properties
        [PK]
        public long ReadingID { get => _readingId; set => UpdateProperty(ref value, ref _readingId); }
        [FK]
        public TenantAddress? TenantAddress { get => _tenantAddress; set => UpdateProperty(ref value, ref _tenantAddress); }
        [Field]
        public string ReadValue { get => _readValue; set => UpdateProperty(ref value, ref _readValue); }
        [Field]
        public DateTime? DOR { get => _dor; set => UpdateProperty(ref value, ref _dor); }
        #endregion

        #region Constructors
        public Reading() { }
        public Reading(long id) => _readingId = id;
        public Reading(DbDataReader reader) 
        {
            _readingId = reader.GetInt64(0);
            _tenantAddress = new(reader.GetInt64(1));
            _readValue = reader.GetString(2);
            _dor = reader.TryFetchDate(3);
        }
        #endregion

        public override string ToString() => $"Reading {ReadValue}, taken on {DOR} at {TenantAddress}";
    }
}