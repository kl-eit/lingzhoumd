using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Common
{
    public class Constants
    {
        public static int PAGE_SIZE = 10;
        
        #region FOR RESPONSE OBJECT STATUS

        public const string RESPONSE_SUCCESS = "SUCCESS";
        public const string RESPONSE_ERROR = "ERROR";
        public const string RESPONSE_EXISTS = "EXISTS";
        
        #endregion
        
    }
    
    #region Enums
    
    public enum UserRoles
    {
        Admin = 1
    }
    
    #endregion
}
