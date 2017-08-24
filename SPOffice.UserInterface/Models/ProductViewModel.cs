using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace UserInterface.Models
{
    public class ProductViewModel
    {
      public Guid? ID { get; set; }
      
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is missing")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is missing")]
        public string Description { get; set; }
        [Required(ErrorMessage = "UnitCode is missing")]
        public string UnitCode { get; set; }
        [Required(ErrorMessage = "Rate is missing")]
        public decimal Rate { get; set; }
      public CommonViewModel commonObj { get; set; }
      public List<SelectListItem> unitList { get; set; }
      public List<UnitViewModel> unitViewModelList;
        public UnitViewModel unit { get; set; }
    }
    public class UnitViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

}