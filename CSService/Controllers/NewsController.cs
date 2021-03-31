using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CSModel.Models;
using CSModel.ViewModels;

namespace CSService.Controllers
{
    public class NewsController : ApiController
    {
        private List<NewsList>GetNewsList(NewsListCondition objCondition)
        {
            using (CSDBEntities objDB = new CSDBEntities())
            {
                var sqlNews1 = objDB.cs_news.Where(n => n.enabled == "Y" && n.begin_date <= DateTime.Now && n.top_news == "Y")
                                            .Select(n => new NewsList
                                            {
                                                news_id = n.news_id,
                                                title = n.title,
                                                content = n.content,
                                                begin_date = n.begin_date,
                                                end_date = n.end_date,
                                                top_news = n.top_news,
                                                kind = n.news_kind
                                            }).AsEnumerable();

                var sqlNews2 = objDB.cs_news.Where(n => n.enabled == "Y" && n.begin_date <= DateTime.Now && n.top_news != "Y")
                                            .Select(n => new NewsList
                                            {
                                                news_id = n.news_id,
                                                title = n.title,
                                                content = n.content,
                                                begin_date = n.begin_date,
                                                end_date = n.end_date,
                                                top_news = n.top_news,
                                                kind = n.news_kind
                                            }).AsEnumerable();

                if (objCondition != null)
                {
                    if (String.IsNullOrEmpty(objCondition.search_title) == false)
                    {
                        sqlNews1 = sqlNews1.Where(n => n.title.Contains(objCondition.search_title));
                        sqlNews2 = sqlNews2.Where(n => n.title.Contains(objCondition.search_title));
                    }

                    if (String.IsNullOrEmpty(objCondition.searc_content) == false)
                    {
                        sqlNews1 = sqlNews1.Where(n => n.content.Contains(objCondition.searc_content));
                        sqlNews2 = sqlNews2.Where(n => n.content.Contains(objCondition.searc_content));
                    }

                    if (String.IsNullOrEmpty(objCondition.search_date) == false)
                    {
                        DateTime dteSearch;
                        if (DateTime.TryParse(objCondition.search_date, out dteSearch))
                        {
                            sqlNews1 = sqlNews1.Where(n => DateTime.Compare(dteSearch, n.begin_date) >= 0 &&
                                                           (n.end_date is null || DateTime.Compare((DateTime)n.end_date, dteSearch) >= 0));

                            sqlNews2 = sqlNews2.Where(n => DateTime.Compare(dteSearch, n.begin_date) >= 0 &&
                                                           (n.end_date is null || DateTime.Compare((DateTime)n.end_date, dteSearch) >= 0));
                        }
                    }

                    if (String.IsNullOrEmpty(objCondition.search_kind) == false)
                    {
                        sqlNews1 = sqlNews1.Where(n => n.kind == objCondition.search_kind);
                        sqlNews2 = sqlNews2.Where(n => n.kind == objCondition.search_kind);
                    }
                }

                var lstNews = sqlNews1.Concat(sqlNews2).ToList();

                lstNews.ForEach(
                    n =>
                    {
                        if (n.top_news == "Y")
                        {
                            n.top_news = "v";
                        }
                        else
                        {
                            n.top_news = "";
                        }

                        n.begin_end_date = n.begin_date.ToString("yyyy/MM/dd");
                        if (n.end_date != null)
                        {
                            n.begin_end_date += " ~ " + n.end_date.Value.ToString("yyyy/MM/dd");
                        }
                    }
                );

                return lstNews;
            }
        }

        public List<NewsList> Get(string SearchTitle, string SearchContent,
                            string SearchDate, string SearchKind)
        {
            var objCondition = new NewsListCondition
            {
                search_title = SearchTitle,
                searc_content = SearchContent,
                search_date = SearchDate,
                search_kind = SearchKind
            };

            return GetNewsList(objCondition);
        }

        public cs_news Get(int NewsID)
        {
            using (CSDBEntities objDB = new CSDBEntities())
            {
                var objNews = objDB.cs_news.Where(n => n.news_id == NewsID).FirstOrDefault();
                return objNews;
            }
        }

        //public List<NewsList.News> Post([FromBody] NewsListCondition objCondition)
        //{
        //    var lstNews = NewsList(objCondition);
        //    return lstNews;
        //}
    }
}
