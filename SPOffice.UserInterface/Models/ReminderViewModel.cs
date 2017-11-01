using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPOffice.UserInterface.Models
{
    public class ReminderViewModel
    {
        public List<SelectListItem> ReminderList { get; set; }
        public string Code { get; set; }
        public string ReminderDesc { get; set; }
    }
}