using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class CompanyViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public CommonViewModel commonObj { get; set; }
        public List<SelectListItem> CompanyList{get;set;}
        public string LogoURL { get; set; }
        public string BankDetails { get; set; } 

    }
}