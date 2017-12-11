﻿using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class EnquiryBusiness: IEnquiryBusiness
    {
        private IEnquiryRepository _enquiryRepository;
        private ICommonRepository _commonRepository;

        public EnquiryBusiness(IEnquiryRepository enquiryRepository, ICommonRepository commonRepository)
        {
            _enquiryRepository = enquiryRepository;
            _commonRepository = commonRepository;
        }
        public Enquiry InsertUpdateEnquiry(Enquiry _enquiriesObj)
        {
            Enquiry result = null;
            try
            {
                if (_enquiriesObj.ID == Guid.Empty)
                {
                    result = _enquiryRepository.InsertEnquiry(_enquiriesObj);
                }
                else
                {
                    result = _enquiryRepository.UpdateEnquiry(_enquiriesObj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Enquiry>GetAllEnquiryList(Enquiry EqyObj)
        {
            return _enquiryRepository.GetAllEnquiryList(EqyObj);
        }


        public Enquiry SearchEnquiriesList(Enquiry enqObj)
        {
            Enquiry eqlObj = new Enquiry();
            List<Enquiry> enqList = new List<Enquiry>();
            try
            {
                enqList = _enquiryRepository.SearchEnquiriesList(enqObj);
                if (enqList != null)
                {
                    eqlObj.OpenList = new List<Enquiry>();
                    eqlObj.OpenList = enqList.Where(c => c.EnquiryStatus == "OE").ToList();
               
                    eqlObj.ConvertList = new List<Enquiry>();
                    eqlObj.ConvertList = enqList.Where(c => c.EnquiryStatus == "CE").ToList();
                
                    eqlObj.NonConvertList = new List<Enquiry>();
                    eqlObj.NonConvertList = enqList.Where(c => c.EnquiryStatus == "NCE").ToList();
                }
 
            }
            catch (Exception)
            {

                throw;
            }

            return eqlObj;
        }


        public List<Titles> GetAllTitles()
        {
            return _enquiryRepository.GetAllTitles();
        }

        public Enquiry GetEnquiryDetailsByID(Guid ID)
        {
            return _enquiryRepository.GetEnquiryDetailsById(ID);

        }

        //DeleteEnquiry
        public object DeleteEnquiry(Guid ID)
        {
            return _enquiryRepository.DeleteEnquiry(ID);
        }


        #region messageSending

        public string SendEnquiryMessage(string mobileNumber)
        {
           string result = _enquiryRepository.GetEnquiryMessage();
            result = "0000";//---- resetting msg to otp 
            return _commonRepository.SendMessage(result, mobileNumber, "2factor", "OTP");
        }

        #endregion messageSending

    }
}