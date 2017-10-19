using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class ProgressStatusBusiness: IProgressStatusBusiness
    {
        private IProgressStatusRepository _progressStatusRepository;

        public ProgressStatusBusiness(IProgressStatusRepository progressStatusRepository)
        {
            _progressStatusRepository = progressStatusRepository;
        }
        public List<ProgressStatus> GetAllProgressStatusList()
        {
            return _progressStatusRepository.GetAllProgressStatusList();
        }
    }
}