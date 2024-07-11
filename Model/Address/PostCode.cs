using FrontEnd.Model;
using Backend.Model;
using System.Data.Common;

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
        public PostCode() { }
        public PostCode(long id) => _postcodeId = id;
        public PostCode(DbDataReader reader) 
        {
            _postcodeId = reader.GetInt64(0);
            _code = reader.GetString(1);
            _city = new(reader.GetInt64(2));
        }
        #endregion

        public override string ToString() => $"{Code}, {City}";
    }
}