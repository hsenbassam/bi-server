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
using EBenefit = EmployeeBenefit.Models.EmployeeBenefit;
using EmployeeBenefit.Models;

namespace EmployeeBenefit.Controllers
{
    public class EmployeeBenefitController : ApiController
    {
        private BIEntities db = new BIEntities();

        // GET api/EmployeeBenefit
        public IEnumerable<EBenefit> GetEmployeeBenefits()
        {
            var employeebenefits = db.EmployeeBenefits.Include(e => e.Benefit).Include(e => e.Employee);
            return employeebenefits.AsEnumerable();
        }

        // GET api/EmployeeBenefit/5
        public EBenefit GetEmployeeBenefit(int id)
        {
            EBenefit employeebenefit = db.EmployeeBenefits.Find(id);
            if (employeebenefit == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return employeebenefit;
        }

        // PUT api/EmployeeBenefit/5
        public HttpResponseMessage PutEmployeeBenefit(int id, EBenefit employeebenefit)
        {
            if (ModelState.IsValid && id == employeebenefit.EmployeeBenefitId)
            {
                db.Entry(employeebenefit).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/EmployeeBenefit
        public HttpResponseMessage PostEmployeeBenefit(EBenefit employeebenefit)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeBenefits.Add(employeebenefit);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employeebenefit);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/EmployeeBenefit/5
        public HttpResponseMessage DeleteEmployeeBenefit(int id)
        {
            EBenefit employeebenefit = db.EmployeeBenefits.Find(id);
            if (employeebenefit == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.EmployeeBenefits.Remove(employeebenefit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employeebenefit);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}