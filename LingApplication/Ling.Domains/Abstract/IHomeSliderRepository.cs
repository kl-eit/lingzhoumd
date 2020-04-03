using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Abstract
{
    public interface IHomeSliderRepository : IRepositoryBase<HomeSlider>
    {
        ResponseObjectForAnything UpdateSortOrderID(string pSortedRowIDs, int pUserID);

        ResponseObjectForAnything SelectActiveSlides();
    }
}
