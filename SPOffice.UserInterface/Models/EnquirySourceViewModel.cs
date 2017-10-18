using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Models
{
    public class EnquirySourceViewModel
    {
        public List<SelectListItem> EnquirySourceList { get; set; }
        public string SourceCode { get; set; }
        public string Source { get; set; }
    }
}