using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SPOffice.UserInterface.API
{
    public class CompanyController : ApiController
    {
        AppConst c = new AppConst();
        ICompanyBusiness _companyBusiness;
        public CompanyController(ICompanyBusiness companyBusiness)
        {
            _companyBusiness = companyBusiness;
        }
        #region GetAllCompanies
        [System.Web.Http.HttpPost]
        public string GetAllCompanies()
        {
            try
            {
                List<CompanyViewModel> CompaniesList = Mapper.Map<List<Company>, List<CompanyViewModel>>(_companyBusiness.GetAllCompanies());
               
                foreach (CompanyViewModel Cmp in CompaniesList)
                {
                    Cmp.Name = Cmp.UnitName != null ? Cmp.Name + '-' + '(' + Cmp.UnitName + ')' : Cmp.Name;
                }

                return JsonConvert.SerializeObject(new { Result = true, Records = CompaniesList });
            }
            catch (Exception ex)
            {
                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }
        }
        #endregion  GetAllCompanies

    }
}
