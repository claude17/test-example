using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppCRUDEF.Context;

namespace WebAppCRUDEF.Controllers
{
    public class UserController : Controller
    {
        AspBDbEntitiesContext _dbContext;

        public UserController()
        {
            this._dbContext = new AspBDbEntitiesContext();
        }

        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            ModelState.Remove("RePassword");
            if(ModelState.IsValid)
            {
                var data = _dbContext.Users.Where(x => x.Username == u.Username && x.Password == u.Password).FirstOrDefault();
                if(data != null)
                {
                    Session["id"] = data.Id;
                    Session["username"] = data.Username;
                    Session["type"] = data.Type;
                    return RedirectToAction("Index", "Employee");
                }
                ViewBag.Invalid = "Invalid User";
                ModelState.Clear();
                return View();
            }

            ModelState.Clear();
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            TempData["Logout"] = "Logged out from the system";

            return View("Login");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User u)
        {
            if(_dbContext.Users.Any(x => x.Username == u.Username))
            {
                ModelState.Clear();
                return View();
            }

            _dbContext.Users.Add(u);
            _dbContext.SaveChanges();

            return View("Login");
        }

        public ViewResult ClearLogin()
        {
            ModelState.Clear();
            return View("Login");
        }

        public ViewResult ClearSignUp()
        {
            ModelState.Clear();
            return View("SignUp");
        }
    }
}