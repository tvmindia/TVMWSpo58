﻿using System;
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
    }
}