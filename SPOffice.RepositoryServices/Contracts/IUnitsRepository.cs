using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IUnitsRepository
    {
        List<Units> GetAllUnits();
        object InsertUnits(Units unitsObj);
        object UpdateUnits(Units unitsObj);
        object DeleteUnits(string code);
    }
}
