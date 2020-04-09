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
    public class BlogsRepository : DBContext, IBlogsRepository
    {
        public BlogsRepository(IConfiguration iConfiguration) : base(iConfiguration)
        {

        }

        public ResponseObjectForAnything Delete(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("Blogs_D");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = Convert.ToInt32((sqldb.ExecuteScalar(dbCommand)));

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

        public ResponseObjectForAnything Select(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            List<Blogs> entityList = new List<Blogs>();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("[eitlingzhaoumd].[Blogs_S]");
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
                            Blogs entity = new Blogs();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Slug = CommonHelper.FromDB<String>(iReader["Slug"]);
                            entity.Title = CommonHelper.FromDB<String>(iReader["Title"]);
                            entity.Description = CommonHelper.FromDB<String>(iReader["Description"]);
                            entity.IsActive = CommonHelper.FromDB<Boolean>(iReader["IsActive"]);
                            entity.CreatedBy = CommonHelper.FromDB<String>(iReader["CreatedBy"]);
                            entity.CreatedDate = CommonHelper.FromDB<DateTime>(iReader["CreatedDate"]);
                            entity.ModifiedBy = CommonHelper.FromDB<String>(iReader["ModifiedBy"]);
                            entity.ModifiedDate = CommonHelper.FromDB<DateTime>(iReader["ModifiedDate"]);
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
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "Select", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            Blogs model = new Blogs();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("Blogs_S_By_ID");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));
                IDataReader iReader = sqldb.ExecuteReader(dbCommand);
                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            model.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            model.Slug = CommonHelper.FromDB<String>(iReader["Slug"]);
                            model.Title = CommonHelper.FromDB<String>(iReader["Title"]);
                            model.Description = CommonHelper.FromDB<String>(iReader["Description"]);
                            model.ImageName = CommonHelper.FromDB<String>(iReader["ImageName"]);
                            model.IsActive = CommonHelper.FromDB<Boolean>(iReader["IsActive"]);
                        }
                    }
                }
                if (!iReader.IsClosed)
                    iReader.Close();

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObject = model;
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

        public int SelectTotalCount(string pSearchText = "")
        {
            throw new NotImplementedException();
        }

        public ResponseObjectForAnything Upsert(Blogs pEntity)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                pEntity.CreatedDate = pEntity.ModifiedDate = DateTime.Now;

                DbCommand dbCommand = sqldb.GetStoredProcCommand("Blogs_Upsert");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.ID));
                sqldb.AddInParameter(dbCommand, "@BlogCategoryID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.BlogCategoryID));
                sqldb.AddInParameter(dbCommand, "@Slug", DbType.String, CommonHelper.ToDB<String>(pEntity.Slug));
                sqldb.AddInParameter(dbCommand, "@Title", DbType.String, CommonHelper.ToDB<String>(pEntity.Title));
                sqldb.AddInParameter(dbCommand, "@Description", DbType.String, CommonHelper.ToDB<String>(pEntity.Description));
                sqldb.AddInParameter(dbCommand, "@ImageName", DbType.String, CommonHelper.ToDB<String>(pEntity.ImageName));
                sqldb.AddInParameter(dbCommand, "@MetaTitle", DbType.String, CommonHelper.ToDB<String>(pEntity.MetaTitle));
                sqldb.AddInParameter(dbCommand, "@MetaDescription", DbType.String, CommonHelper.ToDB<String>(pEntity.MetaDescription));
                sqldb.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, CommonHelper.ToDB<Boolean>(pEntity.IsActive));
                sqldb.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.CreatedBy)));
                sqldb.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.CreatedDate));
                sqldb.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.ModifiedBy)));
                sqldb.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.ModifiedDate));

                responseObjectForAnything.ResultObjectID = CommonHelper.ConvertTo<Int32>(sqldb.ExecuteScalar(dbCommand));
                if (responseObjectForAnything.ResultObjectID == -1)
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONSE_EXISTS;
                }
                else
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "Upsert", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything GetBlogCategoryList()
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            List<BlogCategory> entityList = new List<BlogCategory>();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("BlogCategory_S");

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);
                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            BlogCategory model = new BlogCategory();
                            model.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            model.BlogCategoryName = CommonHelper.FromDB<String>(iReader["BlogCategoryName"]);
                            entityList.Add(model);
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
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "GetBlogCategoryList", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything SaveBlogCategory(BlogCategory pEntity)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("BlogCategory_Upsert");
                sqldb.AddInParameter(dbCommand, "@BlogCategoryName", DbType.String, CommonHelper.ToDB<String>(pEntity.BlogCategoryName));
                sqldb.AddInParameter(dbCommand, "@CreatedBy", DbType.String, CommonHelper.ToDB<String>(pEntity.CreatedBy));
                
                responseObjectForAnything.ResultObjectID = CommonHelper.ConvertTo<Int32>(sqldb.ExecuteScalar(dbCommand));
                if (responseObjectForAnything.ResultObjectID == -1)
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONSE_EXISTS;
                }
                else
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "SaveBlogCategory", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }
    }
}
