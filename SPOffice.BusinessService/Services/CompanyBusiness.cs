using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;

namespace SPOffice.BusinessService.Services
{
    public class CompanyBusiness : ICompanyBusiness
    {
        ICompanyRepository _companyRepository;
        public CompanyBusiness(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public List<Company> GetAllCompanies()
        {
            List<Company> companyList = null;
            companyList = _companyRepository.GetAllCompanies();
            companyList = companyList != null ? companyList.OrderBy(c => c.Name).ToList() : null;
            return companyList;
        }

        public Company GetCompanyDetailsByCode(string Code)
        {
            Company company = null;
            List<Company> companyList = null;
            companyList = _companyRepository.GetAllCompanies();
            company = companyList != null ? companyList.Where(Q => Q.Code == Code).SingleOrDefault() : null;
            return company;
        }
    }
}