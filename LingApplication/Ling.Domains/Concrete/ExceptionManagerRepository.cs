using Ling.Common;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Domains.Concrete
{
    public class ExceptionManagerRepository : DBContext
    {
        public ExceptionManagerRepository(IConfiguration iConfiguration) : base(iConfiguration)
        {
        }

        public static ResponseObjectForAnything PublishException(ExceptionLog pEntity)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                pEntity.CreatedDate = DateTime.Now;
                DbCommand dbcommand = dbStatic.GetStoredProcCommand("ExceptionLog_Upsert");

                dbStatic.AddInParameter(dbcommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.ID));
                dbStatic.AddInParameter(dbcommand, "@ErrorMessage", DbType.String, CommonHelper.ToDB<string>(pEntity.ErrorMessage));
                dbStatic.AddInParameter(dbcommand, "@StackTrace", DbType.String, CommonHelper.ToDB<string>(pEntity.StackTrace));
                dbStatic.AddInParameter(dbcommand, "@ClassName", DbType.String, CommonHelper.ToDB<string>(pEntity.ClassName));
                dbStatic.AddInParameter(dbcommand, "@Methodname", DbType.String, CommonHelper.ToDB<string>(pEntity.MethodName));
                dbStatic.AddInParameter(dbcommand, "@OtherInformation", DbType.String, CommonHelper.ToDB<string>(pEntity.OtherInformation));
                dbStatic.AddInParameter(dbcommand, "@CreatedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.CreatedDate));
                dbStatic.AddInParameter(dbcommand, "@ErrorTypeCode", DbType.String, CommonHelper.ToDB<string>(pEntity.ErrorTypeCode));

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = Convert.ToInt32(dbStatic.ExecuteScalar(dbcommand));
                if (responseObjectForAnything.ResultObjectID == -1)
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONSE_EXISTS;
                }
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
            }
            return responseObjectForAnything;
        }

        /// <summary>
        /// Delete ExceptionLog by ID
        /// </summary>
        /// <param name="pID">ID object for delete</param>
        /// <returns></returns>
        public ResponseObjectForAnything Delete(int pExceptionID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbcommand = dbStatic.GetStoredProcCommand("ExceptionLog_D");

                dbStatic.AddInParameter(dbcommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pExceptionID));

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = Convert.ToInt32(dbStatic.ExecuteScalar(dbcommand));
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "Delete", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything Select(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", string pErrorType = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            List<ExceptionLog> entityList = new List<ExceptionLog>();
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("ExceptionLog_S");
                dbStatic.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, CommonHelper.ToDB<Int32>(pPageIndex));
                dbStatic.AddInParameter(dbCommand, "@PageSize", DbType.Int32, CommonHelper.ToDB<Int32>(pPageSize));
                dbStatic.AddInParameter(dbCommand, "@SearchText", DbType.String, CommonHelper.ToDB<string>(pSearchText));
                dbStatic.AddInParameter(dbCommand, "@ErrorType", DbType.String, CommonHelper.ToDB<string>(pErrorType));
                dbStatic.AddInParameter(dbCommand, "@SortColumn", DbType.Int32, CommonHelper.ToDB<Int32>(pOrderColumn));
                dbStatic.AddInParameter(dbCommand, "@SortOrder", DbType.String, CommonHelper.ToDB<String>(pCurrentOrder));
                IDataReader iReader = dbStatic.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            ExceptionLog entity = new ExceptionLog();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.ErrorMessage = CommonHelper.FromDB<string>(iReader["ErrorMessage"]);
                            entity.StackTrace = CommonHelper.FromDB<String>(iReader["StackTrace"]);
                            entity.ClassName = CommonHelper.FromDB<string>(iReader["ClassName"]);
                            entity.MethodName = CommonHelper.FromDB<string>(iReader["MethodName"]);
                            entity.OtherInformation = CommonHelper.FromDB<string>(iReader["OtherInformation"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ErrorTypeCode = CommonHelper.FromDB<string>(iReader["ErrorTypeCode"]);
                            entity.TotalCount = CommonHelper.FromDB<Int32>(iReader["TotalRecord"]);
                            entityList.Add(entity);
                        }
                    }
                }

                if (!iReader.IsClosed)
                    iReader.Close();

                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObject = entityList;
            }
            catch (Exception ex)
            {
                responseOjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseOjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "Select", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseOjectForAnything;
        }
        
        /// <summary>
        /// Fetch ExceptionLog by ID
        /// </summary>
        /// <param name="pID">ID object foe select</param>
        /// <returns></returns>
        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            ExceptionLog entity = null;
            try
            {
                DbCommand dbcommand = dbStatic.GetStoredProcCommand("ExceptionLog_S_By_ID");
                dbStatic.AddInParameter(dbcommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));
                IDataReader iReader = dbStatic.ExecuteReader(dbcommand);
                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new ExceptionLog();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.ErrorMessage = CommonHelper.FromDB<string>(iReader["ErrorMessage"]);
                            entity.StackTrace = CommonHelper.FromDB<String>(iReader["StackTrace"]);
                        }
                    }
                }
                if (!iReader.IsClosed)
                {
                    iReader.Close();
                }
                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObject = entity;
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "SelectByID", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }
    }
}
