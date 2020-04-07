using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Entities
{
    public class ContactInquiry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalCount { get; set; }
    }
}
