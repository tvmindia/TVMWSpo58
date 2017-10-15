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
        object UpdateFollowUp(FollowUp _followupObj);
        List<FollowUp> GetFollowUpDetails(Guid EnquiryID);
        List<FollowUp> GetFollowUpDetailsOnDate(DateTime onDate);
    }
}