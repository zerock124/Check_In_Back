//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Check_InDB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class check_in
    {
        public int ci_sn { get; set; }
        public string ur_id { get; set; }
        public System.DateTime ci_da { get; set; }
        public System.TimeSpan ci_ut { get; set; }
        public System.TimeSpan ci_dt { get; set; }
        public int ca_mo { get; set; }
        public double ci_hr { get; set; }
        public string Remark { get; set; }
        public System.DateTime ci_ct { get; set; }
        public Nullable<System.DateTime> ci_ud { get; set; }
    }
}