using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Test_KalbeFarma_BackEnd.Methods;
using Test_KalbeFarma_BackEnd.Models;

namespace Test_KalbeFarma_BackEnd.Controllers
{
    [Route("api/{controller}/{action}/{id?}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencyList()
        {
            return await _orderRepository.GetAllCurrencies();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguageList()
        {
            return await _orderRepository.GetAllLanguages();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetRecipientList()
        {
            return await _orderRepository.GetAllRecipients();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrderList()
        {
            return await _orderRepository.GetAllPurchaseOrder();
        }

        [HttpGet]
        public async Task<ActionResult<Orders>> GetOrder(int id)
        {
            return await _orderRepository.GetOrder(id);
        }

        [HttpGet]
        public ActionResult<InputModel> GetInputModel()
        {
            return _orderRepository.GetInputModel();
        }

        [HttpPost]
        public void CreateNewOrder([FromBody]Orders order)
        {
            _orderRepository.CreateOrder(order);
        }

        [HttpPut]
        public void UpdateOrder(int id, [FromBody]Orders orders)
        {
            _orderRepository.UpdateOrder(id, orders);
        }

        [HttpDelete]
        public void DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }

        [HttpGet]
        public async Task<ActionResult<Recipient>> GetRecipient(int id)
        {
            return await _orderRepository.GetRecipient(id);
        }
    }
}
