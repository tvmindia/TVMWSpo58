using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class SupplierPOMailPreview
    {
        public string SentToEmails { get; set; }
        public string MailBody { get; set; }
        public SupplierOrder SOVMobj { get; set; }
    }
}