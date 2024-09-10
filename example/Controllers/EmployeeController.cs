using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppCRUDEF.Context;

namespace WebAppCRUDEF.Controllers
{
    public class EmployeeController : Controller
    {
        AspBDbEntitiesContext _dbContext;

        public EmployeeController()
        {
            _dbContext = new AspBDbEntitiesContext();
        }

        // GET: Employee
        public ActionResult Index()
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "User");

            var empList = _dbContext.Employees.ToList();

            if (empList == null)
                return HttpNotFound();

            return View(empList);
        }

        [HttpGet]
        public ActionResult AddEmployee(Employee e)
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "User");

            if (Session["type"].ToString().Equals("member"))
                return RedirectToAction("Index", "Employee");

            if (e.Id > 0)
                return View(e);
            else
            {
                ModelState.Clear();
                ViewBag.NoData = 0;
                return View();
            }
                
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee emp)
        {
            ModelState.Remove("Id");
            if(ModelState.IsValid)
            {
                if(emp.Id <=0)
                {
                    _dbContext.Employees.Add(emp);
                    _dbContext.SaveChanges();
                    TempData["MsgAdd"] = "Employee information added successfully";
                }
                else
                {
                    _dbContext.Entry(emp).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    TempData["MsgMod"] = "Employee information modified successfully";
                }

                return RedirectToAction("Index");
                
            }

            return View("AddEmployee");

        }

        public ActionResult Delete(int id)
        {
            var data = _dbContext.Employees.Where(x => x.Id == id).First();
            _dbContext.Employees.Remove(data);
            _dbContext.SaveChanges();
            TempData["MsgRem"] = "Employee information removed successfully";

            return RedirectToAction("Index");
        }
    }
}