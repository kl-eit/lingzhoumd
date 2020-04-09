using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ling.Domains.Abstract
{
    public interface IPagesRepository
    {
        ResponseObjectForAnything HomeUpsert(HomeViewModel pEntity);
        ResponseObjectForAnything SelectHome();
    }
}
