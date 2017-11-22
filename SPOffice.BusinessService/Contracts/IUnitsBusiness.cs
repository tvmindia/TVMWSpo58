using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface IUnitsBusiness
    {
        List<Units> GetAllUnits();
        Units GetUnitsDetails(string code);
        object InsertUnits (Units unitsObj);
        object UpdateUnits (Units unitsObj);
        object DeleteUnits (string code);
    }
}
