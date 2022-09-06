using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Interfaces;
using WebApplication1.Models.VM;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly IUnitOfWork<Owner> owner;
        private readonly IUnitOfWork<PortfolioItem> portfolio;

        public HomeController(IUnitOfWork<Owner> owner ,IUnitOfWork<PortfolioItem> Portfolio)
        {
            this.owner = owner;
            portfolio = Portfolio;
        }

        public IActionResult Index()
        {
            var vm = new HomeVM
            {
                 Owner= owner.Entity.GetAll().First() ,
                  PortfolioItems = portfolio.Entity.GetAll().ToList()
            };
            return View(vm);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
