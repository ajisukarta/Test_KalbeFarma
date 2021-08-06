using System;
using System.Collections.Generic;

namespace Test_KalbeFarma_BackEnd.Models
{
    public partial class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Orders Order { get; set; }
    }
}
