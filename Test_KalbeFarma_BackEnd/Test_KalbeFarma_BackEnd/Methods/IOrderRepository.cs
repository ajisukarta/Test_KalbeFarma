using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_KalbeFarma_BackEnd.Models;

namespace Test_KalbeFarma_BackEnd.Methods
{
    public interface IOrderRepository
    {
        Task<ActionResult<IEnumerable<Currency>>> GetAllCurrencies();
        Task<ActionResult<IEnumerable<Orders>>> GetAllOrders();
        Task<ActionResult<IEnumerable<Language>>> GetAllLanguages();
        Task<ActionResult<IEnumerable<Recipient>>> GetAllRecipients();
        Task<ActionResult<IEnumerable<PurchaseOrder>>> GetAllPurchaseOrder();
        Task<ActionResult<Recipient>> GetRecipient(int recipientID);
        void CreateOrder(Orders orders);
        Task<ActionResult<Orders>> GetOrder(int orderID);
        void UpdateOrder(int orderID, Orders orders);
        void DeleteOrder(int orderID);
        InputModel GetInputModel();

    }
}
