using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Domains.Abstract
{
    public interface IUserRepository: IRepositoryBase<Users>
    {
        ResponseObjectForAnything Authentication(string pUserName, string pPassword);
    }
}
