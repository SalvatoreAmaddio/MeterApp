using Backend.ExtensionMethods;
using Backend.Model;
using FrontEnd.Model;
using System.Data.Common;

namespace MeterApp.Model
{
    [Table(nameof(Invoice))]
    public class Invoice : AbstractModel<Invoice>
    {
        #region backing fields
        private long _invoiceId;
        private TenantAddress? _tenantAddress;
        private DateTime? _doi;
        private double _total;
        private double _amountPaid;
        private bool _isPaid;
        #endregion

        #region Properties
        [PK]
        public long InvoiceID { get => _invoiceId; set => UpdateProperty(ref value, ref  _invoiceId); }
        [FK]
        public TenantAddress? TenantAddress { get => _tenantAddress; set => UpdateProperty(ref value, ref _tenantAddress); }
        [Field]
        public DateTime? DOI { get => _doi; set => UpdateProperty(ref value, ref _doi); }
        [Field]
        public double Total { get => _total; set => UpdateProperty(ref value, ref _total); }
        [Field]
        public double AmountPaid { get => _amountPaid; set => UpdateProperty(ref value, ref _amountPaid); }
        [Field]
        public bool IsPaid { get => _isPaid; set => UpdateProperty(ref value, ref _isPaid); }
        #endregion

        #region Constructors
        public Invoice() { }
        public Invoice(long id) => _invoiceId = id;
        public Invoice(DbDataReader reader) 
        {
            _invoiceId = reader.GetInt64(0);
            _tenantAddress = new TenantAddress(reader.GetInt64(1));
            _doi = reader.TryFetchDate(2);
            _total = reader.TryFetchDouble(3);
            _amountPaid = reader.TryFetchDouble(4);
            _isPaid = reader.GetBoolean(5);
        }
        #endregion

        public override string ToString() => $"{InvoiceID} on {DOI} for {TenantAddress?.Tenant} living in {TenantAddress?.Address}";
    }
}