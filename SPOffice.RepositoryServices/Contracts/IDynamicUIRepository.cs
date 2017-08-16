using SPOffice.DataAccessObject.DTO; 
using System.Collections.Generic;
 

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IDynamicUIRepository
    {
        List<Menu> GetAllMenues();
    }
}
