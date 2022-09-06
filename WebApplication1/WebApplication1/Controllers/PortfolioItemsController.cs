using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Interfaces;
using WebApplication1.Models.VM;

namespace WebApplication1.Controllers
{
    public class PortfolioItemsController : Controller
    {
     



        private readonly MyDbContext _context;
        private readonly IUnitOfWork<PortfolioItem> portfolio;
        private readonly IHostingEnvironment hosting;

        public PortfolioItemsController(MyDbContext context,
            IUnitOfWork<PortfolioItem> portfolio, IHostingEnvironment hosting)
        {
            _context = context;
            this.portfolio = portfolio;
            this.hosting = hosting;
        }

        // GET: PortfolioItems
        public async Task<IActionResult> Index()
        {
            return View( portfolio.Entity.GetAll().ToList()  );
        }

        // GET: PortfolioItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem =  portfolio.Entity.GetById(id);

            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        
        public IActionResult Create()
        {
            return View();
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioVM  model )
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                PortfolioItem portfolioItem = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImgURL = model.File.FileName
                };

                portfolio.Entity.Insert(portfolioItem);
                portfolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem =  portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioVM portfolioViewModel = new PortfolioVM
            {
                Id = portfolioItem.Id,
                Description = portfolioItem.Description,
                ImgURL = portfolioItem.ImgURL,
                ProjectName = portfolioItem.ProjectName
            };


            return View(portfolioViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PortfolioVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    PortfolioItem portfolioItem = new PortfolioItem
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImgURL = model.File.FileName
                    };

                    portfolio.Entity.Update(portfolioItem);
                    portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

       

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }


        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
             portfolio.Entity.Delete(id);
             portfolio.Save();
            
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _context.portfolioItems.Any(e => e.Id == id);
        }


    }
}
