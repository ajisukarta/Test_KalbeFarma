using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_KalbeFarma_BackEnd.Models;
using Test_KalbeFarma_BackEnd.Models;

namespace Test_KalbeFarma_BackEnd.Methods
{
    public class OrderImplementationcs : IOrderRepository
    {
        private readonly Test_KalbeFarmaContext context;

        public OrderImplementationcs(Test_KalbeFarmaContext context)
        {
            this.context = context;
        }

        public void CreateOrder(Orders orders)
        {
            orders.CreateDate = DateTime.Now;
            context.Orders.Add(orders);
            context.SaveChanges();
        }

        public async Task<ActionResult<IEnumerable<Orders>>> GetAllOrders()
        {
            return await context.Orders.ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<Currency>>> GetAllCurrencies()
        {
            return await context.Currency.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Language>>> GetAllLanguages()
        {
            return await context.Language.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetAllPurchaseOrder()
        {
            return await context.PurchaseOrder.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Recipient>>> GetAllRecipients()
        {
            return await context.Recipient.ToListAsync();
        }

        public async Task<ActionResult<Orders>> GetOrder(int orderID)
        {
            var myOrder = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderID);
            var myOrderDetails = context.OrderDetails.Where(x => x.OrderId == orderID).ToList();
            myOrder.OrderDetails = myOrderDetails;
            return myOrder;
        }

        public void UpdateOrder(int orderID, Orders orders)
        {
            var myOrder = context.Orders.FirstOrDefault(x => x.Id == orderID);
            myOrder.From = orders.From;
            myOrder.RecipientId = orders.RecipientId;
            myOrder.OrderDate = orders.OrderDate;
            myOrder.Pono = orders.Pono;
            myOrder.DiscountName = orders.DiscountName;
            myOrder.DiscountPercentage = orders.DiscountPercentage;
            myOrder.DiscountValue = orders.DiscountValue;
            myOrder.SubTotal = orders.SubTotal;
            myOrder.Total = orders.Total;
            myOrder.OrderDetails = orders.OrderDetails;                 
            context.SaveChanges();
        }

        public void DeleteOrder(int orderID)
        {
            var myOrder = context.Orders.FirstOrDefault(x => x.Id == orderID);
            var orderDetails = context.OrderDetails.Where(x => x.OrderId == orderID);
            foreach(var detail in orderDetails)
            {
                context.OrderDetails.Remove(detail);
            }
            context.Orders.Remove(myOrder);
            context.SaveChanges();
        }

        public string GenerateInvoiceNumber(string lastInvoiceNo)
        {
            int nextNo = Convert.ToInt32(lastInvoiceNo.Substring(4)) + 1;
            string nextInvoiceNo;
            
            if (nextNo < 10)
            {
                nextInvoiceNo = "INV-00" + nextNo;
            }
            else if (nextNo < 100)
            {
                nextInvoiceNo = "INV-0" + nextNo;
            }
            else
            {
                nextInvoiceNo = "INV-" + nextNo;
            }
            return nextInvoiceNo;
        }

        public InputModel GetInputModel()
        {
            InputModel inputModel = new InputModel();
            string lastInvoiceNo = context.Orders.OrderByDescending(t => t.CreateDate).Select(s => s.InvoiceNo).FirstOrDefault();
            string nextInvoiceNo;
            if(lastInvoiceNo == null)
            {
                nextInvoiceNo = "INV-001";
            }
            else
            {
                nextInvoiceNo = GenerateInvoiceNumber(lastInvoiceNo);
            }
            inputModel.InvoiceNo = nextInvoiceNo;
            inputModel.LanguageList = context.Language.ToList();
            inputModel.CurrencyList = context.Currency.ToList();
            inputModel.RecipientList = context.Recipient.ToList();
            inputModel.PurchaseOrderList = context.PurchaseOrder.ToList();
            return inputModel;
        }

        public async Task<ActionResult<Recipient>> GetRecipient(int recipientID)
        {
            return await context.Recipient.FirstOrDefaultAsync(x => x.Id == recipientID);
        }
    }
}
