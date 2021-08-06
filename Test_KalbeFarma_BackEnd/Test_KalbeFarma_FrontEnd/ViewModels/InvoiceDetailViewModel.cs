using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_KalbeFarma_FrontEnd.ViewModels
{
    public class InvoiceDetailViewModel
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
