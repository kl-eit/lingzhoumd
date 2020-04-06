using Ling.Common;
using Ling.Domains.Abstract;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Ling.Domains.Concrete
{
    public class WebsiteSettingRepository : DBContext, IWebSiteSettingRepository
    {
        public WebsiteSettingRepository(IConfiguration iConfiguration) : base(iConfiguration)
        {

        }

        public ResponseObjectForAnything Delete(int pID)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();

            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("WebSiteSetting_D");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pID));

                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObjectID = Convert.ToInt32((sqldb.ExecuteScalar(dbCommand)));

            }
            catch (Exception ex)
            {
                responseOjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseOjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "Delete", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseOjectForAnything;
        }

        public ResponseObjectForAnything Select(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            List<WebSiteSetting> entityList = new List<WebSiteSetting>();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("WebSiteSetting_S");
                sqldb.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, CommonHelper.ToDB<Int32>(pPageIndex));
                sqldb.AddInParameter(dbCommand, "@PageSize", DbType.Int32, CommonHelper.ToDB<Int32>(pPageSize));
                sqldb.AddInParameter(dbCommand, "@SearchText", DbType.String, CommonHelper.ToDB<string>(pSearchText));

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            WebSiteSetting entity = new WebSiteSetting();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Name = CommonHelper.FromDB<string>(iReader["Name"]);
                            entity.Value = CommonHelper.FromDB<string>(iReader["Value"]);
                            entity.ModifiedBy = CommonHelper.FromDB<string>(iReader["UserName"]);
                            entity.ModifiedDate = CommonHelper.FromDB<DateTime>(iReader["ModifiedDate"]);
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

        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            WebSiteSetting entity = null;
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("WebSiteSetting_S_By_ID");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pID));

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new WebSiteSetting();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Name = CommonHelper.FromDB<string>(iReader["Name"]);
                            entity.Value = CommonHelper.FromDB<string>(iReader["Value"]);
                        }
                    }
                }

                if (!iReader.IsClosed)
                    iReader.Close();

                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObject = entity;

            }
            catch (Exception ex)
            {
                responseOjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseOjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "SelectByID", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseOjectForAnything;
        }

        public int SelectTotalCount(string pSearchText = "")
        {
            throw new NotImplementedException();
        }

        public ResponseObjectForAnything Upsert(WebSiteSetting pEntity)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("WebSiteSetting_Upsert");

                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.ID));
                sqldb.AddInParameter(dbCommand, "@Name", DbType.String, CommonHelper.ToDB<string>(pEntity.Name));
                sqldb.AddInParameter(dbCommand, "@Value", DbType.String, CommonHelper.ToDB<string>(pEntity.Value));
                sqldb.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.ModifiedBy)));
                sqldb.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.ModifiedDate));

                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObjectID = Convert.ToInt32(sqldb.ExecuteScalar(dbCommand));

                if (responseOjectForAnything.ResultObjectID == -1)
                {
                    responseOjectForAnything.ResultCode = Constants.RESPONSE_EXISTS;
                }

            }
            catch (Exception ex)
            {
                responseOjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseOjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "Upsert", "E");
                ExceptionManagerRepository.PublishException(exLog);

            }
            return responseOjectForAnything;
        }
    }
}
