using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;

namespace SPOffice.BusinessService.Contracts
{
    public interface IFollowUpBusiness
    {
        object InsertUpdateFollowUp(FollowUp _followupObj);
    }
}