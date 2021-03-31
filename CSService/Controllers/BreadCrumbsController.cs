using System.Collections.Generic;
using System.Web.Http;
using CSModel.ViewModels.Shared;

namespace CSService.Controllers
{
    public class BreadcrumbsController : ApiController
    {
        public List<Breadcrumbs> Get(string MenuCode)
        {
            var lstBreadcrumbs = new List<Breadcrumbs>();
            switch (MenuCode)
            {
                case "news":
                    lstBreadcrumbs = new List<Breadcrumbs>() {
                        new Breadcrumbs()
                        {
                            name = "首頁",
                            action = "Index",
                            controller = "Home"
                        },
                        new Breadcrumbs()
                        {
                            name = "新聞",
                            action = "Index",
                            controller = "News"
                        }
                    };
                    break;
                case "question":
                    lstBreadcrumbs = new List<Breadcrumbs>() {
                        new Breadcrumbs()
                        {
                            name = "首頁",
                            action = "Index",
                            controller = "Home"
                        },
                        new Breadcrumbs()
                        {
                            name = "問題",
                            action = "Index",
                            controller = "Question"
                        }
                    };
                    break;
                default:
                    break;
            }

            return lstBreadcrumbs;
        }
    }
}
