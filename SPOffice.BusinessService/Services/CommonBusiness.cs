using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class CommonBusiness : ICommonBusiness
    {
        ICommonRepository _commonRepository;
        public CommonBusiness(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public string ConvertCurrency(decimal value, int DecimalPoints = 0, bool Symbol = true)
        {
            string result = value.ToString();
            string fare = result;
            decimal parsed = decimal.Parse(fare, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            if (Symbol)
                result = string.Format(hindi, "{0:C" + DecimalPoints + "}", parsed);
            else
            {
                if (DecimalPoints == 0)
                { result = string.Format(hindi, "{0:#,#.##}", parsed); }
                else
                { result = string.Format(hindi, "{0:#,#0.00}", parsed); }
            }
            return result;

        }


        private int getMAndatoryIndex(object myObj, string mandatoryProperties)
        {

            int mandIndx = -1;

            object tmp = myObj;
            var ppty = GetProperties(tmp);
            int i;
            for (i = 0; i < ppty.Length; i++)
            {

                if (ppty[i].Name == mandatoryProperties)
                {
                    mandIndx = i;
                    break;
                }

            }

            return mandIndx;


        }

        private void XML(object some_object, int mandIndx, ref string result, ref int totalRows)
        {

            var properties = GetProperties(some_object);
            var mand = properties[mandIndx].GetValue(some_object, null);

            if ((mand != null) && (!string.IsNullOrEmpty(mand.ToString())))
            {

                result = result + "<item ";


                foreach (var p in properties)
                {
                    string name = p.Name;
                    var value = p.GetValue(some_object, null);
                    result = result + " " + name + @"=""" + value + @""" ";

                }
                result = result + "></item>";
                totalRows = totalRows + 1;
            }
        }

        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

        public string GetXMLfromObject(List<QuoteItem> myObj, string mandatoryProperties)
        {
            string result = "<Details>";
            int totalRows = 0;
            try
            {
                //-------------------------//
                int mandIndx = getMAndatoryIndex(myObj[0], mandatoryProperties); //int mandIndx = 0;                

                foreach (object some_object in myObj)
                {
                    XML(some_object, mandIndx, ref result, ref totalRows);

                }

                result = result + "</Details>";


            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (totalRows > 0)
            {
                return result;
            }
            else
            {
                return "";
            }

        }

        public string GetXMLfromObj(List<ProformaItem> myObj, string mandatoryProperties)
        {
            string result = "<Details>";
            int totalRows = 0;
            try
            {
                //-------------------------//
                int mandIndx = getMAndatoryIndex(myObj[0], mandatoryProperties); //int mandIndx = 0;                

                foreach (object some_object in myObj)
                {
                    XML(some_object, mandIndx, ref result, ref totalRows);
                }
                result = result + "</Details>";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (totalRows > 0)
            {
                return result;
            }
            else
            {
                return "";
            }
        }
        public string GetXMLfromRequisitionDetailList(List<RequisitionDetail> myObj, string mandatoryProperties)
        {
            string result = "<Details>";
            int totalRows = 0;
            try
            {
                //-------------------------//
                int mandIndx = getMAndatoryIndex(myObj[0], mandatoryProperties); //int mandIndx = 0;                
                foreach (object some_object in myObj)
                {
                    XML(some_object, mandIndx, ref result, ref totalRows);
                }
                                result = result + "</Details>";
            }
            catch (Exception)
            {

                throw;
            }
            if (totalRows > 0)
            {
                return result;
            }
            else
            {
                return "";
            }

        }

        public List<POStatuses> GetAllPOStatuses()
        {
            return _commonRepository.GetAllPOStatuses();
        }

        //Send Message
        #region messageSending
        public string SendMessage(string message, string MobileNo, string provider, string type)
        {           
            return _commonRepository.SendMessage(message, MobileNo, provider, type);
        }
        #endregion messageSending
    }
}