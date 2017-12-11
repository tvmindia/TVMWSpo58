using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.UserInterface.Models
{
    public class MessageViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}