using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class PriorityBusiness: IPriorityBusiness
    {
        private IPriorityRepository _priorityRepository;

        public PriorityBusiness(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }
        public List<Priority> GetAllPriorityList()
        {
            return _priorityRepository.GetAllPriorityList();
        }
    }
}