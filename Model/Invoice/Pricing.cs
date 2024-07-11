using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(Pricing))]
    public class Pricing : AbstractModel<Pricing>
    {
        #region backign fields
        private long _pricingId;
        private string _description = string.Empty;
        private double _priceValue;
        #endregion

        #region Properties
        [PK]
        public long PricingID { get => _pricingId; set => UpdateProperty(ref value, ref _pricingId); }
        [Field]
        public string Description { get => _description; set => UpdateProperty(ref value, ref _description); }
        [Field]
        public double PriceValue { get => _priceValue; set => UpdateProperty(ref value, ref _priceValue); }
        #endregion

        #region Constructors
        public Pricing() { }
        public Pricing(DbDataReader reader) 
        {
            _pricingId = reader.GetInt64(0);
            _description = reader.GetString(1);
            _priceValue = reader.GetDouble(2);
        }
        #endregion

        public override string ToString() => $"{Description}: {_priceValue.ToString("N2")}";
    }
}