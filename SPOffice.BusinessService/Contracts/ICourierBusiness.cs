using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface ICourierBusiness
    {
        List<CourierAgency> GetAllCourierAgency();
        CourierAgency GetCourierAgencyDetails(string Code);
        object InsertCourierAgency(CourierAgency courierAgency);
        object UpdateCourierAgency(CourierAgency courierAgency);
        object DeleteCourierAgency(string Code);

        List<Courier> GetAllCouriers();
        Courier GetCourierDetails(Guid ID);
    }
}
