using System;
using System.Collections.Generic;

namespace Test_KalbeFarma_DataAccess.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string CurrencyCode { get; set; }
        public string From { get; set; }
        public int RecipientId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Pono { get; set; }
        public string DiscountName { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Recipient Recipient { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
