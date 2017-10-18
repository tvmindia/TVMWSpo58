﻿using SPOffice.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOffice.RepositoryServices.Contracts
{
   public interface IEnquirySourceRepository
    {
        List<EnquirySource> GetAllEnquirySourceList();
    }
}
