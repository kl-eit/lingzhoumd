using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Abstract
{
    public interface IFAQRepository : IRepositoryBase<FAQ>
    {
        ResponseObjectForAnything UpdateSortOrderID(string pSortedRowIDs, int pUserID);
    }
}
