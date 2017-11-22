using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;

namespace SPOffice.BusinessService.Services
{
    public class UnitsBusiness : IUnitsBusiness
    {
        IUnitsRepository _unitsRepository;
        public UnitsBusiness(IUnitsRepository unitsRepository)
        {
            _unitsRepository = unitsRepository;
        }

        public object DeleteUnits(string code)
        {
            return _unitsRepository.DeleteUnits(code);
        }

        public List<Units> GetAllUnits()
        {
            return _unitsRepository.GetAllUnits();
        }

        public Units GetUnitsDetails(string code)
        {
            List<Units> List = null;
            Units rawMaterial = null;
            try
            {
                List = GetAllUnits();
                rawMaterial = List != null ? List.Where(D => D.UnitsCode == code).SingleOrDefault() : null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rawMaterial;

        }

        public object InsertUnits(Units unitsObj)
        {
            return _unitsRepository.InsertUnits(unitsObj);

        }

        public object UpdateUnits(Units unitsObj)
        {
            return _unitsRepository.UpdateUnits(unitsObj);
        }
    }
}