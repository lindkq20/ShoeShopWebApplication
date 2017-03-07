using Newtonsoft.Json;
using ShoeShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoeShopWeb.Controllers
{
    public class AdminController : Controller
    {
        string Baseurl = System.Configuration.ConfigurationManager.AppSettings["BaseUrlService"].ToString();

        public async Task<ActionResult> Index()
        {
            List<ResponseArticles> viewModelList = new List<ResponseArticles>();
            List<clsArticles> lsArticles = new List<clsArticles>();

            try
            {
                lsArticles = await getListArticle(); 
                return View(lsArticles);
            }
            catch (Exception e)
            {   
                return null;
                throw e;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<clsStores> lsStores = new List<clsStores>();
            lsStores = await getFillStore();          
            return View("Create");       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(clsArticles article)
        {
            List<clsStores> lsStores = new List<clsStores>();
            bool result = false;          

            if (!ModelState.IsValid)
            {
                lsStores = await getFillStore();
                return View(article);
            }

            result = await setArticle(article);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            clsArticles Articles = new clsArticles();
            List<clsStores> lsStores = new List<clsStores>(); 
            lsStores = await getFillStore();
            Articles = await getArticleByID(id);

            return View(Articles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(clsArticles article)
        {
            List<clsStores> lsStores = new List<clsStores>();
            bool result = false;

            if (!ModelState.IsValid)
            {
                lsStores = await getFillStore();
                return View(article);
            }
            result = await saveArticle(article);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            clsArticles Articles = new clsArticles();
            Articles = await getArticleByID(id);
            return View(Articles);       
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            bool result = false;
            result = await deleteArticle(id);
            return RedirectToAction("Index");
        }
        public async Task<List<clsStores>> getFillStore()
        {
            List<clsStores> lsStores = new List<clsStores>();
            try
            {
                

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResStores = await client.GetAsync("services/stores/");

                    if (ResStores.IsSuccessStatusCode)
                    {
                        var StoresResponse = ResStores.Content.ReadAsStringAsync().Result;
                        ResponseStores account = JsonConvert.DeserializeObject<ResponseStores>(StoresResponse);
                        lsStores = JsonConvert.DeserializeObject<List<clsStores>>(account.stores.ToString());
                    }

                    ViewBag.StoreList = lsStores;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lsStores;
        }

        public async Task<List<clsArticles>> getListArticle()
        {
            List<ResponseArticles> viewModelList = new List<ResponseArticles>();
            List<clsArticles> lsArticles = new List<clsArticles>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("services/articles/");

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        ResponseArticles account = JsonConvert.DeserializeObject<ResponseArticles>(EmpResponse);
                        lsArticles = JsonConvert.DeserializeObject<List<clsArticles>>(account.articles.ToString());
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lsArticles;
        }

        public async Task<bool> setArticle(clsArticles article)
        {
            List<clsStores> lsStores = new List<clsStores>();
            bool result = false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.PostAsJsonAsync("/services/articles", article);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        result = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<clsArticles> getArticleByID(int? id)
        {
            clsArticles Articles = new clsArticles();           

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync(String.Format("services/articles/{0}", id));

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        ResponseArticles account = JsonConvert.DeserializeObject<ResponseArticles>(EmpResponse);
                        Articles = JsonConvert.DeserializeObject<clsArticles>(account.articles.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Articles;
        }

        public async Task<bool> saveArticle(clsArticles article)
        {          
            bool result = false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.PutAsJsonAsync(String.Format("/services/articles/{0}", article.id), article);

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<bool> deleteArticle(int? id)
        {
            bool result = false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.DeleteAsync(string.Format("/services/articles/{0}", id));

                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}