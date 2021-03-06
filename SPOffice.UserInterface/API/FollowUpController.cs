﻿using AutoMapper;
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
using UserInterface.Models;

namespace SPOffice.UserInterface.API
{
    public class FollowUpController : ApiController
    {

        AppConst c = new AppConst();
        #region Constructor_Injection
        
        IFollowUpBusiness _followupBusiness;
       

        public FollowUpController(IFollowUpBusiness followupBusiness)
        {
            _followupBusiness = followupBusiness;
          
        }
        #endregion Constructor_Injection

        //Const messages = new Const();
        #region InsertUpdateFollowUp
        [HttpPost]
        public string InsertUpdateFollowUp(FollowUpViewModel _followupObj)
        {
            object result = null;
            try
            {
                _followupObj.commonObj = new CommonViewModel();
                _followupObj.commonObj.CreatedBy = c.AppUser;
                _followupObj.commonObj.CreatedDate = DateTime.Now;
                _followupObj.commonObj.UpdatedBy = c.AppUser;
                _followupObj.commonObj.UpdatedDate = DateTime.Now;

                result = _followupBusiness.InsertUpdateFollowUp(Mapper.Map<FollowUpViewModel, FollowUp>(_followupObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = result });

            }
            catch (Exception ex)
            {

                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = cm.Message });
            }

        }

        #endregion InsertUpdateFollowUp

        #region GetFollowupList
        [HttpPost]
        public object GetFollowUpDetailsForMobile(FollowUpViewModel followObj)
        {
            try
            {
                List<FollowUpViewModel> followUpObj = Mapper.Map <List<FollowUp>,List<FollowUpViewModel>>(_followupBusiness.GetFollowUpDetails(followObj.EnquiryID != null && followObj.EnquiryID.ToString() != "" ? Guid.Parse(followObj.EnquiryID.ToString()) : Guid.Empty));
              
                return JsonConvert.SerializeObject(new { Result = true, Records = followUpObj });
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetFollowupList

    }
}
