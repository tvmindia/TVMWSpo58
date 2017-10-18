using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Models
{
    public class IndustryViewModel
    {      
        public List<SelectListItem> IndustyList { get; set; }
        public string IndustryCode { get; set; }
        public string IndustryName { get; set; }
    }
}