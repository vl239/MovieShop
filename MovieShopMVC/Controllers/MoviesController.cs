﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        // showing details of the movie
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}

