﻿using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface IProformaBusiness
    {
        List<Proforma> GetProformaDetails(string duration);
    }
}
