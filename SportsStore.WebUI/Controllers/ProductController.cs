using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 2;
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = repository.Products;
            if (!string.IsNullOrEmpty(category))
            {
                products =  products.Where(p => p.Category == null || p.Category == category);
            }

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products.OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count()
                },
                CurrentCategory = category
            };


            return View(model);
        }
    }
}