//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cs_survey_topic
    {
        public int st_id { get; set; }
        public int s_id { get; set; }
        public string topic_name { get; set; }
        public string answer_type { get; set; }
        public string required { get; set; }
        public string is_subtitle { get; set; }
        public short serial_no { get; set; }
        public short row_cols { get; set; }
        public string memo { get; set; }
        public int update_user_id { get; set; }
        public System.DateTime update_date { get; set; }
    }
}
