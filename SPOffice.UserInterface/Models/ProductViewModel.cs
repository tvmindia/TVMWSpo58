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
        [Required(ErrorMessage = "Code is missing")]
        public string Code { get; set; }
        public string OldCode { get; set; }
        [Required(ErrorMessage = "Name is missing")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is missing")]
        public string Description { get; set; }
        [Display(Name = "Unit Code")]
        [Required(ErrorMessage = "UnitCode is missing")]
        public string UnitCode { get; set; }
        [Required(ErrorMessage = "Rate is missing")]
        public decimal Rate { get; set; }
      public CommonViewModel commonObj { get; set; }
      public List<SelectListItem> unitList { get; set; }
      public List<UnitViewModel> unitViewModelList;
        public UnitViewModel unit { get; set; }
        public string APIKey { get; set; }
        public string UserName { get; set; }
        public UserViewModel userObj { get; set; }
        public List<SelectListItem> ProductLists { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        public List<SelectListItem> CategoryList { get; set; }

    }
    public class UnitViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class CategoryViewModel
    {
        //public string Category { get; set; }
      
    }

}