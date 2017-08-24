using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class RawMaterial
    {
        public Guid? ID { get; set; }
        public string  MaterialCode { get; set; }
        public string Description { get; set; }
        public Common commonObj { get; set; }
    }
}