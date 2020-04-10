using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Abstract
{
    public interface ICMSRepository : IRepositoryBase<CMS>
    {
        ResponseObjectForAnything SelectByCMSKey(string pCmskey);

        ResponseObjectForAnything SelectActiveCMSKey();

        ResponseObjectForAnything ProfileUpsert(ProfileViewModel pEntity);

        ResponseObjectForAnything SelectProfile();
    }
}
