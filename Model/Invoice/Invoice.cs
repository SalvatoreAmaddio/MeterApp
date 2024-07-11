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
        [Field]
        public DateTime? DOI { get => _doi; set => UpdateProperty(ref value, ref _doi); }
        [Field]
        public double Total { get => _total; set => UpdateProperty(ref value, ref _total); }
        [Field]
        public double AmountPaid { get => _amountPaid; set => UpdateProperty(ref value, ref _amountPaid); }
        [Field]
        public bool IsPaid { get => _isPaid; set => UpdateProperty(ref value, ref _isPaid); }
        [FK]
        public TenantAddress? TenantAddress { get => _tenantAddress; set => UpdateProperty(ref value, ref _tenantAddress); }
        #endregion

        #region Constructors
        public Invoice() 
        {
            SelectQry = this.Select()
                        .Fields("Invoice.InvoiceID", "Invoice.DOI", "Invoice.Total", "Invoice.AmountPaid", "Invoice.IsPaid", "TenantAddress.*", "Tenant.FirstName", "Tenant.LastName", "Address.StreetNum", "Address.StreetName", "Address.OtherInfo", "Address.MeterNumber", "PostCode.*", "CityName")
                        .From()
                            .InnerJoin(new TenantAddress())
                            .InnerJoin(nameof(TenantAddress), nameof(Tenant), "TenantID")
                            .InnerJoin(nameof(TenantAddress), nameof(Address), "AddressID")
                            .InnerJoin(nameof(Address), nameof(PostCode), "PostCodeID")
                            .InnerJoin(nameof(PostCode), nameof(City), "CityID")
                        .Statement();
        }
        public Invoice(long id) : this() => _invoiceId = id;
        public Invoice(long id, TenantAddress tenantAddress) : this(id) => _tenantAddress = tenantAddress;
        public Invoice(DbDataReader reader) : this(reader.GetInt64(0))
        {
            _doi = reader.TryFetchDate(1);
            _total = reader.TryFetchDouble(2);
            _amountPaid = reader.TryFetchDouble(3);
            _isPaid = reader.GetBoolean(4);
            _tenantAddress = new TenantAddress(reader.GetInt64(5), reader.TryFetchDate(6), reader.TryFetchDate(7), reader.GetBoolean(8),
                                    new Tenant(reader.GetInt64(9), reader.GetString(10), reader.GetString(11)), 
                                    new Address(reader.GetInt64(12), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetString(16), 
                                        new PostCode(reader.GetInt64(17), reader.GetString(18), 
                                            new City(reader.GetInt64(19), reader.GetString(20))
                                            ))
                                    );
        }
        #endregion

        public override string ToString() => $"{InvoiceID} on {DOI} for {TenantAddress?.Tenant} living in {TenantAddress?.Address}";
    }
}