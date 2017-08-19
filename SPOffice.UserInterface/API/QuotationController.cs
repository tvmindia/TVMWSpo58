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

namespace SPOffice.UserInterface.API
{
    public class QuotationController : ApiController
    {
        AppConst c = new AppConst();
        #region Constructor_Injection

        IQuotationBusiness _quotationBusiness;


        public QuotationController(IQuotationBusiness quotationBusiness)
        {
            _quotationBusiness = quotationBusiness;

        }
        #endregion Constructor_Injection



        #region GetQuotationList
        [HttpPost]
        public object GetQuotationDetailsForMobile(Quotation duration)
        {
            try
            {
                List<QuotationViewModel> QuotationsList = Mapper.Map<List<Quotation>, List<QuotationViewModel>>(_quotationBusiness.GetQuotationDetails(duration));
                return JsonConvert.SerializeObject(new { Result = true, Records = QuotationsList });
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetQuotationList
    }
}
