using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CSModel.Models;
using CSModel.ViewModels;

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

                lstQuestion = (from q in sqlQuestion
                               from pk in objDB.cs_code_kind.Where(k => k.kind == "Q_PRIORITY").DefaultIfEmpty()
                               from pc in objDB.cs_code.Where(c => c.kind == pk.kind && c.enabled == "Y" && c.code == q.q_priority).DefaultIfEmpty()
                               from kk in objDB.cs_code_kind.Where(k => k.kind == "Q_KIND").DefaultIfEmpty()
                               from kc in objDB.cs_code.Where(c => c.kind == kk.kind && c.enabled == "Y" && c.code == q.q_kind).DefaultIfEmpty()
                               from sk in objDB.cs_code_kind.Where(k => k.kind == "Q_STATUS").DefaultIfEmpty()
                               from sc in objDB.cs_code.Where(c => c.kind == sk.kind && c.enabled == "Y" && c.code == q.q_status).DefaultIfEmpty()
                               select new QuestionList()
                               {
                                   q_code = q.q_code,
                                   q_priority_desc = pc.description,
                                   q_kind_desc = kc.description,
                                   q_title = q.q_title,
                                   q_content = q.q_content,
                                   q_status_desc = sc.description,
                                   apply_date = q.apply_date,
                               }).ToList();

                lstQuestion.ForEach(
                    q =>
                    {
                        q.apply_date_desc = q.apply_date.ToString("yyyy/MM/dd");
                    });

                return lstQuestion;
            }            
        }
        private QuestionList ConvertQuestion(object obj)
        {
            return (QuestionList)obj;
        }
    }
}
