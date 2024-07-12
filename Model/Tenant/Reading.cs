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
        public Reading()
        {
            SelectQry = this.Select().Fields("Reading.ReadingID", "Reading.ReadValue", "Reading.DOR", "TenantAddress.*", "Tenant.FirstName", "Tenant.LastName", "Address.StreetNum", "Address.StreetName", "Address.OtherInfo", "Address.MeterNumber", "PostCode.*", "CityName")
                        .From()
                        .InnerJoin(new TenantAddress())
                        .InnerJoin(nameof(TenantAddress), nameof(Tenant), "TenantID")
                        .InnerJoin(nameof(TenantAddress), nameof(Address), "AddressID")
                        .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
                        .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Statement();
        }
        public Reading(long id) : this() => _readingId = id;
        public Reading(long id, string readValue, DateTime? dor, TenantAddress tenantAddress) : this(id)
        {
            _readValue = readValue;
            _dor = dor;
            _tenantAddress = tenantAddress;
        }
        public Reading(DbDataReader reader)
        {
            _readingId = reader.GetInt64(0);
            _readValue = reader.GetString(1);
            _dor = reader.TryFetchDate(2);
            _tenantAddress = new(reader.GetInt64(3));
            //_tenantAddress = new(reader.GetInt64(3), reader.TryFetchDate(4), reader.TryFetchDate(4), reader.GetBoolean(5),
            //    new Tenant(reader.GetInt64(6), reader.GetString(7), reader.GetString(8)),
            //    new Address(reader.GetInt64(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13), 
            //                new PostCode(reader.GetInt64(14), reader.GetString(15), 
            //                new City(reader.GetInt64(16), reader.GetString(17))
            //                ))
            //    );
        }
        #endregion

        public override string ToString() => $"Reading {ReadValue}, taken on {DOR} at {TenantAddress}";
    }
}