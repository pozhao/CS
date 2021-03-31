using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CSModel.Models;
using CSModel.ViewModels;
using CSCommon.library;

namespace CSService.Controllers
{
    public class QuestionController : ApiController
    {
        public List<QuestionList> Get(string SearchStatus, string SearchDateBegin, string SearchDateEnd, string SearchTitle)
        {
            using (CSDBEntities objDB = new CSDBEntities())
            {
                var sqlQuestion = objDB.cs_question.AsQueryable();
                if (String.IsNullOrEmpty(SearchStatus) == false)
                {
                    sqlQuestion = sqlQuestion.Where(q => q.q_status == SearchStatus);
                }

                if (String.IsNullOrEmpty(SearchDateBegin) == false)
                {
                    DateTime dteSearch;
                    if (DateTime.TryParse(SearchDateBegin, out dteSearch))
                    {
                        sqlQuestion = sqlQuestion.Where(q => DateTime.Compare(dteSearch, q.apply_date) <= 0);
                    }
                }

                if (String.IsNullOrEmpty(SearchDateEnd) == false)
                {
                    DateTime dteSearch;
                    if (DateTime.TryParse(SearchDateEnd, out dteSearch))
                    {
                        sqlQuestion = sqlQuestion.Where(q => DateTime.Compare(dteSearch, q.apply_date) >= 0);
                    }
                }

                if (String.IsNullOrEmpty(SearchTitle) == false)
                {
                    sqlQuestion = sqlQuestion.Where(q => q.q_title.Contains(SearchTitle));
                }

                List<QuestionList> lstQuestion;
                //CopyClass.CopyListToList(sqlQuestion, ref lstQuestion, CopyClass.Scope.DestinationAsMain);

                lstQuestion = Array.ConvertAll<cs_question, QuestionList>(sqlQuestion.ToArray(), q => (QuestionList)q).ToList();

                //lstQuestion = sqlQuestion.ToList().ConvertAll(q => q. new Converter<cs_question, QuestionList>(ConvertQuestion));

                lstQuestion.ForEach(
                    q =>
                    {
                        q.apply_date_desc = q.apply_date.ToString("yyyy/MM/dd");
                    }
                );

                //var lstsqlQuestion = sqlQuestion.Select(q => new QuestionList()
                //{
                //    apply_date_desc = q.apply_date.ToString("yyyy/MM/dd"),
                //});

                return lstQuestion;

                //return sqlQuestion.Select(q => new QuestionList() 
                //{
                    
                //}).ToList();
            }            
        }
        private QuestionList ConvertQuestion(object obj)
        {
            return (QuestionList)obj;
        }
    }
}
