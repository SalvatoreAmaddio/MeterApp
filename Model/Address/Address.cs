using FrontEnd.Model;
using Backend.Model;
using System.Data.Common;
using Backend.ExtensionMethods;

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
        public Address() 
        { 
            SelectQry = this.Select().All().Fields("Code").Fields("City.*")
                        .From().InnerJoin(new PostCode())
                        .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Statement();
        }

        public Address(long id) : this() => _addressId = id;
        public Address(long id, string streetNum, string streetName, string otherInfo, string meterNumber, PostCode postCode) : this(id)
        {
            _streetNum = streetNum;
            _streetName = streetName;
            _otherInfo = otherInfo;
            _meterNumber = meterNumber;
            _postCode = postCode;
        }

        public Address(DbDataReader reader) : this()
        {
            _addressId = reader.GetInt64(0);
            _streetNum = reader.GetString(1);
            _streetName = reader.GetString(2);
            _otherInfo = reader.GetString(3);
            _meterNumber = reader.GetString(4);
            _postCode = new(reader.GetInt64(5), reader.GetString(6), new City(reader.GetInt64(7), reader.GetString(8)));
        }
        #endregion

        public override string ToString() => $"{StreetNum}, {StreetName} - {OtherInfo}, {PostCode}";
    }
}