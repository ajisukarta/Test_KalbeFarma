using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test_KalbeFarma_FrontEnd.ViewModels;
using Newtonsoft.Json;
using Test_KalbeFarma_DataAccess.ViewModels;
using Test_KalbeFarma_DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Test_KalbeFarma_FrontEnd.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<Orders> orderList = new List<Orders>();
            var url = "http://localhost:24755/api/Order/GetAllOrders";
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    orderList = JsonConvert.DeserializeObject<IEnumerable<Orders>>(data);
                }
            }
            return View(orderList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Mode = "Create";
            var viewModel = new InvoiceViewModel();
            var url = "http://localhost:24755/api/Order/GetInputModel";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var objectReceived = JsonConvert.DeserializeObject<InputModel>(data);
                    var currency = from cur in objectReceived.CurrencyList.AsQueryable()
                                   select new SelectListItem()
                                   {
                                       Value = cur.Code,
                                       Text = cur.Name
                                   };
                    var languages = from lang in objectReceived.LanguageList.AsQueryable()
                                    select new SelectListItem()
                                    {
                                        Value = lang.Id.ToString(),
                                        Text = lang.Name
                                    };
                    var recipient = from rcpt in objectReceived.RecipientList.AsQueryable()
                                    select new SelectListItem()
                                    {
                                        Value = rcpt.Id.ToString(),
                                        Text = rcpt.Name
                                    };
                    viewModel.InvoiceNumber = objectReceived.InvoiceNo;
                    viewModel.CurrencyList = currency.ToList();
                    viewModel.LanguageList = languages.ToList();
                    viewModel.RecipientList = recipient.ToList();
                    viewModel.PurchaseOrderList = objectReceived.PurchaseOrderList.ToList();
                }
            }
            return View("Invoice",viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(InvoiceViewModel viewModel)
        {
            Orders newOrder = new Orders();
            newOrder.CurrencyCode = viewModel.CurrencyCode;
            newOrder.From = viewModel.From;
            newOrder.InvoiceNo = viewModel.InvoiceNumber;
            newOrder.RecipientId = viewModel.RecipientID;
            newOrder.OrderDate = viewModel.OrderDate;
            newOrder.Pono = viewModel.PurchaseOrderNumber;
            List<OrderDetails> orderDetails1 = new List<OrderDetails>();
            foreach(InvoiceDetailViewModel inv in viewModel.InvoiceDetail)
            {
                OrderDetails newOrderDetail = new OrderDetails();
                newOrderDetail.Name = inv.Name;
                newOrderDetail.Quantity = inv.Quantity;
                newOrderDetail.Rate = inv.Rate;
                newOrderDetail.Amount = inv.Amount;
                newOrderDetail.CreateDate = DateTime.Now;
                orderDetails1.Add(newOrderDetail);
            }
            ICollection<OrderDetails> orderDetails = orderDetails1;
            newOrder.OrderDetails =  orderDetails1;
            newOrder.SubTotal = viewModel.SubTotal;
            newOrder.DiscountName = viewModel.DiscountName;
            newOrder.DiscountPercentage = viewModel.DiscountPercentage;
            newOrder.DiscountValue = viewModel.DiscountValue;
            newOrder.Total = viewModel.Total;

            var url = "http://localhost:24755/api/Order/CreateNewOrder";
            StringContent content = new StringContent(JsonConvert.SerializeObject(newOrder), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24755/api/Order/CreateNewOrder");
                var response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Mode = "Edit";
            var viewModel = new InvoiceViewModel();
            viewModel.OrderID = id;
            var url = "http://localhost:24755/api/Order/GetInputModel";
            var url2 = "http://localhost:24755/api/Order/GetOrder/"+id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var objectReceived = JsonConvert.DeserializeObject<InputModel>(data);
                    var currency = from cur in objectReceived.CurrencyList.AsQueryable()
                                   select new SelectListItem()
                                   {
                                       Value = cur.Code,
                                       Text = cur.Name
                                   };
                    var languages = from lang in objectReceived.LanguageList.AsQueryable()
                                    select new SelectListItem()
                                    {
                                        Value = lang.Id.ToString(),
                                        Text = lang.Name
                                    };
                    var recipient = from rcpt in objectReceived.RecipientList.AsQueryable()
                                    select new SelectListItem()
                                    {
                                        Value = rcpt.Id.ToString(),
                                        Text = rcpt.Name
                                    };
                    viewModel.CurrencyList = currency.ToList();
                    viewModel.LanguageList = languages.ToList();
                    viewModel.RecipientList = recipient.ToList();
                    viewModel.PurchaseOrderList = objectReceived.PurchaseOrderList.ToList();
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url2);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url2);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var objectReceived = JsonConvert.DeserializeObject<Orders>(data);
                    viewModel.InvoiceNumber = objectReceived.InvoiceNo;
                    viewModel.OrderDate = objectReceived.OrderDate;
                    viewModel.CurrencyCode = objectReceived.CurrencyCode;
                    viewModel.From = objectReceived.From;
                    viewModel.RecipientID = objectReceived.RecipientId;
                    viewModel.PurchaseOrderNumber = objectReceived.Pono;
                    List<InvoiceDetailViewModel> invoiceDetails = new List<InvoiceDetailViewModel>();
                    foreach(var detail in objectReceived.OrderDetails)
                    {
                        InvoiceDetailViewModel invoiceDetail = new InvoiceDetailViewModel();
                        invoiceDetail.Name = detail.Name;
                        invoiceDetail.Quantity = detail.Quantity;
                        invoiceDetail.Rate = detail.Rate;
                        invoiceDetail.Amount = detail.Amount;
                        invoiceDetails.Add(invoiceDetail);
                    }
                    viewModel.InvoiceDetail = invoiceDetails;
                    viewModel.SubTotal = objectReceived.SubTotal;
                    viewModel.DiscountName = objectReceived.DiscountName;
                    viewModel.DiscountPercentage = Convert.ToDecimal(objectReceived.DiscountPercentage);
                    viewModel.DiscountValue = Convert.ToDecimal(objectReceived.DiscountValue);
                    viewModel.Total = objectReceived.Total;
                }
            }
            return View("Invoice", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(InvoiceViewModel viewModel)
        {
            Orders editOrder = new Orders();
            editOrder.Id = viewModel.OrderID;
            editOrder.CurrencyCode = viewModel.CurrencyCode;
            editOrder.From = viewModel.From;
            editOrder.InvoiceNo = viewModel.InvoiceNumber;
            editOrder.RecipientId = viewModel.RecipientID;
            editOrder.OrderDate = viewModel.OrderDate;
            editOrder.Pono = viewModel.PurchaseOrderNumber;
            List<OrderDetails> orderDetails1 = new List<OrderDetails>();
            foreach (InvoiceDetailViewModel inv in viewModel.InvoiceDetail)
            {
                OrderDetails newOrderDetail = new OrderDetails();
                newOrderDetail.Name = inv.Name;
                newOrderDetail.Quantity = inv.Quantity;
                newOrderDetail.Rate = inv.Rate;
                newOrderDetail.Amount = inv.Amount;
                newOrderDetail.CreateDate = DateTime.Now;
                orderDetails1.Add(newOrderDetail);
            }
            ICollection<OrderDetails> orderDetails = orderDetails1;
            editOrder.OrderDetails = orderDetails1;
            editOrder.SubTotal = viewModel.SubTotal;
            editOrder.DiscountName = viewModel.DiscountName;
            editOrder.DiscountPercentage = viewModel.DiscountPercentage;
            editOrder.DiscountValue = viewModel.DiscountValue;
            editOrder.Total = viewModel.Total;

            var url = "http://localhost:24755/api/Order/UpdateOrder/"+viewModel.OrderID;
            StringContent content = new StringContent(JsonConvert.SerializeObject(editOrder), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:24755/api/Order/UpdateOrder/"+viewModel.OrderID);
                var response = await client.PutAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var url = "http://localhost:24755/api/Order/DeleteOrder/" + id;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = await client.DeleteAsync(url);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        public async Task<JsonResult> GetRecipient(int recipientID)
        {
            Recipient recipient = new Recipient();
            var url = "http://localhost:24755/api/Order/GetRecipient/"+ recipientID;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    recipient = JsonConvert.DeserializeObject<Recipient>(data);
                }
            }
            return Json(recipient);
        }

        
    }
}
