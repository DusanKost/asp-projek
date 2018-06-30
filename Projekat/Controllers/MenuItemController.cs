using Microsoft.AspNet.Identity;
using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat.Controllers
{
    [Authorize(Roles = "ROwner")]
    public class MenuItemController : Controller
    {
        private ApplicationDbContext db;
        // GET: MenuItem
        public ActionResult Index(int Id)
        {
            using (db = new ApplicationDbContext())
            {
                ViewBag.MenuId = Id;
                return View(db.MenuItems.Where(m => m.MenuModels_id.Equals(Id)).ToList());
            }
        }

        // GET: MenuItem/Details/5
        public ActionResult Details(int id)
        {
            var menu = new MenuItemModels();
            using (db = new ApplicationDbContext())
            {
                menu = db.MenuItems.Find(id);
            }
            return View(menu);
        }

        // GET: MenuItem/Create
        public ActionResult Create(int menu_id)
        {
            ViewBag.MenuId = menu_id;
            return View();
        }

        // POST: MenuItem/Create
        [HttpPost]
        public ActionResult Create(MenuItemModels collection)
        {
            try
            {
                // TODO: Add insert logic here

                using (db = new ApplicationDbContext())
                {
                    MenuItemModels menu = new MenuItemModels { Name = collection.Name,
                        Desc = collection.Desc, Price = collection.Price, MenuModels_id = collection.MenuModels_id };
                    menu.ApplicationUserId = User.Identity.GetUserId();
                    menu.MenuModels_id = collection.MenuModels_id;
                    menu.RestaurantModels_id = db.Menus.Find(collection.MenuModels_id).RestaurantModels_id;
                    db.MenuItems.Add(menu);
                    db.SaveChanges();

                    TempData["Message"] = $"You have created a menu item: {menu.Name}";

                    return RedirectToAction("Index", new { Id = collection.MenuModels_id });
                }
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        // GET: MenuItem/Edit/5
        public ActionResult Edit(int id)
        {
            var menu = new MenuItemModels();
            using (db = new ApplicationDbContext())
            {
                menu = db.MenuItems.Find(id);
            }
            return View(menu);
        }

        // POST: MenuItem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MenuItemModels collection)
        {
            try
            {
                // TODO: Add update logic here

                using (db = new ApplicationDbContext())
                {
                    var r = db.MenuItems.Find(id);
                    collection.ApplicationUserId = User.Identity.GetUserId();
                    collection.MenuModels_id = r.MenuModels_id;
                    collection.RestaurantModels_id = r.RestaurantModels_id;
                    db.Entry(r).CurrentValues.SetValues(collection);
                    db.SaveChanges();

                    TempData["Message"] = $"You have edited restaurant: {r.Name}";

                    return RedirectToAction("Index", new { Id = collection.MenuModels_id });
                }
            }
            catch(Exception e)
            {
                return Content(e.ToString());
            }
        }

        // GET: MenuItem/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                using (db = new ApplicationDbContext())
                {
                    var some = db.MenuItems.Find(id);
                    db.MenuItems.Remove(some);
                    db.SaveChanges();

                    TempData["Message"] = "It Was Deleted";

                    // TODO: Add insert logic here
                    return RedirectToAction("Index", new { Id = some.MenuModels_id });
                }
               
            }
            catch
            {
                return View();
            }
        }

    }
}
