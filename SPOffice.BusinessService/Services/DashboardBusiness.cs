using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
	public class DashboardBusiness:IDashboardBusiness
	{
        IDashboardRepository _dashboardrepository;
        private IEnquiryRepository _enquiryRepository;
        private IFollowUpRepository _followupRepository;
        public DashboardBusiness(IDashboardRepository dashboardrepository, IEnquiryRepository enquiryRepository, IFollowUpRepository followupRepository)
        {
            _dashboardrepository = dashboardrepository;
            _enquiryRepository = enquiryRepository;
            _followupRepository = followupRepository;
        }
        public DashboardStatus GetCountOfEnquiries(string duration)
        {
            return _dashboardrepository.GetCountOfEnquiries(duration);
        }

        public List<Enquiry> GetRecentEnquiryList( string BaseURL) {
            List<Enquiry> result= _enquiryRepository.GetRecentEnquiryList();

            if (result != null)
            {
                foreach (Enquiry m in result)
                {
                 
                    m.URL = BaseURL + m.ID;
                }
            }

            return result;
        }

        public EnquirySummary GetEnquirySummary() {
            EnquirySummary result= _enquiryRepository.GetEnquirySummary();
            if (result != null && result.Total>0)
            {
                result.NotConvertedPercentage = (result.NotConverted * 100 )/ result.Total ;
                result.ConvertedPercentage = (result.Converted *100) / result.Total ;
                result.OpenPercentage = (result.Open*100) / result.Total ;

            }
            return result;
        }


      public  List<FollowUp> GetTodaysFollowUpDetails(DateTime onDate, string BaseURL) {

            List<FollowUp> result = _followupRepository.GetFollowUpDetailsOnDate(onDate);
            if (result != null)
            {
                foreach (FollowUp m in result)
                {

                    m.URL = BaseURL + m.EnquiryID;
                }
            }

            return result;
        }
    }
}