using System;

namespace CSModel.ViewModels
{
    public class NewsListCondition
    {
        public string search_title { get; set; }
        public string searc_content { get; set; }
        public string search_date { get; set; }
        public string search_kind { get; set; }
    }

    public class NewsList
    {
        public int news_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime begin_date { get; set; }
        public Nullable<DateTime> end_date { get; set; }
        public string begin_end_date { get; set; }
        public string top_news { get; set; }
        public string kind { get; set; }
    }

    public class NewsDetail
    {
        public string news_kind { get; set; }
        public string news_kind_desc { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public System.DateTime begin_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public string top_news { get; set; }
        public int update_user_id { get; set; }
        public string update_user_name { get; set; }
        public System.DateTime update_date { get; set; }
        public string maintainer { get; set; }
        public string maintainer_tel { get; set; }
    }
}