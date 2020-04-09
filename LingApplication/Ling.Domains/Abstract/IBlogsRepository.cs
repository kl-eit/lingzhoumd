using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Abstract
{
    public interface IBlogsRepository : IRepositoryBase<Blogs>
    {
        ResponseObjectForAnything GetBlogCategoryList();
        ResponseObjectForAnything SaveBlogCategory(BlogCategory pEntity);
    }
}
