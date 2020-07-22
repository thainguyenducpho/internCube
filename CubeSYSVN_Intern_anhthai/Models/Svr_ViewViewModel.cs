using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeSYSVN_Intern_anhthai.Models
{
    public class Svr_ViewViewModel
    {
        public string IMEI { get; set; }
        public Nullable<decimal> BUMON_ID { get; set; }
        public string STORE_CD { get; set; }
        public Nullable<decimal> PRODUCT_ID { get; set; }
        public string VIEW_DATE { get; set; }
        public Nullable<decimal> GENDER { get; set; }
        public Nullable<decimal> AGE { get; set; }
        public Nullable<decimal> VIEWS { get; set; }
        public Nullable<System.DateTime> INSERT_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public decimal LOG_ID { get; set; }


        public string BUMON_NAME { get; set; }
        public string STORE_NAME { get; set; }
    }
}