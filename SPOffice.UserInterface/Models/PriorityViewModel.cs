using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Models
{
    public class PriorityViewModel
    {
        public List<SelectListItem> PriorityList { get; set; }
        public string PriorityCode { get; set; }
        public string PriorityDescription { get; set; }
    }
}