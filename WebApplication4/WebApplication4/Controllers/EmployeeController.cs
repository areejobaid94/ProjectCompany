using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class EmployeeController : Controller
    {
        private Helpers helpers;
        // GET: Employee
        public EmployeeController()
        {
            helpers = new Helpers();
        }

        public ActionResult Index()
        {
            using (CompanyDB dBModels = new CompanyDB())
            {
                return View(dBModels.Employees.ToList());
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    var employee = dBModels.Employees.Where(model => model.ID == id).FirstOrDefault();
                    employee.Password = helpers.DecodeFrom64(employee.Password);
                    return View(employee);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Exception raise = ex;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            using (CompanyDB dBModels = new CompanyDB())
            {
                AllModels mymodel = new AllModels();
                mymodel.Departments = dBModels.Departments.ToList();
                if (mymodel.Departments.Count() == 0)
                {
                    var department = new Department();
                    department.Date_Time_UTC = DateTime.Now;
                    department.Update_Date_Time_UTC = DateTime.Now;
                    department.Server_Date_Time = DateTime.Now;
                    department.Name = "Default";
                    dBModels.Departments.Add(department);
                    dBModels.SaveChanges();
                }
                mymodel.Employee = new Employee();
                mymodel.Departments = dBModels.Departments.ToList();
                return View(mymodel);
            }
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee Employee)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    // TODO: Add insert logic here
                    Employee.Date_Time_UTC = DateTime.Now;
                    Employee.Password = Helpers.EncodePasswordToBase64(Employee.Password);
                    dBModels.Employees.Add(Employee);
                    dBModels.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Exception raise = ex;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    AllModels mymodel = new AllModels();
                    mymodel.Employee = dBModels.Employees.Where(model => model.ID == id).FirstOrDefault();
                    mymodel.Employee.Password = helpers.DecodeFrom64(mymodel.Employee.Password);
                    mymodel.Departments = dBModels.Departments.ToList();
                    if (mymodel.Departments.Count() == 0)
                    {
                        var department = new Department();
                        department.Date_Time_UTC = DateTime.Now;
                        department.Update_Date_Time_UTC = DateTime.Now;
                        department.Server_Date_Time = DateTime.Now;
                        department.Name = "Default";
                        dBModels.Departments.Add(department);
                        dBModels.SaveChanges();
                    }
                    mymodel.Departments = dBModels.Departments.ToList();
                    return View(mymodel);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Exception raise = ex;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    employee.Update_Date_Time_UTC = DateTime.Now;
                    employee.Password = Helpers.EncodePasswordToBase64(employee.Password);
                    dBModels.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    dBModels.SaveChanges();
                }               
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Exception raise = ex;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (CompanyDB dBModels = new CompanyDB())
            {
                return View(dBModels.Employees.Where(model => model.ID == id).FirstOrDefault());
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    var employee = dBModels.Employees.Where(model => model.ID == id).FirstOrDefault();
                    dBModels.Employees.Remove(employee);
                    dBModels.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Exception raise = ex;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
    }
}
