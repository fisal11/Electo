using AutoMapper;
using Electo.DAL.Context;
using Electo.DAL.Entities;
using Electo.PL.Models;
using Electo.PL.Repositories;
using Electo.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Stripe;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;

namespace Electo.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private UnitOfWork _unitOfWork;

        IConfiguration _iconfiguration;

        public HomeController(IMapper mapper,ApplicationDbContext context,
            IConfiguration iconfiguration)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
            _iconfiguration = iconfiguration;
        }
        public IActionResult Index()
        {

            var getAllCategory = _context.Category
                .Select(a => new CategoryVM
            {
                Id =a.Id,
                CategoryName =a.CategoryName,
                Product= a.Product
            }).ToList();
            return View(getAllCategory);

        }
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchName )
        {
            if (searchName != null)
            {
                var search = _unitOfWork.ProductRepository.Get().Where(
                    a => a.Name.Contains(searchName) || a.Descrption.Contains(searchName)
                 ).ToList();
                var data = _mapper.Map<IEnumerable<ProductVM>>(search);
                return View(data);
            }
            return View();
        }
        public IActionResult GetProductByCategory(int Id)
        {
              var getData = _context.Category.Where(a =>a.Id == Id)
                .Select(a => new CategoryVM
              {
                  Id = a.Id,
                  CategoryName = a.CategoryName,
                  Product = a.Product
              }).ToList();
              return View(getData);
        }
        public IActionResult PDetails(int id) 
        {
            var getData = _context.Product.Where(a=>a.Id==id).Select(a => new ProductVM
            {
                Id = a.Id,
                Name = a.Name,
                Salary = a.Salary,
                Image = a.Image,
                Descrption = a.Descrption,
                BrandName = a.Brand.BrandName,
                CategoryName = a.Category.CategoryName
            }).ToList();
            return View(getData);

        }
        public IActionResult Contect()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contect(Contectus contectus)
        {

            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("Email", "Password");
                smtp.Send("Email", "Email resever", contectus.Email, contectus.Message);

                TempData["Mess"] = "Mail Sent Successfully";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e;

            }

            return View();
        }
        public IActionResult GetAllPrands(int Id)
        {
   
            var getData = _context.Brand.Where(a => a.Id == Id)
              .Select(a => new BrandVM
              {
                  Id = a.Id,
                  BrandName = a.BrandName,
                  Product = a.Product
              }).ToList();
            return View(getData);
        }
        [Authorize]
        public IActionResult AddToCart(int ProductId, int Quantity)
        {

            var product = _context.Product.Find(ProductId).Salary;
            var CustomerId = GetCustomerId();
            var cart = new CartVM 
            {
                ProductId = ProductId,
                Quantity = Quantity,
                Price = product,
                CustomerId = CustomerId
            };
            var mapData = _mapper.Map<Cart>(cart);
            _context.Carts.Add(mapData);
            _context.SaveChanges();
            return RedirectToAction("Cart");
        }
        public IActionResult Cart()
        {
            var CustomerId = "";

            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                CustomerId = GetCustomerId();
            }
            else
            {
                CustomerId = HttpContext.Session.GetString("CustomerId");
            }
            var cartItem = _context.Carts.Include(a => a.Product)
                .Where(a => a.CustomerId == CustomerId)
                .ToList();
            return View(cartItem);
        }
        private string GetCustomerId()
        {
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                var CustomerId = "";

                if (User.Identity.IsAuthenticated)
                {
                    CustomerId = User.Identity.Name;
                }
                else
                {
                    CustomerId = Guid.NewGuid().ToString();
                }
                HttpContext.Session.SetString("CustomerId", CustomerId);
            }
            return HttpContext.Session.GetString("CustomerId");
        }
        public IActionResult RemoveCart(int id ) 
        {
            var cartItem = _context.Carts.Find(id);
            if (cartItem != null) 
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Cart");
        }
        public IActionResult CheckOut() 
        {


            return View();
        }
        [HttpPost]
        public IActionResult CheckOut([Bind("FirstName", "LastName", "Address", "City",
            "Province","PostalCode","Phone")]OrderVM orderVM)
        {
             _mapper.Map<DAL.Entities.Order>(orderVM);

            orderVM.CustomerId = User.Identity.Name;

             orderVM.Total = (from a in _context.Carts
                              where  a.CustomerId == HttpContext.Session.GetString("CustomerId")
                              select a.Quantity * a.Price).Sum();


            HttpContext.Session.SetObject("Order", orderVM );

            return RedirectToAction("Payment");
        }
        public IActionResult Payment()
        {
            var order = HttpContext.Session.GetObject<DAL.Entities.Order>("Order");

            ViewBag.Total = order.Total;
            ViewBag.PublishKey = _iconfiguration.GetSection("Stripe")["Publishablekey"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment() 
        {
            var order = HttpContext.Session.GetObject<DAL.Entities.Order>("Order");
            StripeConfiguration.ApiKey = _iconfiguration.GetSection("Stripe")["SecretKey"];

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
            {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = ((long?)(order.Total*100)),
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "Elect Electronics",
              },

            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = "https://"+ Request.Host +"/Home/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Home/Cart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }

        [HttpPost]
        public IActionResult SaveOrder() 
        {
            var order = HttpContext.Session.GetObject<DAL.Entities.Order>("Order");
            _context.Orders.Add(order);
            _context.SaveChanges();

            var cartitem = _context.Carts.Where(a=>a.CustomerId == HttpContext.Session.GetString("CustomerId"));

            foreach (var item in cartitem)
            {
                var orderdetails = new OrderDetail{
                
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = order.Id
                };
                _context.OrderDetails.Add(orderdetails);
            }
            _context.SaveChanges();

            foreach (var item in cartitem)
            {
                _context.Carts.Remove(item);
            }
            _context.SaveChanges();
            HttpContext.Session.SetInt32("ItemCount", 0);

            return RedirectToAction("Details", "Orders", new{@id=order.Id});
        }
    }
}
