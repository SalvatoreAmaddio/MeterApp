using FrontEnd.Model;
using Backend.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(Address))]
    public class Address : AbstractModel<Address>
    {
        #region backing fields
        private long _addressId;
        private string _streetNum = string.Empty;
        private string _streetName = string.Empty;
        private string _otherInfo = string.Empty;
        private string _meterNumber = string.Empty;
        private PostCode? _postCode;
        #endregion

        #region Properties
        [PK]
        public long AddressID { get => _addressId; set => UpdateProperty(ref value, ref _addressId); }
        [Field]
        public string StreetNum { get => _streetNum; set => UpdateProperty(ref value, ref _streetNum); }
        [Field]
        public string StreetName { get => _streetName; set => UpdateProperty(ref value, ref _streetName); }
        [Field]
        public string OtherInfo { get => _otherInfo; set => UpdateProperty(ref value, ref _otherInfo); }
        [Field]
        public string MeterNumber { get => _meterNumber; set => UpdateProperty(ref value, ref _meterNumber); }
        [FK]
        public PostCode? PostCode { get => _postCode; set => UpdateProperty(ref value, ref _postCode); }
        #endregion

        #region Constructor
        public Address() { }
        public Address(long id) => _addressId = id;
        public Address(DbDataReader reader) 
        {
            _addressId = reader.GetInt64(0);
            _streetNum = reader.GetString(1);
            _streetName = reader.GetString(2);
            _otherInfo = reader.GetString(3);
            _meterNumber = reader.GetString(4);
            _postCode = new(reader.GetInt64(5));
        }
        #endregion

        public override string ToString() => $"{StreetNum}, {StreetName} - {OtherInfo}, {PostCode}";
    }
}