using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPOffice.UserInterface.SecurityFilter;
using UserInterface.Models;
using AutoMapper;
using SPOffice.DataAccessObject.DTO;
using SPOffice.BusinessService.Contracts;
using SPOffice.UserInterface.Models;
using Newtonsoft.Json;

namespace SPOffice.UserInterface.Controllers
{
    public class FollowupController : Controller
    {

        #region Constructor_Injection 

        AppConst c = new AppConst();
        IFollowUpBusiness _followUpBusiness;

        ICommonBusiness _commonBusiness;

        public FollowupController(IFollowUpBusiness followUpBusiness, ICommonBusiness commonBusiness)
        {
            _followUpBusiness = followUpBusiness;

            _commonBusiness = commonBusiness;

        }
        #endregion Constructor_Injection 


        // GET: Followup
        public ActionResult Index()
        {
            return View();
        }



        #region InsertUpdateFollowUp
        [HttpPost]
        public string InsertUpdateFollowUp(FollowUpViewModel _followupObj)
        {
            FollowUpViewModel result = null;
            FollowUp resultfromBusiness = null;
            try
            {
                _followupObj.commonObj.CreatedDate = DateTime.Now;
                _followupObj.commonObj.UpdatedDate = DateTime.Now;

                resultfromBusiness =(FollowUp) _followUpBusiness.InsertUpdateFollowUp(Mapper.Map<FollowUpViewModel, FollowUp>(_followupObj));
                result = Mapper.Map<FollowUp, FollowUpViewModel>(resultfromBusiness);
                return JsonConvert.SerializeObject(new { Result = "OK", Records = result });

            }
            catch (Exception ex)
            {

                AppConstMessage cm = c.GetMessage(ex.Message);
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = cm.Message });
            }

        }
        #endregion InsertUpdateFollowUp

    }
}