using Ling.Common;
using Ling.Domains.Abstract;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Ling.Domains.Concrete
{
    public class CMSRepository : DBContext, ICMSRepository
    {
        /// <summary>
        /// To delete Page
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public ResponseObjectForAnything Delete(int pID)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();

            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("CMS_D");
                dbStatic.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pID));
                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObjectID = Convert.ToInt32((dbStatic.ExecuteScalar(dbCommand)));

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

        /// <summary>
        /// To select All Pages
        /// </summary>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pSearchText"></param>
        /// <param name="pOrderColumn"></param>
        /// <param name="pCurrentOrder"></param>
        /// <returns></returns>
        public ResponseObjectForAnything Select(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            List<CMS> entityList = new List<CMS>();
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("CMS_S");
                sqldb.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, CommonHelper.ToDB<Int32>(pPageIndex));
                sqldb.AddInParameter(dbCommand, "@PageSize", DbType.Int32, CommonHelper.ToDB<Int32>(pPageSize));
                sqldb.AddInParameter(dbCommand, "@SearchText", DbType.String, CommonHelper.ToDB<String>(pSearchText));
                sqldb.AddInParameter(dbCommand, "@SortColumn", DbType.Int32, CommonHelper.ToDB<Int32>(pOrderColumn));
                sqldb.AddInParameter(dbCommand, "@SortOrder", DbType.String, CommonHelper.ToDB<String>(pCurrentOrder));
                IDataReader iReader = dbStatic.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            CMS entity = new CMS();
                            entity.ID = CommonHelper.FromDB<int>(iReader["ID"]);
                            entity.CMSKey = CommonHelper.FromDB<string>(iReader["CMSKey"]);
                            entity.Title = CommonHelper.FromDB<string>(iReader["Title"]);
                            entity.Content = CommonHelper.FromDB<string>(iReader["Content"]);
                            entity.IsActive = CommonHelper.FromDB<bool>(iReader["IsActive"]);
                            entity.CreatedBy = CommonHelper.FromDB<string>(iReader["CreatedBy"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ModifiedBy = CommonHelper.FromDB<string>(iReader["ModifiedBy"]);
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

        /// <summary>
        /// GET DATA BY CMS KEY TO DISPLAY IT ON FRONT SIDE
        /// </summary>
        /// <param name="pCmskey"></param>
        /// <returns></returns>
        public ResponseObjectForAnything SelectByCMSKey(string pCmskey)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            CMS entity = null;
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("CMS_S_By_CMSKey");
                dbStatic.AddInParameter(dbCommand, "@CMSKey", DbType.String, CommonHelper.ToDB<string>(pCmskey));

                IDataReader iReader = dbStatic.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new CMS();
                            entity.ID = CommonHelper.FromDB<int>(iReader["ID"]);
                            entity.CMSKey = CommonHelper.FromDB<string>(iReader["CMSKey"]);
                            entity.Title = CommonHelper.FromDB<string>(iReader["Title"]);
                            entity.Content = CommonHelper.FromDB<string>(iReader["Content"]);
                            entity.IsActive = CommonHelper.FromDB<bool>(iReader["IsActive"]);
                            entity.CreatedBy = CommonHelper.FromDB<string>(iReader["CreatedBy"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ModifiedBy = CommonHelper.FromDB<string>(iReader["ModifiedBy"]);
                            entity.ModifiedDate = CommonHelper.FromDB<DateTime>(iReader["ModifiedDate"]);
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
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "SelectByCMSKey", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseOjectForAnything;
        }

        public ResponseObjectForAnything SelectActiveCMSKey()
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            List<CMS> cmsList = new List<CMS>();
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("CMS_S_Web");
                IDataReader iReader = dbStatic.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            CMS entity = new CMS();
                            entity.ID = CommonHelper.FromDB<int>(iReader["ID"]);
                            entity.CMSKey = CommonHelper.FromDB<string>(iReader["CMSKey"]);
                            entity.Title = CommonHelper.FromDB<string>(iReader["Title"]);
                            entity.Content = CommonHelper.FromDB<string>(iReader["Content"]);
                            entity.IsActive = CommonHelper.FromDB<bool>(iReader["IsActive"]);
                            entity.CreatedBy = CommonHelper.FromDB<string>(iReader["CreatedBy"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ModifiedBy = CommonHelper.FromDB<string>(iReader["ModifiedBy"]);
                            entity.ModifiedDate = CommonHelper.FromDB<DateTime>(iReader["ModifiedDate"]);
                            cmsList.Add(entity);
                        }
                    }
                }

                if (!iReader.IsClosed)
                    iReader.Close();

                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObject = cmsList;

            }
            catch (Exception ex)
            {
                responseOjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseOjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "SelectActiveCMSKey", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseOjectForAnything;
        }

        /// <summary>
        /// Get Single Category
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            CMS entity = null;
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("CMS_S_By_ID");
                dbStatic.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pID));
                IDataReader iReader = dbStatic.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new CMS();
                            entity.ID = CommonHelper.FromDB<int>(iReader["ID"]);
                            entity.CMSKey = CommonHelper.FromDB<string>(iReader["CMSKey"]);
                            entity.Title = CommonHelper.FromDB<string>(iReader["Title"]);
                            entity.Content = CommonHelper.FromDB<string>(iReader["Content"]);
                            entity.IsActive = CommonHelper.FromDB<bool>(iReader["IsActive"]);
                            entity.CreatedBy = CommonHelper.FromDB<string>(iReader["CreatedBy"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ModifiedBy = CommonHelper.FromDB<string>(iReader["ModifiedBy"]);
                            entity.ModifiedDate = CommonHelper.FromDB<DateTime>(iReader["ModifiedDate"]);
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

        /// <summary>
        /// Insert or Update Category
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public ResponseObjectForAnything Upsert(CMS pEntity)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            try
            {
                pEntity.CreatedDate = pEntity.ModifiedDate = DateTime.Now;
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("CMS_Upsert");
                dbStatic.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pEntity.ID));
                dbStatic.AddInParameter(dbCommand, "@CMSKey", DbType.String, CommonHelper.ToDB<string>(pEntity.CMSKey));
                dbStatic.AddInParameter(dbCommand, "@Title", DbType.String, CommonHelper.ToDB<string>(pEntity.Title));
                dbStatic.AddInParameter(dbCommand, "@Content", DbType.String, CommonHelper.ToDB<string>(pEntity.Content));
                dbStatic.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, CommonHelper.ToDB<bool>(pEntity.IsActive));
                dbStatic.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.CreatedBy)));
                dbStatic.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.CreatedDate));
                dbStatic.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.ModifiedBy)));
                dbStatic.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.ModifiedDate));

                responseOjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseOjectForAnything.ResultObjectID = Convert.ToInt32((dbStatic.ExecuteScalar(dbCommand)));
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
