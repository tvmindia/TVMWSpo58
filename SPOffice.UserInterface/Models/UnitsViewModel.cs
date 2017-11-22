using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class UnitsViewModel
    {
        [Display(Name = "Code")]
        [Required(ErrorMessage = "Code is missing")]
        public string UnitsCode { get; set; }
        public string hdnCode { get; set; }
        public string Description { get; set; }
        public CommonViewModel commonObj { get; set; }
        public List<SelectListItem> UnitsList { get; set; }
    }
}