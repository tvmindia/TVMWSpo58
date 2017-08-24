using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class CourierAgency
    {
        public string Code { get; set; }
       
        public string Name { get; set; }
        public string Website { get; set; }
       
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Common commonObj { get; set; }
       
    }
}