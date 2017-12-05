using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IFollowUpRepository
    {
        FollowUp InsertFollowUp(FollowUp _followupObj);
        FollowUp UpdateFollowUp(FollowUp _followupObj);
        List<FollowUp> GetFollowUpDetails(Guid EnquiryID);
        List<FollowUp> GetFollowUpDetailsOnDate(DateTime onDate);
        FollowUp GetFollowupDetailsByFollowUpID(Guid ID);
        List<FollowUp> GetRecentFollowUpCount(DateTime? Today);
        object DeleteFollowUp(Guid ID);
    }
}