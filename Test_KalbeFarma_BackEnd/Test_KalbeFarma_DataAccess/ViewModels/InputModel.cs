using System;
using System.Collections.Generic;
using System.Text;
using Test_KalbeFarma_DataAccess.Models;

namespace Test_KalbeFarma_DataAccess.ViewModels
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
