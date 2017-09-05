using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebshopProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult GetDropDownMenu()
        {
            return PartialView("_DropDownMenuPartial");
        }
    }
}
