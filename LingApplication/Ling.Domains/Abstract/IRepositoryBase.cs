using Ling.Domains.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Domains.Abstract
{
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Method for Insert or Update
        /// </summary>
        /// <param name="pEntity">Entity Object</param>
        /// <returns></returns>
        ResponseObjectForAnything Upsert(T pEntity);

        /// <summary>
        /// Delete record from ID
        /// </summary>
        /// <param name="pID">ID of object to be deleted</param>
        /// <returns></returns>
        ResponseObjectForAnything Delete(int pID);

        /// <summary>
        /// Select Record By ID
        /// </summary>
        /// <param name="pID">ID object to be select</param>
        /// <returns></returns>
        ResponseObjectForAnything SelectByID(int pID);

        /// <summary>
        /// Fetch All Records
        /// </summary>
        /// <returns></returns>
        ResponseObjectForAnything Select(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc");
        
        /// <summary>
        /// Get Total Record Count
        /// </summary>
        /// <param name="pSearchText"></param>
        /// <returns></returns>
        int SelectTotalCount(string pSearchText = "");

        
    }
}
