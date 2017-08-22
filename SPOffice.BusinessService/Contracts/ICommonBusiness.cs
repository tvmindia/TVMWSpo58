﻿using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.BusinessService.Contracts
{
    public interface ICommonBusiness
    {
        UA GetUA();
        string ConvertCurrency(decimal value, int DecimalPoints = 0, bool Symbol = true);
        
    }
}
