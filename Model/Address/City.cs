using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(City))]
    public class City : AbstractModel<City>
    {
        #region backing fields
        private long _cityId;
        private string _cityName = string.Empty;
        #endregion

        #region Properties
        [PK]
        public long CityID { get => _cityId; set => UpdateProperty(ref value, ref _cityId); }
        [Field]
        public string CityName { get => _cityName; set => UpdateProperty(ref value, ref _cityName); }
        #endregion

        #region Constructors
        public City() { }
        public City(long id) => _cityId = id;
        public City(DbDataReader reader) 
        {
            _cityId = reader.GetInt64(0);
            _cityName = reader.GetString(1);
        }
        #endregion

        public override string ToString() => CityName;
    }
}