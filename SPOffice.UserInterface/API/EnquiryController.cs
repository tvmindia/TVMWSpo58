using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPOffice.DataAccessObject.DTO;
using System.Data;
using SPOffice.UserInterface.Models;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using UserInterface.Models;
using AutoMapper;

namespace SPOffice.UserInterface.API
{
    public class EnquiryController : ApiController
    {
        AppConst c = new AppConst();
        #region Constructor_Injection

        IEnquiryBusiness _enquiriesBusiness;


        public EnquiryController(IEnquiryBusiness enquiriesBusiness)
        {
            _enquiriesBusiness = enquiriesBusiness;

        }
        #endregion Constructor_Injection

        //Const messages = new Const();
        #region InsertUpdateEnquiry
        [HttpPost]
        public string InsertUpdateEnquiry(EnquiryViewModel _enquiriesObj)
        {
            object result = null;

            try
            {
                AppUA _appUA = new AppUA();
                _enquiriesObj.commonObj = new CommonViewModel();
                _enquiriesObj.commonObj.CreatedBy = "AppUser";
                _enquiriesObj.commonObj.CreatedDate = DateTime.Now;
                _enquiriesObj.commonObj.UpdatedBy = "AppUser";
                _enquiriesObj.commonObj.UpdatedDate = DateTime.Now;

                result = _enquiriesBusiness.InsertUpdateEnquiry(Mapper.Map<EnquiryViewModel, Enquiry>(_enquiriesObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = result });

            }
            catch (Exception ex)
            {

                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }

        }

        #endregion InsertUpdateEnquiry


        #region GetAllEnquiryType
        [HttpPost]
        public Object GetEnquiryListForMobile(Enquiry EqyObj)
        {
            try
            {
                List<EnquiryViewModel> enquiryObj = Mapper.Map<List<Enquiry>, List<EnquiryViewModel>>(_enquiriesBusiness.GetAllEnquiryList(EqyObj));
               
                return JsonConvert.SerializeObject(new { Result = true, Records = enquiryObj });

            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetAllEnquiryType


        #region SearchEnquiry
        [HttpPost]
        public string SearchEnquiryList(Enquiry enqObj)
        {
            try
            {
               EnquiryViewModel enquiryObj = Mapper.Map<Enquiry,EnquiryViewModel>(_enquiriesBusiness.SearchEnquiriesList(enqObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = new { Open = enquiryObj.OpenList, Converted = enquiryObj.ConvertList, NotConverted = enquiryObj.NonConvertList } });

            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetOutstandingInvoices
    }
}
    