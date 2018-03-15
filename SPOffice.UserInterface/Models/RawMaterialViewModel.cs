using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class RawMaterialViewModel
    {
        public Guid? ID { get; set; }
        [Display(Name ="Code")]
        [Required(ErrorMessage = "Code is missing")]
        public string MaterialCode { get; set; }
        public decimal ApproximateRate { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public CommonViewModel commonObj { get; set; }
        public List<SelectListItem> RawMaterialList { get; set; }
        public List<MaterialTypeViewModel> materialTypeList { get; set; }
    }

    public class MaterialTypeViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}