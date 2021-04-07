using CSModel.Models;

namespace CSModel.ViewModels
{
    public class QuestionList
    {
        public string q_code { get; set; }
        public string q_priority_desc { get; set; }
        public string q_kind_desc { get; set; }
        public string q_title { get; set; }
        public string q_content { get; set; }
        public string q_status_desc { get; set; }        
        public System.DateTime apply_date { get; set; }
        public string apply_date_desc { get; set; }
    }
}