using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    public class CourierViewModel
    {
        public Guid? ID { get; set; }
        public Guid hdnFileID { get; set; }
        [Required(ErrorMessage = "Please Choose Type")]
        [Display(Name = "Courier Type")]
        [MaxLength(10)]
        public string Type { get; set; }
        public List<SelectListItem> CourierTypeList { get; set; }
        [Required(ErrorMessage = "Please enter Transaction Date")]
        [Display(Name = "Transaction Date")]
        public string TransactionDate { get; set; }
        [Required(ErrorMessage = "Please enter Source name")]
        [Display(Name = "Source Name")]
        [MaxLength(150)]
        public string SourceName { get; set; }
        [Display(Name = "Source Address")]
        [DataType(DataType.MultilineText)]
        public string SourceAddress { get; set; }
        [Required(ErrorMessage = "Please enter Destination name")]
        [Display(Name = "Destination Name")]
        [MaxLength(150)]
        public string DestName { get; set; }
        [Display(Name = "Destination Address")]
        [DataType(DataType.MultilineText)]
        public string DestAddress { get; set; }
        [Display(Name = "Distribution To")]
        [MaxLength(150)]
        public string DistributedTo { get; set; }
        [Display(Name = "Distribution Date")]
        public string DistributionDate { get; set; }
        [Required(ErrorMessage = "Please chosse agency")]
        [Display(Name = "Agency")]
        public string AgencyCode { get; set; }
        public CourierAgencyViewModel courierAgency { get; set; }
        public List<SelectListItem> AgencyList { get; set; }
        [Display(Name = "Tracking Reference No")]
        [MaxLength(50)]
        public string TrackingRefNo { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "General Notes")]
        public string GeneralNotes { get; set; }
        [Display(Name = "Tracking URL")]
        [MaxLength(500)]
        public string TrackingURL { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}