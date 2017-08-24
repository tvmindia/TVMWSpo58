﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class CourierAgencyViewModel
    {
        [Required(ErrorMessage = "Code is missing")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is missing")]
        public string Name { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage = "Phone is missing")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Email is missing")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is missing")]
        public string Address { get; set; }
        public CommonViewModel commonObj { get; set; }
        public string Operation { get; set; }
    }
}