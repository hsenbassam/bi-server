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
    public class BenefitController : ApiController
    {
        private BIEntities db = new BIEntities();

        // GET api/Benefit
        public IEnumerable<Benefit> GetBenefits()
        {
            return db.Benefits.AsEnumerable();
        }

        // GET api/Benefit/5
        public Benefit GetBenefit(int id)
        {
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return benefit;
        }

        // PUT api/Benefit/5
        public HttpResponseMessage PutBenefit(int id, Benefit benefit)
        {
            if (ModelState.IsValid && id == benefit.BenefitId)
            {
                db.Entry(benefit).State = EntityState.Modified;

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

        // POST api/Benefit
        public HttpResponseMessage PostBenefit(Benefit benefit)
        {
            if (ModelState.IsValid)
            {
                db.Benefits.Add(benefit);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, benefit);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = benefit.BenefitId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Benefit/5
        public HttpResponseMessage DeleteBenefit(int id)
        {
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Benefits.Remove(benefit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, benefit);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}