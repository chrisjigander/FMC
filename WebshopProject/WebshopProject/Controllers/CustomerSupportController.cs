using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopProject.Models.VM;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebshopProject.Controllers
{
    public class CustomerSupportController : Controller
    {
        public IActionResult Index(int id) //Ta in ett id för att avgöra vart man börjar?
        {
            return View(new CustomerSupportIndexVM { PageId = id });
        }

        public IActionResult HandleContactForm()
        {
            return Redirect("/Home/Index/");
        }

        public IActionResult CustomerSupport(int id)
        {
            switch (id)
            {
                case 1:
                    return PartialView("_ContactUsPartial");

                case 2:
                    return PartialView("_AboutUsPartial");

                case 3:
                    return PartialView("_ReturnAndExchangePartial");
                    
                case 4:
                    return PartialView("_DeliveryPartial");
                    
                case 5:
                    return PartialView("_TermsAndAgreementsPartial");
                    
                default:
                    return PartialView("_ContactUsPartial");
                    
            }
            
        }
    }
}


