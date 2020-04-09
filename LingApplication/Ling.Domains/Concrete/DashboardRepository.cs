using Ling.Common;
using Ling.Domains.Abstract;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Ling.Domains.Concrete
{
    public class DashboardRepository : DBContext, IDashboardRepository
    {
        public DashboardRepository(IConfiguration iConfiguration) : base(iConfiguration)
        {

        }
        
        public ResponseObjectForAnything GetContactInquiry(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            List<ContactInquiry> entityList = new List<ContactInquiry>();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("ConactInquiry_S");
                sqldb.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, CommonHelper.ToDB<Int32>(pPageIndex));
                sqldb.AddInParameter(dbCommand, "@PageSize", DbType.Int32, CommonHelper.ToDB<Int32>(pPageSize));
                sqldb.AddInParameter(dbCommand, "@SearchText", DbType.String, CommonHelper.ToDB<String>(pSearchText));
                sqldb.AddInParameter(dbCommand, "@SortColumn", DbType.Int32, CommonHelper.ToDB<Int32>(pOrderColumn));
                sqldb.AddInParameter(dbCommand, "@SortOrder", DbType.String, CommonHelper.ToDB<String>(pCurrentOrder));

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            ContactInquiry entity = new ContactInquiry();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Name = CommonHelper.FromDB<String>(iReader["Name"]);
                            entity.EmailAddress = CommonHelper.FromDB<String>(iReader["EmailAddress"]);
                            entity.Subject = CommonHelper.FromDB<String>(iReader["Subject"]);
                            entity.Message = CommonHelper.FromDB<string>(iReader["Message"]);
                            entity.Status = CommonHelper.FromDB<bool>(iReader["Status"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.TotalCount = CommonHelper.FromDB<int>(iReader["TotalRecord"]);
                            entityList.Add(entity);
                        }
                    }
                }

                if (!iReader.IsClosed)
                    iReader.Close();

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObject = entityList;
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "GetContactInquiry", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything FAQ_Inquiry_Blog_Count()
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            DashboardViewModel entity = new DashboardViewModel(); ;
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("Dashboard_S");

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity.TotalFAQ = CommonHelper.FromDB<Int32>(iReader["TotalFAQ"]);
                            entity.TotalContactInquiry = CommonHelper.FromDB<Int32>(iReader["TotalContactInquiry"]);
                            entity.TotalBlog = CommonHelper.FromDB<Int32>(iReader["TotalBlog"]);
                        }
                    }
                }

                if (!iReader.IsClosed)
                    iReader.Close();

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObject = entity;
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "FAQ_Inquiry_Blog_Count", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        /// <summary>
        /// Fetch ExceptionLog by ID
        /// </summary>
        /// <param name="pID">ID object foe select</param>
        /// <returns></returns>
        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            ContactInquiry entity = null;
            try
            {
                DbCommand dbcommand = dbStatic.GetStoredProcCommand("ContactInquiry_S_By_ID");
                dbStatic.AddInParameter(dbcommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));
                IDataReader iReader = dbStatic.ExecuteReader(dbcommand);
                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new ContactInquiry();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Message= CommonHelper.FromDB<String>(iReader["Message"]);
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

        public ResponseObjectForAnything UpdateStatusByID(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbcommand = dbStatic.GetStoredProcCommand("ConactInquiry_Status_U");
                dbStatic.AddInParameter(dbcommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));
                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = CommonHelper.ConvertTo<Int32>(sqldb.ExecuteScalar(dbcommand));
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
