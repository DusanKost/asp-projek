using Microsoft.AspNet.Identity.EntityFramework;
using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            var users = db.Users.Where(u => u.UserName != "").ToArray();
            var viewUsers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                viewUsers.Add(user);
            }
            return View(viewUsers);
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            return View(db.Users.Find(id));
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateRole(string role)
        {
            db.Roles.Add(new IdentityRole { Name = role});
            db.SaveChanges();
            TempData["Message"] = $"You have created a new role: {role}";
            return RedirectToAction("Index");
        }

        // POST: Admin/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                var customer = db.Users.Where(u => u.Id == id).First();
                db.Users.Remove(customer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return  Content(e.Message);
            }
        }
    }
}
