using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CSModel.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;

namespace CSWeb.Controllers
{
    public class QuestionController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:14794/api/");
        HttpClient client;
        HttpResponseMessage response;

        public QuestionController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }



        // GET: Question
        public ActionResult Index(string SearchStatus, string SearchDateBegin, string SearchDateEnd, string SearchTitle)
        {            
            response = client.GetAsync(string.Format("Question?SearchStatus={0}&SearchDateBegin={1}&SearchDateEnd={2}&SearchTitle={3}", SearchStatus, SearchDateBegin, SearchDateEnd, SearchTitle)).Result;

            List<QuestionList> lstQuestion = new List<QuestionList>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstQuestion = JsonConvert.DeserializeObject<List<QuestionList>>(data);

                //lstQuestion.ForEach(
                //    q =>
                //    {
                //        q.apply_date_desc = q.apply_date.ToString("yyyy/MM/dd");
                //    }
                //);

                //var selectListKind = objNews.kind_list.Select(k => new SelectListItem()
                //{
                //    Value = k.value,
                //    Text = k.text
                //}).ToList();

                //objNews.select_list_kind = selectListKind;
            }

            //ViewBag.breadcrumbs_list = objNews.breadcrumbs_list;
            return View(lstQuestion);
        }

        // GET: Question/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Question/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Question/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
