using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Abstract
{
    public interface IDashboardRepository : IRepositoryBase<DashboardViewModel>
    {
        ResponseObjectForAnything FAQ_Inquiry_Blog_Count();
        ResponseObjectForAnything GetContactInquiry(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc");
    }
}
