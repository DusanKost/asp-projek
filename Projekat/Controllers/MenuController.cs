using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Projekat.Models;

namespace Projekat.Controllers
{
    [Authorize(Roles = "ROwner")]
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index(int Id)
        {
            ViewBag.ResId = Id;
            //db.Menus.Where(m => m.RestaurantModels_id.Equals(Id)).ToList()
            return View(db.Menus.Where(m => m.RestaurantModels_id.Equals(Id)).ToList());
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            var menu = new MenuModels();
            using (db = new ApplicationDbContext())
            {
                menu = db.Menus.Find(id);
            }
            return View(menu);
        }

        public ActionResult Create(int restauarnt_id)
        {
            ViewBag.ResId = restauarnt_id;
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(MenuModels model)
        {
            try
            {
                string user_id = User.Identity.GetUserId();
                MenuModels newM = new MenuModels
                {
                    MenuName = model.MenuName,
                    Desc = model.MenuName,
                    ApplicationUserId = User.Identity.GetUserId(),
                    RestaurantModels_id = model.RestaurantModels_id

                };
                db.Menus.Add(newM);
                db.SaveChanges();

                TempData["Message"] = $"Created Menu: {model.MenuName}";

                // TODO: Add insert logic here
                return RedirectToAction("Index", new { Id = model.RestaurantModels_id });
            }
            catch(Exception e)
            {
                return Content(e.InnerException.ToString());
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            var menu = new MenuModels();
            using (db = new ApplicationDbContext())
            {
                menu = db.Menus.Find(id);
            }
            return View(menu);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MenuModels collection)
        {
            try
            {
                // TODO: Add update logic here

                var r = db.Menus.Find(id);
                collection.ApplicationUserId = User.Identity.GetUserId();
                collection.RestaurantModels_id = r.RestaurantModels_id;
                db.Entry(r).CurrentValues.SetValues(collection);
                db.SaveChanges();

                TempData["Message"] = $"You have edited restaurant: {r.MenuName}";

                return RedirectToAction("Index", new { Id = r.RestaurantModels_id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                using (db = new ApplicationDbContext())
                {
                    var some = db.Menus.Find(id);
                    db.Menus.Remove(some);
                    db.SaveChanges();

                    TempData["Message"] = "It Was Deleted";

                    // TODO: Add insert logic here
                    return RedirectToAction("Index", new { Id = some.RestaurantModels_id });
                }
                
            }
            catch
            {
                return View();
            }
        }

    }
}
