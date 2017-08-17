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
                    AppUA _appUA = new AppUA();
                _followupObj.commonObj = new CommonViewModel();
                _followupObj.commonObj.CreatedBy = "AppUser";
                _followupObj.commonObj.CreatedDate = DateTime.Now;
                _followupObj.commonObj.UpdatedBy = "AppUser";
                _followupObj.commonObj.UpdatedDate = DateTime.Now;

                    result = _followupBusiness.InsertUpdateFollowUp(Mapper.Map<FollowUpViewModel, FollowUp>(_followupObj));
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