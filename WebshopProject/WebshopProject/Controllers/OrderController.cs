﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopProject.Models;
using Microsoft.AspNetCore.Http;
using WebshopProject.Utils;

namespace WebshopProject.Controllers
{
    public class OrderController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
    }
}