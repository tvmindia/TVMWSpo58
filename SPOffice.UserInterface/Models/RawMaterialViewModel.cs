using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class RawMaterialViewModel
    {
        public Guid? ID { get; set; }
        [Display(Name ="Code")]
        [Required(ErrorMessage = "Code is missing")]
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}