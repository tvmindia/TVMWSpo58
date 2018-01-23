using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SPOffice.BusinessService.Services
{
    public class RequisitionBusiness:IRequisitionBusiness
    {
        IRequisitionRepository _requisitionRepository;
        ICommonBusiness _commonBusiness;
        public RequisitionBusiness(IRequisitionRepository requisitionRepository, ICommonBusiness commonBusiness)
        {
            _requisitionRepository = requisitionRepository;
            _commonBusiness = commonBusiness;
        }
       public List<Requisition> GetUserRequisitionList(string LoginName, Guid AppID, bool IsAdminOrCeo, ReqAdvanceSearch ReqAdvanceSearchObj)
        {
            return _requisitionRepository.GetUserRequisitionList(LoginName,AppID,  IsAdminOrCeo,  ReqAdvanceSearchObj);
        }
        public List<RequisitionDetail> GetRequisitionDetailList(Guid ID)
        {
            return _requisitionRepository.GetRequisitionDetailList(ID);
        }
        public object InsertRequisition(Requisition RequisitionObj, bool isAdminOrCeo)
        {
            RequisitionObj.DetailXML = _commonBusiness.GetXMLfromRequisitionDetailList(RequisitionObj.RequisitionDetailList, "MaterialID");
            return _requisitionRepository.InsertRequisition(RequisitionObj,isAdminOrCeo);
        }
        public object UpdateRequisition(Requisition RequisitionObj)
        {
            RequisitionObj.DetailXML = _commonBusiness.GetXMLfromRequisitionDetailList(RequisitionObj.RequisitionDetailList, "MaterialID");
            return _requisitionRepository.UpdateRequisition(RequisitionObj);
        }
        public Requisition GetRequisitionDetails(Guid ID,string LoginName)
        {
            return _requisitionRepository.GetRequisitionDetails(ID,LoginName);
        }
        public object DeleteRequisitionDetailByID(Guid ID)
        {
            return _requisitionRepository.DeleteRequisitionDetailByID(ID);
        }
        public RequisitionOverViewCount GetRequisitionOverViewCount(string UserName, bool IsAdminORCeo)
        {
            return _requisitionRepository.GetRequisitionOverViewCount(UserName, IsAdminORCeo);
        }
        public Requisition ApproveRequisition(Requisition RequisitionObj, bool IsAdminORCeo)
        {
           return _requisitionRepository.ApproveRequisition(RequisitionObj,IsAdminORCeo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleString"></param>
        /// <param name="descriptionString"></param>
        /// <param name="isCommon"></param>
        /// <param name="CompanyCode"></param>
        public void SendToFCMManager(string titleString, string descriptionString, Boolean isCommon, string CompanyCode = "")
        {
            //Validation

            if (titleString == "" || titleString == null)
                throw new Exception("No title");
            if (descriptionString == "" || descriptionString == null)
                throw new Exception("No description");
            //Sending notification through Firebase Cloud Messaging
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                string to_String = "";
                if (isCommon)
                    to_String = "/topics/common";

                else
                    to_String = "/topics/" + CompanyCode;
                var objNotification = new
                {
                    to = to_String,

                    data = new
                    {
                        title = titleString,
                        body = descriptionString,
                        sound = "default",

                    }
                };
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonNotificationFormat = js.Serialize(objNotification);
                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);

                //Put here the Server key from Firebase
                string FCMServerKey = ConfigurationManager.AppSettings["FCMServerKey"].ToString();
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FCMServerKey));
                //Put here the Sender ID from Firebase
                string FCMSenderID = ConfigurationManager.AppSettings["FCMSenderID"].ToString();
                tRequest.Headers.Add(string.Format("Sender: id={0}", FCMSenderID));

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();
                                tReader.Close();
                                dataStream.Close();
                                tResponse.Close();

                                if (!responseFromFirebaseServer.Contains("message_id"))//Doesn't contain message_id means some error occured
                                    throw new Exception(responseFromFirebaseServer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendToFCMCEO(string titleString, string descriptionString, Boolean isCommon)
        {
            //Validation

            if (titleString == "" || titleString == null)
                throw new Exception("No title");
            if (descriptionString == "" || descriptionString == null)
                throw new Exception("No description");
            //Sending notification through Firebase Cloud Messaging
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                string to_String = "";
                if (isCommon)
                    to_String = "/topics/" + "CEO";
                else
                    to_String = "/topics/common";
                var objNotification = new
                {
                    to = to_String,

                    data = new
                    {
                        title = titleString,
                        body = descriptionString,
                        sound = "default",

                    }
                };
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonNotificationFormat = js.Serialize(objNotification);
                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);

                //Put here the Server key from Firebase
                string FCMServerKey = ConfigurationManager.AppSettings["FCMServerKey"].ToString();
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FCMServerKey));
                //Put here the Sender ID from Firebase
                string FCMSenderID = ConfigurationManager.AppSettings["FCMSenderID"].ToString();
                tRequest.Headers.Add(string.Format("Sender: id={0}", FCMSenderID));

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();
                                tReader.Close();
                                dataStream.Close();
                                tResponse.Close();

                                if (!responseFromFirebaseServer.Contains("message_id"))//Doesn't contain message_id means some error occured
                                    throw new Exception(responseFromFirebaseServer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object UpdateNotification(Requisition requisitionObj)
        {
            Object result = null;
            try
            {

                result = _requisitionRepository.UpdateNotification(requisitionObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}