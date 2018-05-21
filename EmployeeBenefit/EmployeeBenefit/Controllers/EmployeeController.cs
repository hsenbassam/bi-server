using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;  
using EmployeeBenefit.Models;

namespace EmployeeBenefit.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private BIEntities db = new BIEntities();

        // GET api/Employee
        public List<Employee> GetEmployees()
        {            
   
           return db.Employees.ToList();
        }

        // GET api/Employee/5
        public Employee GetEmployee(int id)
        {       
                Employee employee = db.Employees.FirstOrDefault(o => o.EmployeeId == id);
                if (employee == null)
                {   
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "Employee Not Found"));
                   
                }
               
            return employee;          
        }


        [HttpGet]
        [Route("GetHighestPaid/{salary}")] // Employees have a salary higher than the argument
        public List<Employee> GetHighestPaid(int salary)
        {
            return db.Employees.Where(e => e.Salary > salary).ToList();
        }


    

        // PUT api/Employee/5
        public HttpResponseMessage PutEmployee(int id, Employee employee)
        {
            if (ModelState.IsValid && id == employee.EmployeeId)
            {
                db.Entry(employee).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Employee
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employee);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Employee/5
        public HttpResponseMessage DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Employees.Remove(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}