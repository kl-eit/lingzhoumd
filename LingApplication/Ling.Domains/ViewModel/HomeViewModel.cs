using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.ViewModel
{
    public class HomeViewModel
    {
        //PHILOSOPHY DETAILS
        public string PhilosophyTitle { get; set; }
        public string PhilosophyDescription { get; set; }

        //PROFILE IMAGE DETAILS
        public string ProfileImage{ get; set; }

        //PROFILE EDUCATION DETAILS
        public string EducationTitle { get; set; }
        public string EducationDescription { get; set; }
        public string EducationImage { get; set; }

        //PROFILE Training DETAILS
        public string TrainingTitle { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingImage { get; set; }

        //PROFILE Certificate DETAILS
        public string CertificateTitle { get; set; }
        public string CertificateDescription { get; set; }
        public string CertificateImage { get; set; }

        //PROFILE Medical DETAILS
        public string MedicalTitle { get; set; }
        public string MedicalDescription { get; set; }
        public string MedicalImage { get; set; }
    }
}
