using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.DataAccessObject.DTO
{
    public class CourierAgency
    {
        public string Code { get; set; }
       
        public string Name { get; set; }
        public string Website { get; set; }
       
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Common commonObj { get; set; }
       
    }
    public class Courier
    {
        public Guid? ID { get; set; }
        public Guid hdnFileID { get; set; }
        public string Type { get; set; }
        public string TransactionDate { get; set; }
        public string Track { get; set; }
        public string SourceName { get; set; }
        public string SourceAddress { get; set; }
        public string DestName { get; set; }
        public string DestAddress { get; set; }
        public string DistributedTo { get; set; }
        public string DistributionDate { get; set; }
        public CourierAgency courierAgency { get; set; }
        public string AgencyCode { get; set; }
        public string TrackingRefNo { get; set; }
        public string GeneralNotes { get; set; }
        public string TrackingURL { get; set; }
        public Common commonObj { get; set; }
    }
}