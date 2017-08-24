using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class CourierAgencyViewModel
    {
       
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is missing")]
        public string Name { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage = "Phone is missing")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public CommonViewModel commonObj { get; set; }
        public string Operation { get; set; }
    }
}