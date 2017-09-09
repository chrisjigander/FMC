using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopProject.Models.VM;
using Microsoft.AspNetCore.Http;
using WebshopProject.Utils;

namespace WebshopProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string nrOfArticlesInCart = SessionUtils.GetSessionCount(this);
            if (nrOfArticlesInCart == "-1")
            {
                nrOfArticlesInCart = "0";
            }

            HomeIndexVM indexVM = new HomeIndexVM { NrOfArticlesInCart = Convert.ToInt32(nrOfArticlesInCart) };

            return View(indexVM);
            
        }
        public IActionResult Guide()
        {
            return View();
        }
    }
}
