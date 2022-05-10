using Electo.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Electo.Web.Controllers
{
    public class OrdersController : Controller
    {
        // GET: OrdersController
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            var order = _context.Orders.Include(c => c.OrderDetails)
                .FirstOrDefaultAsync(a => a.Id == id);
            return View(order);
        }
       
    }
}
