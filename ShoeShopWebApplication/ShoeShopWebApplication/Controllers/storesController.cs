using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShoeShopWebApplication;
using ShoeShopWebApplication.Models;

namespace ShoeShopWebApplication.Controllers
{
    public class storesController : ApiController
    {
        private BDShoeShopEntities db = new BDShoeShopEntities();
        private generalResponse objResponse = new generalResponse();
        private ResponseStores objResponseStores = new ResponseStores();
        private errorResponse objError = new errorResponse();

        // GET: service/stores
        public HttpResponseMessage Getstores()
        {

            List<clsStores> lsStores = new List<clsStores>();

            lsStores = (from a in db.stores
                         select new clsStores()
                         {
                             id = a.id,
                             name = a.name,
                             address = a.address
                         }).ToList<clsStores>();

            objResponseStores = objResponse.responseStoresSuccess(lsStores, storesTotal());
            return Request.CreateResponse(HttpStatusCode.OK, objResponseStores);
        }

        // GET: service/stores/5
        [ResponseType(typeof(stores))]
        public HttpResponseMessage Getstores(int id)
        {
            clsStores objStores = new clsStores();

            try
            {
                objStores = (from a in db.stores
                              where (a.id == id)
                              select new clsStores()
                              {
                                  id = a.id,
                                  name = a.name,
                                  address = a.address
                              }).FirstOrDefault<clsStores>();


                if (objStores == null)
                {
                    objError = objResponse.responseError(HttpStatusCode.NotFound);
                    return Request.CreateResponse(HttpStatusCode.NotFound, objError, Configuration.Formatters.JsonFormatter);
                }

                objResponseStores = objResponse.responseStoresSuccess(objStores, storesTotal());
                return Request.CreateResponse(HttpStatusCode.OK, objResponseStores, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                objError = objResponse.responseError(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.BadRequest, objError, Configuration.Formatters.JsonFormatter);
            }
        }

        // PUT: service/stores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstores(int id, stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stores.id)
            {
                return BadRequest();
            }

            db.Entry(stores).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!storesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: service/stores
        [ResponseType(typeof(stores))]
        public IHttpActionResult Poststores(stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stores.Add(stores);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (storesExists(stores.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stores.id }, stores);
        }

        // DELETE: service/stores/5
        [ResponseType(typeof(stores))]
        public IHttpActionResult Deletestores(int id)
        {
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return NotFound();
            }

            db.stores.Remove(stores);
            db.SaveChanges();

            return Ok(stores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool storesExists(int id)
        {
            return db.stores.Count(e => e.id == id) > 0;
        }

        private int storesTotal()
        {
            int numDataTotal = 0;

            try
            {
                numDataTotal = db.stores.Count();
            }

            catch (Exception e)
            {
                return numDataTotal;
                throw e;
            }

            return numDataTotal;
        }
    }
}