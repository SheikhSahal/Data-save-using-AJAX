using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MVCEntities1 db = new MVCEntities1();

            List<Employee> EMP = db.Employees.ToList();

            Employee_ViewModel empvm = new Employee_ViewModel();

            List<Employee_ViewModel> empvmlist = EMP.Select(x => new Employee_ViewModel
            { id = x.id,
              Name = x.Name,
              Address = x.Address,
              Departmentid =x.Departmentid,
              Department_Name = x.Department.department_Name }).ToList();

            ViewBag.show = empvmlist;

            return View();
        }

        public ActionResult Details(int empid)
        {
            MVCEntities1 db = new MVCEntities1();
            Employee emp = db.Employees.SingleOrDefault(x => x.id == empid);

            Employee_ViewModel empvm = new Employee_ViewModel();

            empvm.Name = emp.Name;
            empvm.Department_Name = emp.Department.department_Name;
            empvm.Address = emp.Address;

            return View(empvm);
        }

        public ActionResult DropDown()
        {
            MVCEntities1 db = new MVCEntities1();

             List<Department> list = db.Departments.ToList();

            ViewData["Show"] = new SelectList(list, "id", "Department_Name");


            return View();
        }

        [HttpPost]
        public ActionResult DropDown(Employee_ViewModel emp)
        {
            if(ModelState.IsValid)
            { 
            MVCEntities1 db = new MVCEntities1();

            List<Department> list = db.Departments.ToList();
            ViewData["Show"] = new SelectList(list, "id", "Department_Name");

            Employee empvm = new Employee();

            empvm.Name = emp.Name;
            empvm.Address = emp.Address;
            empvm.Departmentid = emp.Departmentid;

            db.Employees.Add(empvm);
            db.SaveChanges();


            int lastestid = emp.id;

            Site sitevm = new Site();
            sitevm.empid = lastestid;
            sitevm.sitename = emp.Sitename;

            db.Sites.Add(sitevm);
            db.SaveChanges();
            return RedirectToAction("DropDown");
            }
            return View();
        }

    }

}