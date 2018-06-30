using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projekat.Models;

namespace Projekat.Controllers
{
    [Authorize]
    public class StoreContentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: StoreContent
        public ActionResult Index()
        {
            return View(db.Restaurants.Where(r => r.Id != 0).ToList());
        }

        public ActionResult AllMenus(int resId)
        {
            return View(db.Menus.Where(m => m.RestaurantModels_id == resId).ToList());
        }

        public ActionResult AllMenuItems(int menuId)
        {
            return View(db.MenuItems.Where(mit => mit.MenuModels_id == menuId).ToList());
        }

        public ActionResult AllTransactions()
        {
            string user_id = User.Identity.GetUserId();

            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            //deserializedUser = ser.ReadObject(ms) as MenuItemModels;
            //db.Transactions.Where(tr => tr.ApplicationUserId == user_id).ToList();

            return View(db.Transactions.Where(tr => tr.ApplicationUserId == user_id).ToList());
        }

        public ActionResult Pay()
        {
            List<MenuItemModels> x = (List<MenuItemModels>)Session[User.Identity.GetUserId()];

            var json = new JsonResult() { Data = x, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            var y = JsonConvert.SerializeObject(json.Data);
            var trans = new Transactions { ApplicationUserId = User.Identity.GetUserId(), Cart = y };
            db.Transactions.Add(trans);
            db.SaveChanges();

            x.Clear();
            Session[User.Identity.GetUserId()] = x;

            TempData["Message"] = "You have bought item thanks!";
            return RedirectToAction("Cart");
        }

        public ActionResult RemoveFromCart(int id)
        {
            List<MenuItemModels> x = (List<MenuItemModels>)Session[User.Identity.GetUserId()];
            var item = db.MenuItems.Find(id);
            x.RemoveAt(0);
            Session[User.Identity.GetUserId()] = x;
            TempData["Message"] = "You have removed item";
            return RedirectToAction("Cart");
        }

        public ActionResult Buy(int itemId)
        {
            MenuItemModels item = db.MenuItems.Find(itemId);
            List<MenuItemModels> x = (List<MenuItemModels>)Session[User.Identity.GetUserId()];
            x.Add(item);
            Session[User.Identity.GetUserId()] = x;
            TempData["Message"] = $"You added item: {item.Name} to cart";
            return RedirectToAction("AllMenuItems", new { menuId = item.MenuModels_id });
            //return View("AllMenuItems", db.MenuItems.Where(mit => mit.MenuModels_id == item.MenuModels_id).ToList());
        }

        public ActionResult Cart()
        {
            List<MenuItemModels> list = new List<MenuItemModels>();
            list = (List<MenuItemModels>)Session[User.Identity.GetUserId()];
            if (list != null)
            {
                return View(list.ToList());
            }
            TempData["Message"] = "You must add items to cart";
            return RedirectToAction("Index","Home");
        }
    }
}