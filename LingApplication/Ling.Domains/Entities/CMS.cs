using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Entities
{
    public class CMS
    {
        public CMS()
        {
            ID = 0;
            CMSKey = string.Empty;
            Title = string.Empty;
            Content = string.Empty;
            IsActive = false;
            CreatedBy = string.Empty;
            ModifiedBy = string.Empty;

        }

        public int ID { get; set; }
        public string CMSKey { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
