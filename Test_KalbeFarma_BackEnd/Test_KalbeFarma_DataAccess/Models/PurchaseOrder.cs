using System;
using System.Collections.Generic;

namespace Test_KalbeFarma_DataAccess.Models
{
    public partial class PurchaseOrder
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public string Pic { get; set; }
    }
}
