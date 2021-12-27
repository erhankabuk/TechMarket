using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public static int amount = 0;
        [HttpPost]
        public IActionResult AddItem(int productId)
        {
            return Json(++amount);
        }
    }
}
