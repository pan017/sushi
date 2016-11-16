using MvcApplication37.Models;
using MvcApplication38.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication38.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();
            db.Database.Initialize(false);
            List<Sushi> model = new List<Sushi>();
            model = db.Sushis.ToList();
            return View(model);
        }
        public ActionResult Filter (string Query)
        {
            DataBaseContext db = new DataBaseContext();
            List<Sushi> model = new List<Sushi>();
            List<Sushi> _tempList = new List<Sushi>();
            _tempList = db.Sushis.ToList();
            for (int i = 0; i < _tempList.Count; i++)
            {
                if (_tempList[i].Type == Query)
                    model.Add(_tempList[i]);
            }
            return View("Index", model);
        }
    }
}
