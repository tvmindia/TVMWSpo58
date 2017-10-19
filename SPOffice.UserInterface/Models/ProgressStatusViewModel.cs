using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Models
{
    public class ProgressStatusViewModel
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> ProgressStatusList { get; set; }
    }
}