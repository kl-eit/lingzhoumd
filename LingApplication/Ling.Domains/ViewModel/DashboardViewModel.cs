using Ling.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.ViewModel
{
    public class DashboardViewModel
    {
        public int TotalFAQ { get; set; }
        public int TotalContactInquiry { get; set; }
        public int TotalBlog { get; set; }
        public List<ContactInquiry> ContactInquiryList { get; set; }

        public DashboardViewModel()
        {
            ContactInquiryList = new List<ContactInquiry>();
        }
    }

}
