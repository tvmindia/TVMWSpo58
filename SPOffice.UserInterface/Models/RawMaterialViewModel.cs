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
        public string UnitsCode { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public CommonViewModel commonObj { get; set; }
        public List<SelectListItem> RawMaterialList { get; set; }
        public List<SelectListItem> UnitList { get; set; }
        public List<MaterialTypeViewModel> materialTypeList { get; set; }
        public List<UnitsViewModel> unitTypeList { get; set; }
    }

    public class MaterialTypeViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
   
    public class SpareAPI
    {
        public string id { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public string is_enable_serial_no { get; set; }
        public string uom { get; set; }

    }
    public class MaterialAPI
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string material_name { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        
        public string uom { get; set; }
        public string msq { get; set; }
        public object vendorMaterialCode { get; set; }
        public object inventory_vendor_id { get; set; }
        

    }
    public class ResponseAPI
    {
        public string status { get; set; }
        public object message { get; set; }
        public string result { get; set; }
        public string time { get; set; }
        public string error_code { get; set; }
        public string error_msg { get; set; }

    }

    }