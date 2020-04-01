using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Common
{
    public class Constants
    {
        // Admin List Page Size
        public static int PAGE_SIZE = 10;
        public static string IMAGE_NOT_FOUND = "/assets/images/no-image.png";
        public static string USER_IMAGE_NOT_FOUND = "/assets/images/user-placeholder-image.jpg";


        #region FOR RESPONSE OBJECT STATUS

        public const string RESPONSE_SUCCESS = "SUCCESS";
        public const string RESPONSE_ERROR = "ERROR";
        public const string RESPONSE_EXISTS = "EXISTS";
        public const string RESPONCE_EMAIL_EXISTS = "EMAILEXISTS";
        public const string RESPONSE_INVALID = "INVALID";

        #endregion

        #region REMEMBERME

        public static string REMEMBERMEUSERNAME = "REMEMBERMEUSERNAME";
        public static string REMEMBERMEPASSWORD = "REMEMBERMEPASSWORD";

        #endregion

        #region Enums

        public enum UserRoles
        {
            Admin = 1
        }
        #endregion

        #region USER SESSION VARIABLES

        public static string USERSESSION_USERID = "USERSESSION_USERID";
        public static string USERSESSION_USEREMAIL = "USERSESSION_USEREMAIL";
        public static string USERSESSION_USERNAME = "USERSESSION_USERNAME";
        public static string USERSESSION_USERFIRSTNAME = "USERSESSION_USERFIRSTNAME";
        public static string USERSESSION_USERLASTNAME = "USERSESSION_USERLASTNAME";
        public static string USERSESSION_USERAVATAR = "USERSESSION_USERAVATAR";
        public static string USERSESSION_USERROLE = "USERSESSION_USERROLE";
        public static string USERSESSION_USERROLEID = "USERSESSION_USERROLEID";

        #endregion
    }
}