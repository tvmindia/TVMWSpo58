﻿using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class ReportBusiness : IReportBusiness
    {
        IReportRepository _reportRepository;
        
        public ReportBusiness(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
           
        }

        public List<Reports> GetAllSysReports(AppUA appUA)
        {
            List<Reports> systemReportList = null;
            try
            {
                systemReportList = _reportRepository.GetAllSysReports(appUA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return systemReportList;
        }

        public List<EnquiryReport> GetEnquiryDetails(DateTime? FromDate, DateTime? ToDate,string EnquiryStatus, string search)
        {
            List<EnquiryReport> enquiryReportList = null;
            try
            {
                enquiryReportList = _reportRepository.GetEnquiryDetails(FromDate, ToDate, EnquiryStatus, search);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return enquiryReportList;

        }

        public List<QuotationReport> GetQuotationDetails(ReportAdvanceSearch ReptAdvancedSearchObj)
        {
            List<QuotationReport> quotationList = null;
            try
            {
                quotationList = _reportRepository.GetQuotationDetails(ReptAdvancedSearchObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return quotationList;

        }


        //CourierReport
        public List<CourierReport> GetCourierDetails(DateTime? FromDate, DateTime? ToDate, string AgencyCode, string search, string Type)
        {
            //CourierReport CourierdetailObj = new CourierReport();
            List<CourierReport> courierDetailList = null;
            try
            {
                courierDetailList = _reportRepository.GetCourierDetails(FromDate, ToDate, AgencyCode, search, Type);
               // CourierdetailObj.courierDetailList = courierDetailList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return courierDetailList;
        }



    }
    }