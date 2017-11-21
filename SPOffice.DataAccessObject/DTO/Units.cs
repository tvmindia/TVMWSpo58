using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Units
    {
        public string UnitsCode { get; set; }
        public string hdnCode { get; set; }
        public string Description { get; set; }
        public Common commonObj { get; set; }
    }
}