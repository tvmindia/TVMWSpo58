using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
    public interface IRawMaterialRepository
    {
        List<RawMaterial> GetAllRawMaterial();
        object InsertRawMaterial(RawMaterial rawMaterial);
        object UpdateRawMaterial(RawMaterial rawMaterial);
        object DeleteRawMaterial(Guid ID);
        List<MaterialType> GetAllMaterialType();
    }
}
