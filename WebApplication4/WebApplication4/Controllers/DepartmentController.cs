using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            using (CompanyDB dBModels = new CompanyDB())
            {
                return View(dBModels.Departments.ToList());
            }
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    var departments = dBModels.Departments.Where(model => model.ID == id).FirstOrDefault();
                    return View(departments);
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

        // GET: Department/Create
        public ActionResult Create()
        {
            using (CompanyDB dBModels = new CompanyDB())
            {              
                var department = new Department();
                return View(department);
            }
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    // TODO: Add insert logic here
                    department.Date_Time_UTC = DateTime.Now;
                    department.Update_Date_Time_UTC = DateTime.Now;
                    department.Server_Date_Time = department.Server_Date_Time;
                    dBModels.Departments.Add(department);
                    dBModels.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException ex)
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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    var department = dBModels.Departments.Where(model => model.ID == id).FirstOrDefault();
                    return View(department);
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

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Department department)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    department.Update_Date_Time_UTC = DateTime.Now;
                    dBModels.Entry(department).State = System.Data.Entity.EntityState.Modified;
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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            using (CompanyDB dBModels = new CompanyDB())
            {
                return View(dBModels.Departments.Where(model => model.ID == id).FirstOrDefault());
            }
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (CompanyDB dBModels = new CompanyDB())
                {
                    var department = dBModels.Departments.Where(model => model.ID == id).FirstOrDefault();
                    dBModels.Departments.Remove(department);
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
