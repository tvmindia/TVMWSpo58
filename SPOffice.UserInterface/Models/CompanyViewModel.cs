using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class CompanyViewModel
    {
            public string Code { get; set; }
            public string Name { get; set; }
            public string BillingAddress { get; set; }
            public string ShippingAddress { get; set; }
            public CommonViewModel commonObj { get; set; }

        
    }
}