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
        #region Constructor_Injection

        IEnquiryBusiness _enquiriesBusiness;


        public EnquiryController(IEnquiryBusiness enquiriesBusiness)
        {
            _enquiriesBusiness = enquiriesBusiness;

        }
        #endregion Constructor_Injection

        Const messages = new Const();
   #region InsertUpdateEnquiry
        [HttpPost]
        public string InsertUpdateEnquiry(EnquiryViewModel _enquiriesObj)
        {
            object result = null;

            try
            {
                AppUA _appUA = Session["AppUA"] as AppUA;
                _enquiriesObj.commonObj = new CommonViewModel();
                _enquiriesObj.commonObj.CreatedBy = _appUA.UserName;
                _enquiriesObj.commonObj.CreatedDate = _appUA.DateTime;
                _enquiriesObj.commonObj.UpdatedBy = _appUA.UserName;
                _enquiriesObj.commonObj.UpdatedDate = _appUA.DateTime;

                result = _enquiriesBusiness.InsertUpdateEnquiry(Mapper.Map<EnquiryViewModel, Enquiry>(_enquiriesObj));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = result });

            }
            catch (Exception ex)
            {

                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }

        }

#endregion InsertUpdateEnquiry
        }
}

    