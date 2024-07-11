using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(InvoicedReading))]
    public class InvoicedReading : AbstractModel<InvoicedReading>
    {
        #region backing fields
        private long _invoicedReadingId;
        private Invoice? _invoice;
        private Reading? _reading;
        #endregion

        #region Properties
        [PK]
        public long InvoicedReadingID { get => _invoicedReadingId; set => UpdateProperty(ref value, ref _invoicedReadingId); }
        [FK]
        public Invoice? Invoice { get => _invoice; set => UpdateProperty(ref value, ref _invoice); }
        [FK]
        public Reading? Reading { get => _reading; set => UpdateProperty(ref value, ref _reading); }
        #endregion

        #region Constructors
        public InvoicedReading()
        {
            SelectQry = this.Select().All().Fields("Reading.ReadValue", "Reading.DOR", "Reading.TenantAddressID")
                        .From().InnerJoin(new Reading())
                        .Statement();
        }
        public InvoicedReading(DbDataReader reader) : this()
        {
            _invoicedReadingId = reader.GetInt64(0);
            _invoice = new(reader.GetInt64(1), new(reader.GetInt64(5)));
            _reading = new(reader.GetInt64(2), reader.GetString(3), reader.TryFetchDate(4), new(reader.GetInt64(5)));
        }
        #endregion

        public override string ToString() => $"{Invoice} {Reading}";
    }
}