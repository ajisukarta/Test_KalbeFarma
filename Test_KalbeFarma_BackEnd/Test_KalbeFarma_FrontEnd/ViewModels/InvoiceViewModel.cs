using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_KalbeFarma_DataAccess.Models;

namespace Test_KalbeFarma_FrontEnd.ViewModels
{
    public class InvoiceViewModel
    {
        public int OrderID { get; set; }
        public string InvoiceNumber { get; set; }
        public string CurrencyCode { get; set; }
        public string From { get; set; }
        public int RecipientID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Total { get; set; }
        public RecipientViewModel SelectRecipient { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get; set; }
        public IEnumerable<SelectListItem> CurrencyList { get; set; }
        public IEnumerable<SelectListItem> RecipientList { get; set; }
        public List<PurchaseOrder> PurchaseOrderList { get; set; }
        public List<InvoiceDetailViewModel> InvoiceDetail { get; set; }
    }
}
