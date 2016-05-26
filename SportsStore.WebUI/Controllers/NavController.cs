using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly IProductRepository _productRepo;
        public NavController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _productRepo.Products
                                                .Select(p => p.Category)
                                                .Distinct()
                                                .OrderBy(x => x);
                                            
            return PartialView("_menu", categories);
        }
    }
}