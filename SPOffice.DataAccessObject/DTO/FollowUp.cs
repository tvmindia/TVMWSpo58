using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class FollowUp
    {
        public Guid ID { get; set; }
        public Guid EnquiryID { get; set; }
        public string FollowUpDate { get; set; }
        public string FollowUpTime { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public string ContactName { get; set; }
        public string RemindPriorTo { get; set; }
        public string ReminderType { get; set; }
        public string Status { get; set; }
        public string GeneralNotes { get; set; }
        public Common commonObj { get; set; }
    }
}