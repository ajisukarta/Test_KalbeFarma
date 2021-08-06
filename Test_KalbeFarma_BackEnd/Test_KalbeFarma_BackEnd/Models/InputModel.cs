using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_KalbeFarma_BackEnd.Models
{
    public class InputModel
    {
        public string InvoiceNo { get; set; }
        public IEnumerable<Currency> CurrencyList { get; set; }
        public IEnumerable<Language> LanguageList { get; set; }
        public IEnumerable<Recipient> RecipientList { get; set; }
        public IEnumerable<PurchaseOrder> PurchaseOrderList { get; set; }
    }
}
