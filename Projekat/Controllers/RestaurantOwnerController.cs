using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat.Controllers
{
    [Authorize(Roles = "ROwner")]
    public class RestaurantOwnerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RestaurantOwner
        public ActionResult Index()
        {
            string user_ID = User.Identity.GetUserId();
            var list = db.Restaurants.Where(r => r.ApplicationUserId == user_ID).ToList();
            return View(list);
        }

        // GET: RestaurantOwner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantOwner/Create
        [HttpPost]
        public ActionResult Create(RestaurantModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RestaurantModels r = new RestaurantModels { Name = model.Name, Desc = model.Desc, ApplicationUserId = User.Identity.GetUserId() };
                    db.Restaurants.Add(r);
                    db.SaveChanges();

                    TempData["Message"] = $"You have created new restaurant: {model.Name}";

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: RestaurantOwner/Edit/5
        public ActionResult Edit(int id)
        {
            var r = db.Restaurants.Find(id);
            return View(r);
        }

        // POST: RestaurantOwner/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RestaurantModels collection)
        {
            try
            {
                // TODO: Add update logic here
                var r = db.Restaurants.Find(id);
                collection.ApplicationUserId = User.Identity.GetUserId();
                db.Entry(r).CurrentValues.SetValues(collection);
                db.SaveChanges();

                TempData["Message"] = $"You have edited restaurant: {r.Name}";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            return View(db.Restaurants.Find(id));
        }

        // GET: RestaurantOwner/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var res = db.Restaurants.Find(id);
                db.Restaurants.Remove(res);
                db.SaveChanges();

                TempData["Message"] = $"You have deleted restaurant: {res.Name}";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
