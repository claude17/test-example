using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Context;
using WebApplication8.ViewModel;


namespace WebApplication8.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        userEntities1 database;

        public StudentController()
        {

            database = new userEntities1();


        }


        public ActionResult Index()
        {
            var students = database.students.ToList();
            return View(students);
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            //var departments = database.departments.ToList();
            //ViewBag.departments = new SelectList(departments, "DepartmentId", "Departmentname");
            return View();

        }

        [HttpPost]
        public ActionResult AddStudent(student s)
        {
            if (ModelState.IsValid)
            {
                database.students.Add(s);
                database.SaveChanges();
                TempData["Msg"] = "Student added successfully";
                return RedirectToAction("Index");
            }

            //var departments = database.departments.ToList();
            //ViewBag.departments = new SelectList(departments, "DepartmentId", "Departmentname");
            return View();
        }

        //[HttpGet]
        //public ActionResult AddDepartment()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddDepartment(department d)
        //{
        //    //if (database.departments.Any(x => x.Departmentname == d.Departmentname))
        //    //{
        //    //    ModelState.Clear();
        //    //    return View();
        //    //}
        //    ////if (ModelState.IsValid)
        //    ////{
        //    ////    database.departments.Add(d);
        //    ////    database.SaveChanges();
        //    ////    TempData["Msg"] = "Department added successfully";
        //    ////    return RedirectToAction("Index");
        //    ////}
        //    //database.departments.Add(d);
        //    //database.SaveChanges();
        //    //TempData["Msg"] = "Department added successfully";
        //    if (database.departments.Any(x => x.Departmentname == d.Departmentname))
        //    {
        //        ModelState.Clear();
        //        return View();
        //    }
           
        //    database.departments.Add(d);
        //    database.SaveChanges();
        //    return View();
        //}
        public ActionResult AddDepartment()
        {
            var model = new DepartmentViewModel
            {
                NewDepartment = new department(),
                Departments = database.departments.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDepartment(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (database.departments.Any(x => x.Departmentname == model.NewDepartment.Departmentname))
                {
                    ModelState.Clear();
                    model.Departments = database.departments.ToList();
                    return View(model);
                }
                database.departments.Add(model.NewDepartment);
                database.SaveChanges();
                TempData["Msg"] = "Department added successfully";
                model.Departments = database.departments.ToList();
                return View(model);
                
            }

            model.Departments = database.departments.ToList();
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var data = database.students.Where(x => x.Id == id).First();
            database.students.Remove(data);
            database.SaveChanges();
            //TempData["MsgRem"] = "Employee information removed successfully";

            return RedirectToAction("Index");
        }

    }
}