using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Entities
{
    public class Blogs
    {
        public int ID { get; set; }
        public int BlogCategoryID { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int TotalCount { get; set; }

        public SelectList CategoryList { get; set; }
    }

    public class BlogCategory
    {
        public int ID { get; set; }
        public string BlogCategoryName { get; set; }
    }
}
