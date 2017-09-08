using System;
using System.Collections.Generic;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System.Linq;

namespace SPOffice.BusinessService.Services
{
    public class CourierBusiness : ICourierBusiness
    {
        ICourierRepository _courierRepository;
        public CourierBusiness(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }
        public object DeleteCourierAgency(string Code)
        {
            return _courierRepository.DeleteCourierAgency(Code);
        }

        public List<CourierAgency> GetAllCourierAgency()
        {
            List<CourierAgency> CourierAgencyList = null;
            try
            {
                CourierAgencyList = _courierRepository.GetAllCourierAgency();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CourierAgencyList;
        }

       

        public CourierAgency GetCourierAgencyDetails(string Code)
        {
            List<CourierAgency> CourierAgencyList = null;
            CourierAgency courierAgency = null;
            try
            {
                CourierAgencyList = GetAllCourierAgency();
                courierAgency = CourierAgencyList != null ? CourierAgencyList.Where(D => D.Code == Code).SingleOrDefault() : null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return courierAgency;
        }

        public object InsertCourierAgency(CourierAgency courierAgency)
        {
            return _courierRepository.InsertCourierAgency(courierAgency);
        }

        public object UpdateCourierAgency(CourierAgency courierAgency)
        {
            return _courierRepository.UpdateCourierAgency(courierAgency);
        }
        public List<Courier> GetAllCouriers()
        {
            List<Courier> CourierList = null;
            try
            {
                CourierList = _courierRepository.GetAllCouriers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CourierList;
        }

        public Courier GetCourierDetails(Guid ID)
        {
            List<Courier> CourierList = null;
            Courier courier = null;
            try
            {
                CourierList = GetAllCouriers();
                courier = CourierList != null ? CourierList.Where(D => D.ID == ID).SingleOrDefault() : null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return courier;
        }

        public object InsertCourier(Courier courier)
        {
            return _courierRepository.InsertCourier(courier);
        }

        public object UpdateCourier(Courier courier)
        {
            return _courierRepository.UpdateCourier(courier);
        }

        public object DeleteCourier(Guid ID)
        {
            return _courierRepository.DeleteCourier(ID);
        }
    }
}