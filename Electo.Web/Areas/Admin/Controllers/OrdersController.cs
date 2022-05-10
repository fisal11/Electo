using AutoMapper;
using Electo.DAL.Context;
using Electo.DAL.Entities;
using Electo.PL.Models;
using Electo.PL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Electo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admins")]
    public class OrdersController : Controller
    {
        // GET: OrdersController
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public ActionResult Index()
        {
            var getData = _context.Orders.Select(a => new OrderVM
            {
                Id = a.Id,
                Address = a.Address,
                City = a.City,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Phone = a.Phone,
                CustomerId = a.CustomerId,
                PostalCode = a.PostalCode,
                Province = a.Province,
                Total = a.Total,
            }).ToList();
            return View(getData);
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
       
            var getData = _context.Orders.Where(a => a.Id == id).Select(a => new OrderVM
            {
                Id = a.Id,
                Address = a.Address,
                City = a.City,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Phone = a.Phone,
                CustomerId = a.CustomerId,
                PostalCode = a.PostalCode,
                Province = a.Province,
                Total = a.Total,
            }).FirstOrDefault();
            return View(getData);

        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var Orders = _context.Orders.Find(id);
            if (Orders == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var data = _context.Orders.Find(id);
            _context.Orders.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
