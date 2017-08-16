using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
 
namespace SPOffice.BusinessService.Services
{
    public class DynamicUIBusiness: IDynamicUIBusiness
    {
        private IDynamicUIRepository _dynamicUIRepository;         
        public DynamicUIBusiness(IDynamicUIRepository dynamicUIRespository )
        {
            _dynamicUIRepository = dynamicUIRespository;
           
        }

        public List<Menu> GetAllMenues()
        {
            try
            {
                return _dynamicUIRepository.GetAllMenues();
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}