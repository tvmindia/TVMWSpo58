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

namespace SPOffice.UserInterface.API
{
    public class ProformaController : ApiController
    {
        AppConst c = new AppConst();
        #region Constructor_Injection

        IProformaBusiness _proformaBusiness;


        public ProformaController(IProformaBusiness proformaBusiness)
        {
            _proformaBusiness = proformaBusiness;

        }
        #endregion Constructor_Injection

        #region GetProformaList
        [HttpPost]
        public object GetProformaDetailsForMobile(Proforma duration)
        {
            try
            {
                List<ProformaViewModel> ProformaList = Mapper.Map<List<Proforma>, List<ProformaViewModel>>(_proformaBusiness.GetProformaDetails(duration));
                return JsonConvert.SerializeObject(new { Result = true, Records = ProformaList });
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion GetProformaList
    }
}
