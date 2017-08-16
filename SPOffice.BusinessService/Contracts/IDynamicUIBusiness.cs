using SPOffice.DataAccessObject.DTO;
 
using System.Collections.Generic;
 

namespace SPOffice.BusinessService.Contracts
{
    public interface IDynamicUIBusiness
    {
        List<Menu> GetAllMenues();
    }
}
