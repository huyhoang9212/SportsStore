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
        private readonly IOrderProcessor _orderProcessor;
        public CartController(IProductRepository repo, IOrderProcessor processor)
        {
            _productRepo = repo;
            _orderProcessor = processor;
        }
        // GET: Cart
        public ActionResult Index(Cart cart, string returnUrl)
        {
            CartIndexViewModel model = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepo.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
               cart.AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnUrl)
        {
            Product product = _productRepo.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
               cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Fucking you, your cart is empty, why the shit you checkouted!");
            }
            if(ModelState.IsValid){
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            return View(shippingDetails);
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