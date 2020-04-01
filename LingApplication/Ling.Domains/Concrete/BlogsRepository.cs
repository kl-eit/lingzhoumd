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
    public class BlogsRepository : DBContext, IBlogsRepository
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
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("Blogs_D");
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
            List<Blogs> entityList = new List<Blogs>();
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("Blogs_S");
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
                            Blogs entity = new Blogs();
                            entity.ID = CommonHelper.FromDB<int>(iReader["ID"]);
                            entity.Slug = CommonHelper.FromDB<string>(iReader["Slug"]);
                            entity.Title = CommonHelper.FromDB<string>(iReader["Title"]);
                            entity.Description = CommonHelper.FromDB<string>(iReader["Description"]);
                            entity.ImageName = CommonHelper.FromDB<string>(iReader["ImageName"]);
                            entity.IsActive = CommonHelper.FromDB<bool>(iReader["IsActive"]);
                            entity.CreatedBy = CommonHelper.FromDB<string>(iReader["CreatedBy"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ModifiedBy = CommonHelper.FromDB<string>(iReader["ModifiedBy"]);
                            entity.ModifiedDate = CommonHelper.FromDB<DateTime>(iReader["ModifiedDate"]);
                            entity.TotalCount = CommonHelper.FromDB<int>(iReader["TotalRecord"]);
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
        /// Get Single Category
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            Blogs entity = null;
            try
            {
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("Blogs_S_By_ID");
                dbStatic.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pID));
                IDataReader iReader = dbStatic.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new Blogs();
                            entity.ID = CommonHelper.FromDB<int>(iReader["ID"]);
                            entity.Slug = CommonHelper.FromDB<string>(iReader["Slug"]);
                            entity.Title = CommonHelper.FromDB<string>(iReader["Title"]);
                            entity.Description = CommonHelper.FromDB<string>(iReader["Description"]);
                            entity.ImageName = CommonHelper.FromDB<string>(iReader["ImageName"]);
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
        public ResponseObjectForAnything Upsert(Blogs pEntity)
        {
            ResponseObjectForAnything responseOjectForAnything = new ResponseObjectForAnything();
            try
            {
                pEntity.CreatedDate = pEntity.ModifiedDate = DateTime.Now;
                DbCommand dbCommand = dbStatic.GetStoredProcCommand("Blogs_Upsert");
                dbStatic.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<int>(pEntity.ID));
                dbStatic.AddInParameter(dbCommand, "@Slug", DbType.String, CommonHelper.ToDB<string>(pEntity.Slug));
                dbStatic.AddInParameter(dbCommand, "@Title", DbType.String, CommonHelper.ToDB<string>(pEntity.Title));
                dbStatic.AddInParameter(dbCommand, "@Description", DbType.String, CommonHelper.ToDB<string>(pEntity.Description));
                dbStatic.AddInParameter(dbCommand, "@ImageName", DbType.String, CommonHelper.ToDB<string>(pEntity.ImageName));
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
