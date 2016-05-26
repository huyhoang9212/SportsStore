using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepo;
        public CartController(IProductRepository repo)
        {
            _productRepo = repo;
        }
        // GET: Cart
        public ActionResult Index(string returnUrl)
        {
            CartIndexViewModel model = new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = _productRepo.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
                GetCart().AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _productRepo.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}