using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.Models
{
    public class FollowUpViewModel
    {
        public Guid ID { get; set; }
        public Guid EnquiryID { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string FollowUpTime { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public string ContactName { get; set; }
        public string RemindPriorTo { get; set; }
        public string ReminderType { get; set; }
        public string Status { get; set; }
        public string GeneralNotes { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}