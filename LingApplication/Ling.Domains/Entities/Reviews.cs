using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Entities
{
    public class Reviews
    {
        public int ID { get; set; }
        public string Review { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int TotalCount { get; set; }
    }
}
