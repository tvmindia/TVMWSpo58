﻿using SPOffice.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class EnquirySourceBusiness : IEnquirySourceBusiness
    {
        private IEnquirySourceRepository _enquirySourceRepository;

        public EnquirySourceBusiness(IEnquirySourceRepository enquirySourceRepository)
        {
            _enquirySourceRepository = enquirySourceRepository;
        }
        public List<EnquirySource> GetAllEnquirySourceList()
        {
            return _enquirySourceRepository.GetAllEnquirySourceList();
        }
    }
}