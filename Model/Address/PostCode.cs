using FrontEnd.Model;
using Backend.Model;
using System.Data.Common;
using Backend.ExtensionMethods;

namespace MeterApp.Model
{
    [Table(nameof(PostCode))]
    public class PostCode : AbstractModel<PostCode>
    {
        #region backing fields
        private long _postcodeId;
        private string _code = string.Empty;
        private City? _city;
        #endregion

        #region Properties
        [PK]
        public long PostCodeID { get => _postcodeId; set => UpdateProperty(ref value, ref _postcodeId); }
        [Field]
        public string Code { get => _code; set => UpdateProperty(ref value, ref _code); }
        [FK]
        public City? City { get => _city; set => UpdateProperty(ref value, ref _city); }
        #endregion

        #region Constructors
        public PostCode() 
        { 
            SelectQry = this.Select().All().Fields("CityName").From().InnerJoin(new City()).ToString();
        }
        public PostCode(long id) : this() => _postcodeId = id;
        public PostCode(long id, string code) : this(id) => _code = code;
        public PostCode(long id, string code, City city) : this(id, code) => _city = city;
        public PostCode(DbDataReader reader) : this()
        {
            _postcodeId = reader.GetInt64(0);
            _code = reader.GetString(1);
            _city = new(reader.GetInt64(2), reader.GetString(4));
        }
        #endregion

        public override string ToString() => $"{Code}, {City}";
    }
}