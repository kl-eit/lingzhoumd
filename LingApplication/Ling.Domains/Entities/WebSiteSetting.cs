using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Entities
{
    public class WebSiteSetting
    {
        public WebSiteSetting()
        {
            ID = 0;
            Name = string.Empty;
            Value = string.Empty;
            ModifiedBy = string.Empty;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
