using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.UserInterface.Models
{
    public class TaxTypeViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public string isUpdate { get; set; }
        
    }
}