using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebshopProject.Controllers
{
    public class CustomerSupportController : Controller
    {
        public IActionResult Index() //Ta in ett id för att avgöra vart man börjar?
        {
            return View();
        }

        public IActionResult CustomerSupport(int option)
        {
            //switch (option)
            //{
            //    case 1:
            //        return PartialView("_ContactUsPartial");

            //    case 2:
            //        return PartialView("_ReturnsPartial");

            //    case 3:
            //        Redirect("Account/Register");
            //        break;
            //}
            return View();
        }
    }
}
