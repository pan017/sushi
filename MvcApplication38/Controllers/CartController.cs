using MvcApplication37.Models;
using MvcApplication38.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication38.Controllers
{

    public class CartController : Controller
    {
        public CartController()
            : base()
        {
            DataBaseContext db = new DataBaseContext();
            List<Sushi> _productList = new List<Sushi>();
            _productList = db.Sushis.ToList();
           
        }
        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        //
        // GET: /Cart/
        private IProductsRepository repository;
        public CartController(IProductsRepository repo)
        {
            repository = repo;

        }
        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Order(Client model)
        {
            if (model == null)
            {
                ModelState.AddModelError("Error", "Ошибка! Заполните все поля");
                return View(model);
            }
            if (String.IsNullOrEmpty(model.Adres) || String.IsNullOrEmpty(model.Name) || String.IsNullOrEmpty(model.Phone))
            {
                ModelState.AddModelError("Error", "Ошибка! Заполните все поля");
                return View(model);
            }
            DataBaseContext db = new DataBaseContext();
            db.Clients.Add(model);
            db.SaveChanges();
            Order _sale = new Order();
            _sale.Client = db.Clients.Local[db.Clients.Local.Count - 1];
            _sale.Date = DateTime.Now;
            Cart _cart = GetCart();
            List<Sushi> _productList = new List<Sushi>();
            _productList = db.Sushis.ToList();

            for (int i = 0; i < _cart.lineCollection.Count; i++)
            {
                for (int j = 0; j < _cart.lineCollection[i].Quantity; j++)
                {
                    int id = _cart.lineCollection[i].Product.SushiId;
                    Sushi product = db.Sushis
               .FirstOrDefault(g => g.SushiId == id);
                    _sale.Product = product;
                    db.Orders.Add(_sale);
                    db.SaveChanges();

                }
            }
            double Price = 0;
            ViewOrderModel _newOrder = new ViewOrderModel();
            _newOrder.Name = model.Name;
            _newOrder.State = false;
            for (int i = 0; i < _cart.lineCollection.Count; i++)
            {
                int id = _cart.lineCollection[i].Product.SushiId;
                Sushi product = db.Sushis
           .FirstOrDefault(g => g.SushiId == id);
                    
                _sale.Product = product;
                
                _newOrder.Order += product.Name + " ";
                _newOrder.Order += _cart.lineCollection[i].Quantity + " шт.";
                _newOrder.Order += "; ";
                Price += _cart.lineCollection[i].Product.Price * _cart.lineCollection[i].Quantity;

            }
            _newOrder.Adres = _sale.Client.City + ", " + _sale.Client.Adres;
            _newOrder.Cost = Price;
            _newOrder.Phone = model.Phone;
            _newOrder.Date = DateTime.Now;
            db.ViewOrders.Add(_newOrder);
            
            db.SaveChanges();
            GetCart().Clear();
            return RedirectToAction("ThankYou", "Cart");
        }
        public ActionResult GetCartCount()
        {
            DataBaseContext db = new DataBaseContext();
            Cart _cart = GetCart();

            int CartCount = 0;
            for (int i = 0; i < _cart.lineCollection.Count; i++)
            {
                for (int j = 0; j < _cart.lineCollection[i].Quantity; j++)
                {
                    CartCount++;
                }
            }
            ViewBag.CartCount = CartCount;
            return View();
        }

       
       public ActionResult ThankYou()
        {
            return View();
        }
       
        public ActionResult AddToCart(int productId, string returnUrl)
        {
            DataBaseContext db = new DataBaseContext();
            Sushi product = db.Sushis
                .FirstOrDefault(g => g.SushiId == productId);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            if (returnUrl == "/")
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("ViewItem", "Home", new { id = productId });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Sushi product = repository.Products
                .FirstOrDefault(g => g.SushiId == productId);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary()
        {
            Cart cart = GetCart();
            return PartialView(cart);
        }
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Car"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Car"] = cart;
            }
            return cart;
        }
    }
    public interface IProductsRepository
    {
        IEnumerable<Sushi> Products { get; }
    }
    
}
