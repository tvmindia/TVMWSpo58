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
        public decimal ApproximateRate { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Common commonObj { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public string UnitsCode { get; set; }
    }

    public class MaterialType
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}