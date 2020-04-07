using Ling.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.ViewModel
{
    public class DashboardViewModel
    {
        public List<ContactInquiry> ContactInquiryList { get; set; }

        public DashboardViewModel()
        {
            ContactInquiryList = new List<ContactInquiry>();
        }
    }

}
