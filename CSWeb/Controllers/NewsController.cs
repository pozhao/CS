using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CSModel.Models;
using CSModel.ViewModels.Shared;
using CSModel.ViewModels;
using CSCommon.library;
using System.Net.Http;
using Newtonsoft.Json;

namespace CSWeb.Controllers
{
    public class NewsController : Controller
    {
        Uri baseAddress = new Uri(CSLibrary.apiString);
        HttpClient client;
        HttpResponseMessage response;

        public NewsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        #region "private function"
        private List<Breadcrumbs> GetBreadcrumbs()
        {            
            response = client.GetAsync(string.Format("BreadCrumbs?MenuCode={0}", "news")).Result;

            List<Breadcrumbs> lstBreadcrumbs = new List<Breadcrumbs>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstBreadcrumbs = JsonConvert.DeserializeObject<List<Breadcrumbs>>(data);
            }

            return lstBreadcrumbs;
        }

        private List<DropdownValue> GetNewsKinds()
        {
            response = client.GetAsync(string.Format("DropdownValue?KindCode={0}", "news_kind")).Result;

            List<DropdownValue> lstNewsKind = new List<DropdownValue>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstNewsKind = JsonConvert.DeserializeObject<List<DropdownValue>>(data);
            }

            return lstNewsKind;
        }
        #endregion

        // GET: News
        public ActionResult Index(string SearchTitle, string SearchContent,
                                  string SearchDate, string SearchKind)
        {            
            ViewBag.breadcrumbs_list = GetBreadcrumbs();
            ViewBag.news_kind_list = GetNewsKinds();

            response = client.GetAsync(string.Format("News?SearchTitle={0}&SearchContent={1}&SearchDate={2}&SearchKind={3}", SearchTitle, SearchContent, SearchDate, SearchKind)).Result;

            var lstNews = new List<NewsList>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstNews = JsonConvert.DeserializeObject<List<NewsList>>(data);
            }

            return View(lstNews);
        }

        public ActionResult Create()
        {
            ViewBag.breadcrumbs_list = GetBreadcrumbs();
            return View();
        }

        public ActionResult Detail(int NewsID)
        {
            HttpResponseMessage response;
            ViewBag.breadcrumbs_list = GetBreadcrumbs();

            response = client.GetAsync(string.Format("News?NewsID={0}", NewsID)).Result;

            cs_news objNews = new cs_news();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                objNews = JsonConvert.DeserializeObject<cs_news>(data);
            }

            return View(objNews);
        }

        //public ActionResult Index(string SearchTitle, string SearchContent,
        //                          string SearchDate, string SearchKind)
        //{
        //    var objCondition = new NewsListCondition
        //    {
        //        search_title = SearchTitle,
        //        searc_content = SearchContent,
        //        search_date = SearchDate,
        //        search_kind = SearchKind
        //    };

        //    HttpResponseMessage response
        //    var content = new StringContent(JsonConvert.SerializeObject(objCondition));
        //    response = client.PostAsync("news", content).Result;

        //    List<NewsListViewModel.News> lstNews = new List<NewsListViewModel.News>();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        lstNews = JsonConvert.DeserializeObject<List<NewsListViewModel.News>>(data);
        //    }

        //    return View(lstNews);
        //}
    }
}