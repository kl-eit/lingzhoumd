using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Entities
{
    public class HomeSlider
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Video { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int TotalCount { get; set; }
    }
}
