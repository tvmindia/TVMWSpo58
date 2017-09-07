using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public  interface ICourierRepository
    {
        List<CourierAgency> GetAllCourierAgency();
        object InsertCourierAgency(CourierAgency courierAgency);
        object UpdateCourierAgency(CourierAgency courierAgency);
        object DeleteCourierAgency(string Code);

        List<Courier> GetAllCouriers();
    }
}
