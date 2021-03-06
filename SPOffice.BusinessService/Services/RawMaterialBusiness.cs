﻿using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;

namespace SPOffice.BusinessService.Services
{
    public class RawMaterialBusiness : IRawMaterialBusiness
    {
        IRawMaterialRepository _rawMaterialRepository;
        public RawMaterialBusiness(IRawMaterialRepository rawMaterialRepository)
        {
            _rawMaterialRepository = rawMaterialRepository;
        }

        public object DeleteRawMaterial(Guid ID)
        {
            return _rawMaterialRepository.DeleteRawMaterial(ID);
        }

        public List<RawMaterial> GetAllRawMaterial(string Type)
        {
            List<RawMaterial> RawMaterialList = null;
            try
            {
                RawMaterialList = _rawMaterialRepository.GetAllRawMaterial(Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RawMaterialList;
        }

        public RawMaterial GetRawMaterialDetails(Guid ID)
        {
            List<RawMaterial> RawMaterialList = null;
            RawMaterial rawMaterial = null;
            try
            {
                RawMaterialList = GetAllRawMaterial(null);
                rawMaterial = RawMaterialList != null ? RawMaterialList.Where(D => D.ID == ID).SingleOrDefault() : null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rawMaterial;
        }

        public object InsertRawMaterial(RawMaterial rawMaterial)
        {
            return _rawMaterialRepository.InsertRawMaterial(rawMaterial);
        }

        public object UpdateRawMaterial(RawMaterial rawMaterial)
        {
            return _rawMaterialRepository.UpdateRawMaterial(rawMaterial);
        }

        public List<MaterialType> GetAllMaterialType()
        {
            List<MaterialType> materialList = null;
            try
            {
                materialList = _rawMaterialRepository.GetAllMaterialType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return materialList;
        }

    }
}