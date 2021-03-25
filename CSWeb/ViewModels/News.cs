using System.Collections.Generic;
using CSModel.ViewModels;
using System.Web.Mvc;

namespace CSWeb.ViewModels
{
    public class NewsListViewModel: NewsList
    {
        public List<SelectListItem> select_list_kind { get; set; }
    }
}