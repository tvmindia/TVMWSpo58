using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface ICommonBusiness
    {
        string ConvertCurrency(decimal value, int DecimalPoints = 0, bool Symbol = true);
        string GetXMLfromObject(List<QuoteItem> myObj, string mandatoryProperties);
        string GetXMLfromEnquiryObject(List<EnquiryItem> myObj, string mandatoryProperties);
        string GetXMLfromObj(List<ProformaItem> myOb, string mandatoryProperties);
        string GetXMLfromRequisitionDetailList(List<RequisitionDetail> myObj, string mandatoryProperties);
        List<POStatuses> GetAllPOStatuses();
        string SendMessage(string message, string mobileNo,string provider,string type);
    }
}
