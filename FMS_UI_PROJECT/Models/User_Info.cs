using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS_UI_PROJECT.Models
{
    public class User_Info
    {
        public int? ID { get; set; }

        public string USER_NAME { get; set; }

        public string PASSWORD { get; set; }

        public string FULL_NAME { get; set; }

        public string USER_ID { get; set; }

        public string EMAIL { get; set; }

        public string PHONE { get; set; }

        public int? STATUS { get; set; }
    }
}