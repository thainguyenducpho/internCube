using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CubeSYSVN_Intern_anhthai.Models
{
    public class Svr_VersionViewModel
    {
        public Nullable<decimal> TYPE_SEND { get; set; }
        public Nullable<System.DateTime> FROM_DATE { get; set; }
        public Nullable<System.DateTime> TO_DATE { get; set; }
        public Nullable<System.DateTime> INSERT_DATE { get; set; }
        public string INSERT_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public Nullable<decimal> DEL_FLAG { get; set; }
        public decimal VERSION_NO { get; set; }

        public Nullable<decimal> BUMON_ID { get; set; }
        public string BUMON_NAME { get; set; }
    }
}