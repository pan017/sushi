using MvcApplication37.Models;
using MvcApplication38.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApplication38.Controllers
{
    [Authorize]
    public class AdminMenuController : Controller
    {
        //
        // GET: /AdminMenu/
        public ActionResult ProductList()
        {
            DataBaseContext db = new DataBaseContext();
            List<Sushi> model = db.Sushis.ToList();
            return View(model);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeleteItem(int id)
        {
            DataBaseContext db = new DataBaseContext();
            List<Order> _salesList = new List<Order>();
            _salesList = db.Orders.ToList();
            for (int i = 0; i < _salesList.Count; i++)
            {
                var delSale = db.Orders
                    .Where(q => q.OrderId == id)
                    .FirstOrDefault();
                if (delSale != null)
                {
                    db.Orders.Remove(delSale);
                    db.SaveChanges();
                }
            }

            var EditProduct = db.Sushis
            .Where(c => c.SushiId == id)
            .FirstOrDefault();
            if (EditProduct != null)
                db.Sushis.Remove(EditProduct);
            db.SaveChanges();
            ViewBag.ProductList = db.Sushis.ToList();
            return RedirectToAction("ProductList");
        }
        private bool ValidateUser(string Login, string Password)
        {
            bool isValid = false;

            using (DataBaseContext _db = new DataBaseContext())
            {
                try
                {
                    var FindUser = _db.Admins
                    .Where(c => c.Login == Login)
                    .FirstOrDefault();

                    if (FindUser != null)
                    {
                        if (FindUser.Password == Password)
                            isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        public ActionResult AddNewItem()
        {
            List<string> _tempList = new List<string>();
            _tempList.Add("СушиРрито");
            _tempList.Add("Нигири");
            _tempList.Add("Маки");
            _tempList.Add("Гунканы");
            SelectList _categoryList = new SelectList(_tempList, 0);
            ViewBag._categoryList = _categoryList;
            return View();
        }
        public ActionResult StaffList ()
        {
            DataBaseContext db = new DataBaseContext();
            List<Staff> model = new List<Staff>();
            model = db.Staffs.ToList();
            return View(model);
        }
        public ActionResult NewStaff()
        {
            List<string> _type = new List<string>();
            _type.Add("Курьер пеший");
            _type.Add("Курьер на собственном авто");
            _type.Add("Курьер на автомобиле компании");
            SelectList type = new SelectList(_type, 0);
            ViewBag._type = type;
            return View();
        }
        public ActionResult Proceeds()
        {

            DataBaseContext db = new DataBaseContext();
            List<ViewOrderModel> _orders = db.ViewOrders.ToList();
            ProceedsModel model = new ProceedsModel();
            model.EndDay = model.BeginDay = DateTime.Now;
            model.Money = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult Proceeds(ProceedsModel model)

        {
            double Total = 0;
            if (ModelState.IsValid)
            {
                DataBaseContext db = new DataBaseContext();
                List<ViewOrderModel> _orders = db.ViewOrders.ToList();
                 DateTime d1 = model.BeginDay;
                 DateTime d2 = model.EndDay;
                 TimeSpan time1 = d1 - DateTime.Now;
                 TimeSpan time2 = DateTime.Now - d2;
                 int a1 = d1.DayOfYear;
                 int x = DateTime.Now.DayOfYear;
                 int a2 = d2.DayOfYear;
                 for (int i = 0; i < _orders.Count; i++)
                 {
                     if (d1.DayOfYear <= _orders[i].Date.DayOfYear && _orders[i].Date.DayOfYear <= d2.DayOfYear)
                         Total += _orders[i].Cost;

                     
                 }
                 model.Money = Total;
                return View(model);
            }
            else
            {
                model.Money = Total;
                ModelState.AddModelError("Error", "Введите корректные данные");
                model.EndDay = model.BeginDay = DateTime.Now;
                model.Money = 0;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult NewStaff(Staff model)
        {
            DataBaseContext db = new DataBaseContext();
            db.Staffs.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditStaff(int id)
        {
            DataBaseContext db = new DataBaseContext();
            Staff model = db.Staffs
                .Where(m => m.Id == id)
                .FirstOrDefault();
            List<string> _type = new List<string>();
            _type.Add("Курьер пеший");
            _type.Add("Курьер на собственном авто");
            _type.Add("Курьер на автомобиле компании");
            SelectList type = new SelectList(_type, 0);
            for (int i = 0; i < _type.Count; i++)
            {
                if (_type[i] == model.Type)
                {
                    type = new SelectList(_type, i);
                }
            }
            ViewBag._type = type;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditStaff(Staff model)
        {
            DataBaseContext db = new DataBaseContext();
            var EditStaff = db.Staffs
                .Where(m => m.Id == model.Id)
                .FirstOrDefault();
            EditStaff.Name = model.Name;
            EditStaff.Phone = model.Phone;
            EditStaff.Type = model.Type;
            EditStaff.Adres = model.Adres;
            EditStaff.Dog = model.Dog;
            db.SaveChanges();
            return RedirectToAction("StaffList");
        }
        public ActionResult DelStaff(int id)
        {
            DataBaseContext db = new DataBaseContext();
            Staff model = db.Staffs
                .Where(m => m.Id == id)
                .FirstOrDefault();
            for (int i = 0; i < db.Orders.Count(); i++)
            {
                var delOrder = db.Orders
                    .Where(m => m.Staff.Id == id)
                    .FirstOrDefault();
                db.Orders.Remove(delOrder);
                db.SaveChanges();
            }
            db.Staffs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("StaffList");
        }
        public ActionResult EditItem(int id)
        {
            DataBaseContext db = new DataBaseContext();

            Sushi model = new Sushi();
            model = db.Sushis
                .Where(m => m.SushiId == id)
                .FirstOrDefault();
            List<string> _tempList = new List<string>();
            _tempList.Add("СушиРрито");
            _tempList.Add("Нигири");
            _tempList.Add("Маки");
            _tempList.Add("Гунканы");
            SelectList _categoryList = new SelectList(_tempList, 0);
            for (int i = 0; i < _tempList.Count; i++)
            {
                if (model.Type == _tempList[i])
                    _categoryList = new SelectList(_tempList, i);
            }
            
            ViewBag._categoryList = _categoryList;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditItem(Sushi model, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                string Extention = System.IO.Path.GetExtension(upload.FileName);
                string NewFileName = Crypto.HashPassword(fileName);
                NewFileName = NewFileName.Remove(0, 20);
                NewFileName += Extention;
                NewFileName = NewFileName.Replace('/', 'w');
                NewFileName = NewFileName.Replace('\\', 'a');
                NewFileName = NewFileName.Replace('+', 't');
                upload.SaveAs(Server.MapPath("~/Photos/" + NewFileName));
                model.Photo = "/Photos/" + NewFileName;
            }
            DataBaseContext db = new DataBaseContext();
            var EditProduct = db.Sushis
                .Where(m => m.SushiId == model.SushiId)
                .FirstOrDefault();
            EditProduct.Name = model.Name;
            EditProduct.Price = model.Price;
            EditProduct.Type = model.Type;
            EditProduct.Colorie = model.Colorie;
            EditProduct.Composition = model.Composition;
            if (!String.IsNullOrEmpty(model.Photo))
                EditProduct.Photo = model.Photo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NewOrders()
        {
            DataBaseContext db = new DataBaseContext();


            List<ViewOrderModel> _orderList = new List<ViewOrderModel>();
            _orderList = db.ViewOrders.ToList();
            List<ViewOrderModel> model = new List<ViewOrderModel>();
            for (int i = 0; i < _orderList.Count; i++)
            {
                if (_orderList[i].State == false)
                {
                    model.Add(_orderList[i]);
                }
            }
            return View(model);
        }
        public ActionResult ViewOrder (int OrderId)
        {
            DataBaseContext db = new DataBaseContext();
            List<Staff> _staffList = db.Staffs.ToList();
            List<string> NewStaffList = new List<string>();
            for (int i = 0; i < _staffList.Count; i++)
            {
                NewStaffList.Add(_staffList[i].Name);
            }
            SelectList staffList = new SelectList(NewStaffList, 0);
            var model = db.ViewOrders
                .Where(m => m.Id == OrderId)
                .FirstOrDefault();
            ViewBag.staffList = staffList;
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewOrder(ViewOrderModel model)
        {
            DataBaseContext db = new DataBaseContext();
            List<Staff> _staffList = db.Staffs.ToList();
            SelectList staffList = new SelectList(_staffList, 0);
            var Edit = db.ViewOrders
                .Where(m => m.Id == model.Id)
                .FirstOrDefault();
            Edit.State = true;
            //Edit.Staff = model.Staff;
            Edit.Staff = model.Staff;
            db.SaveChanges();
            //ViewBag.staffList = staffList;
            return RedirectToAction("NewOrders");
        }
        public ActionResult OldOrders()
        {
            DataBaseContext db = new DataBaseContext();


            List<ViewOrderModel> _orderList = new List<ViewOrderModel>();
            _orderList = db.ViewOrders.ToList();
            List<ViewOrderModel> model = new List<ViewOrderModel>();
            for (int i = 0; i < _orderList.Count; i++)
            {
                if (_orderList[i].State == true)
                {
                    model.Add(_orderList[i]);
                }
            }
            return View(model);

        }

        public ActionResult Menu()
        {
            return View();
        }
        [AllowAnonymous]
            public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (String.IsNullOrEmpty(model.Login) || String.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("ErrorLogin", "Заполните все поля");
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    string s = Crypto.Hash(model.Password);
                    if (ValidateUser(model.Login, Crypto.Hash(model.Password)))
                    {
                        //WebSecurity.Login(model.Login, );

                        //set auth cookie
                        FormsAuthentication.SetAuthCookie(model.Login, false);
                        return RedirectToAction("Menu");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Неправильный пароль или логин");
                        return RedirectToAction("Index", "Home");
                    }

                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            //if (Model.Login == null || Model.Password == null)
            //    return RedirectToAction("Index", "Home");
            //bool q;
            //string s = Crypto.SHA256(Model.Password);
            //if (s == "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92")
            //{
            //    DataBaseContext db = new DataBaseContext();
            //    List<Product> ProductList = new List<Product>();
            //    ProductList = db.Products.ToList();
            //    ViewBag.ProductList = ProductList;
            //    return View();
            //}
            //else

        }
        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult Clients()
        {
            DataBaseContext db = new DataBaseContext();
            List<Client> model = new List<Client>();
            model = db.Clients.ToList();
            List<Client> _model = new List<Client>();
            for (int i = 0; i < model.Count; i++)
            {
                bool q = true;
                for (int j = 0; j < _model.Count; j++)
                {
                    if (model[i].Name == _model[j].Name && model[i].Phone == _model[j].Phone)
                    q = false;
                }
                if (q)
                    _model.Add(model[i]);
            }
            return View(_model);
        }
    [HttpPost]
        public ActionResult AddNewItem(Sushi model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    string Extention = System.IO.Path.GetExtension(upload.FileName);
                    string NewFileName = Crypto.HashPassword(fileName);
                    NewFileName = NewFileName.Remove(0, 20);
                    NewFileName += Extention;
                    NewFileName = NewFileName.Replace('/', 'w');
                    NewFileName = NewFileName.Replace('\\', 'a');
                    NewFileName = NewFileName.Replace('+', 't');
                    upload.SaveAs(Server.MapPath("~/Photos/" + NewFileName));
                    model.Photo = "/Photos/" + NewFileName;
                }
                DataBaseContext db = new DataBaseContext();
                db.Sushis.Add(model);
                db.SaveChanges();
                ModelState.AddModelError("add", "Товар добавлен");
                return RedirectToAction("AddNewItem", "AdminMenu");

            }
            else
            {
                ModelState.AddModelError("add", "Ошбика ввода данных");
                return RedirectToAction("AddProduct", "AdminMenu");
            }
            return View();
        }
    }
}
