using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOffice.UserInterface.Models
{
    public class SupplierPOMailPreviewViewModel
    {
        [Display(Name = "Send To")]
        public string SentToEmails { get; set; }
        public string MailBody { get; set; }
        public SupplierOrderViewModel SOVMobj { get; set; }
    }
}