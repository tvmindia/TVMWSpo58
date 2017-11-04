using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class FollowUpViewModel
    {
        public Guid ID { get; set; }
        public Guid EnquiryID { get; set; }
        [Required(ErrorMessage = "FollowUp Date is missing")]
        [Display(Name = "FollowUp Date")]
        public string FollowUpDate { get; set; }
        [Required(ErrorMessage = "FollowUp Time is missing")]
        [Display(Name = "FollowUp Time")]
        public string FollowUpTime { get; set; }
        public string HdnFollowUpTime { get; set; }
        public string Priority { get; set; }
        public string PriorityDescription { get; set; }
        [Required(ErrorMessage = "Subject is missing")]
        [DataType(DataType.MultilineText)]
        public string Subject { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Display(Name = "Remind Prior To")]
        public string RemindPriorTo { get; set; }
        [Display(Name = "Reminder Type")]
        public string ReminderType { get; set; }
        public string Status { get; set; }
        [Display(Name = "GeneralNotes")]
        [DataType(DataType.MultilineText)]
        public string GeneralNotes { get; set; }

        public string Company { get; set; }
        public string Contact { get; set; }

        public string EnquiryStatus { get; set; }
        public CommonViewModel commonObj { get; set; }
        public string URL { get; set; }
        [Display(Name = "Min")]
        public string Minutes { get; set; }
    }


    public class FollowUpListViewModel
    {
        public Guid FlwID { get; set; }
       // public Guid EnqID { get; set; }
        public List<FollowUpViewModel> FollowUpList { get; set; }
    }
}