using System;
using System.Linq;
using System.Web.Mvc;
using CSModel.Models;
using CSWeb.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;

namespace CSWeb.Controllers
{
    public class NewsController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:14794/api/");
        HttpClient client;

        public NewsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: News
        public ActionResult Index(string SearchTitle, string SearchContent,
                                  string SearchDate, string SearchKind)
        {
            HttpResponseMessage response;
            response = client.GetAsync(string.Format("News?SearchTitle={0}&SearchContent={1}&SearchDate={2}&SearchKind={3}",SearchTitle, SearchContent, SearchDate, SearchKind)).Result;

            NewsListViewModel objNews = new NewsListViewModel();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                objNews = JsonConvert.DeserializeObject<NewsListViewModel>(data);

                var selectListKind = objNews.kind_list.Select(k => new SelectListItem()
                {
                    Value = k.value,
                    Text = k.text
                }).ToList();

                objNews.select_list_kind = selectListKind;
            }

            ViewBag.breadcrumbs_list = objNews.breadcrumbs_list;
            return View(objNews);
        }

        public ActionResult Edit(int NewsID)
        {
            HttpResponseMessage response;
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