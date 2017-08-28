using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class TaxType
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public string isUpdate { get; set; }
        public Common commonObj { get; set; }
    }
}