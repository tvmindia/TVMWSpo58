using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
     public  interface IRawMaterialBusiness
    {
        List<RawMaterial> GetAllRawMaterial();
        RawMaterial GetRawMaterialDetails(Guid ID);
        object InsertRawMaterial(RawMaterial rawMaterial);
        object UpdateRawMaterial(RawMaterial rawMaterial);
        object DeleteRawMaterial(Guid ID);
        List<MaterialType> GetAllMaterialType();
    }
}
