using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface ICommonRepository
    {
        List<POStatuses> GetAllPOStatuses();
        string SendMessage(string message, string MobileNo,string provider,string type);
    }
}
