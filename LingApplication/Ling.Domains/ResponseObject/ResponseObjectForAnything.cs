using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Domains.ResponseObject
{
    public class ResponseObjectForAnything
    {
        #region Properties

        /// <summary>
        /// Result code either Success/Error.
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// Error message to be returned in case
        /// of error.
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// Notes returned as part of the result.
        /// </summary>
        public int ResultObjectID { get; set; }
        /// <summary>
        /// Record count of the result object.
        /// </summary>
        public object ResultObject { get; set; }
        
        #endregion
    }
}
