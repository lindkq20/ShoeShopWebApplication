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
using ShoeShopWebApplication.Models;

namespace ShoeShopWebApplication.Controllers
{
public class articlesController : ApiController
    {
        private BDShoeShopEntities db = new BDShoeShopEntities();
        private generalResponse objResponse = new generalResponse();
        private ResponseArticles objResponseArticles = new ResponseArticles();
        private errorResponse objError = new errorResponse();

        // GET: service/articles
        public HttpResponseMessage Getarticles()
        {
            List<clsArticles> lsArticle = new List<clsArticles>();

            lsArticle = (from a in db.articles
                         select new clsArticles()
                         {
                                id = a.id,
                                name = a.name,
                                description =a.description,
                                total_in_shelf = a.total_in_shelf,
                                total_in_vault = a.total_in_vault,
                                store_id = a.store_id
                         }).ToList<clsArticles>();

            objResponseArticles = objResponse.responseArticleSuccess(lsArticle, articlesTotal());
            return Request.CreateResponse(HttpStatusCode.OK, objResponseArticles);
        }

        // GET: service/articles/5
        [ResponseType(typeof(articles))]
        public HttpResponseMessage Getarticles(int id)
        {
            clsArticles objArticle = new clsArticles();

            try
            {          

                objArticle = (from a in db.articles
                             where( a.id == id)
                             select new clsArticles()
                             {
                                 id = a.id,
                                 name = a.name,
                                 description = a.description,
                                 total_in_shelf = a.total_in_shelf,
                                 total_in_vault = a.total_in_vault,
                                 store_id = a.store_id
                             }).FirstOrDefault<clsArticles>();


                if (objArticle == null)
                {
                    objError = objResponse.responseError(HttpStatusCode.NotFound);
                    return Request.CreateResponse(HttpStatusCode.NotFound, objError, Configuration.Formatters.JsonFormatter);
                }

                objResponseArticles = objResponse.responseArticleSuccess(objArticle, articlesTotal());
                return Request.CreateResponse(HttpStatusCode.OK, objResponseArticles, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                objError = objResponse.responseError(HttpStatusCode.BadRequest);
                return Request.CreateResponse(HttpStatusCode.BadRequest, objError, Configuration.Formatters.JsonFormatter);
            }
        }

        // PUT: service/articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putarticles(int id, articles articles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articles.id)
            {
                return BadRequest();
            }

            db.Entry(articles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!articlesExists(id))
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

        // POST: service/articles
        [ResponseType(typeof(articles))]
        public IHttpActionResult Postarticles(articles articles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.articles.Add(articles);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = articles.id }, articles);
        }

        // DELETE: service/articles/5
        [ResponseType(typeof(articles))]
        public IHttpActionResult Deletearticles(int id)
        {
            articles articles = db.articles.Find(id);
            if (articles == null)
            {
                return NotFound();
            }

            db.articles.Remove(articles);
            db.SaveChanges();

            return Ok(articles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool articlesExists(int id)
        {
            return db.articles.Count(e => e.id == id) > 0;
        }

        private int articlesTotal()
        {
            int numDataTotal = 0;

            try
            {
                numDataTotal = db.articles.Count();
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