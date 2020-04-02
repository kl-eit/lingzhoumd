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

        #region ALERT/OPERATION MODE & MESSAGE

        public static string ALERT_SAVE = "Record saved successfully!";
        public static string ALERT_DELETE = "Record deleted successfully!";
        public static string ALERT_ERROR = "Unable to perform operation!";
        public static string ALERT_EXISTS = "Record already exists!";
        public static string ALERT_EMAIL_EXISTS = "Email address already exists!";
        public static string ALERT_NAME_EXISTS = "Name already exists!";
        public static string ALERT_CURRENT_PASSWORD = "Invalid current password!";
        public static string ALERT_RECORD_IS_IN_USE_DELETE = "Unable to delete due to record is already use in another place!";
        public static string ALERT_REQUEST_PROPOSAL_SAVE = "Proposal submitted successfully!";

        #endregion

        #region Enums

        public enum UserRoles
        {
            Admin = 1
        }

        public class Alert
        {
            public ALERTTYPE AlertType { get; set; }
            public string Message { get; set; }
            public ALERTMESSAGETYPE MessageType { get; set; }
        }

        public enum ALERTTYPE
        {
            None = 0,
            Warning = 1,
            Success = 2,
            Info = 3,
            Error = 4
        }

        public enum ALERTMESSAGETYPE
        {
            OnlyText = 0,
            TextWithClose = 1
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