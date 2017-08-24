﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class Company
    {
      public string Code { get; set; }
      public string Name { get; set; }
      public string BillingAddress { get; set; }
      public string ShippingAddress { get; set; }
      public Common commonObj { get; set; }
       
    }
}