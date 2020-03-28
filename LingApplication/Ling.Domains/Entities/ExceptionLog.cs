using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Domains.Entities
{
    public class ExceptionLog
    {
        public int ID { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string OtherInformation { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ErrorTypeCode { get; set; }
        public int TotalCount { get; set; }

        public ExceptionLog()
        {
            ID = 0;
            ErrorMessage = string.Empty;
            StackTrace = string.Empty;
            ClassName = string.Empty;
            MethodName = string.Empty;
            OtherInformation = string.Empty;
            ErrorTypeCode = string.Empty;
        }

        public ExceptionLog(string errorMessage, string stackTrace, string className, string methodName, string errorTypeCode)
        {
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
            ClassName = className;
            MethodName = methodName;
            ErrorTypeCode = errorTypeCode;
        }

        public ExceptionLog(string errorMessage, string stackTrace, string className, string methodName, string otherInformation, string errorTypeCode)
        {
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
            ClassName = className;
            MethodName = methodName;
            OtherInformation = otherInformation;
            ErrorTypeCode = ErrorTypeCode;
        }
    }
}
