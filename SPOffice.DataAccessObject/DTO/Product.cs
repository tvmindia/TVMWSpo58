﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Product
    {
        public Guid? ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitCode { get; set; }
        public decimal Rate { get; set; }
        public Common commonObj { get; set; }
        public Unit unit { get; set; }
    }

    public class Unit
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}