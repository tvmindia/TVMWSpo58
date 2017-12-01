using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;

namespace SPOffice.BusinessService.Contracts
{
    public interface IFollowUpBusiness
    {
        FollowUp InsertUpdateFollowUp(FollowUp _followupObj);
        List<FollowUp> GetFollowUpDetails(Guid EnquiryID);
        FollowUp GetFollowupDetailsByFollowUpID(Guid ID);
        List<FollowUp> GetRecentFollowUpCount(DateTime? Today);
    }
}