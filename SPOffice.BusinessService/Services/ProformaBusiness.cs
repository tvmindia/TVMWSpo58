using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using SPOffice.RepositoryServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class ProformaBusiness : IProformaBusiness
    {

        private IProformaRepository _proformaRepository;

        public ProformaBusiness(IProformaRepository proformaRepository)
        {
            _proformaRepository = proformaRepository;
        }
        public List<Proforma> GetProformaDetails(Proforma duration)
        {
            return _proformaRepository.GetProformaDetails( duration);
        }
    }
}