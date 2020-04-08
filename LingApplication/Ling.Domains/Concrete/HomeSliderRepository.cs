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
    public class HomeSliderRepository : DBContext, IHomeSliderRepository
    {
        public HomeSliderRepository(IConfiguration iConfiguration) : base(iConfiguration)
        {

        }

        public ResponseObjectForAnything Delete(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("HomeSlider_D");
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
            List<HomeSlider> entityList = new List<HomeSlider>();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("HomeSlider_S");
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
                            HomeSlider entity = new HomeSlider();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Title = CommonHelper.FromDB<String>(iReader["Title"]);
                            entity.Content = CommonHelper.FromDB<String>(iReader["Content"]);
                            entity.ImageName = CommonHelper.FromDB<String>(iReader["ImageName"]);
                            entity.Video= CommonHelper.FromDB<String>(iReader["Video"]);
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
            HomeSlider model = new HomeSlider();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("HomeSlider_S_By_ID");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));
                IDataReader iReader = sqldb.ExecuteReader(dbCommand);
                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            model.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            model.Title = CommonHelper.FromDB<String>(iReader["Title"]);
                            model.Content = CommonHelper.FromDB<String>(iReader["Content"]);
                            model.ImageName = CommonHelper.FromDB<String>(iReader["ImageName"]);
                            model.Video = CommonHelper.FromDB<String>(iReader["Video"]);
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

        public ResponseObjectForAnything Upsert(HomeSlider pEntity)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                pEntity.CreatedDate = pEntity.ModifiedDate = DateTime.Now;

                DbCommand dbCommand = sqldb.GetStoredProcCommand("HomeSlider_Upsert");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.ID));
                sqldb.AddInParameter(dbCommand, "@Title", DbType.String, CommonHelper.ToDB<String>(pEntity.Title));
                sqldb.AddInParameter(dbCommand, "@Content", DbType.String, CommonHelper.ToDB<String>(pEntity.Content));
                sqldb.AddInParameter(dbCommand, "@Video", DbType.String, CommonHelper.ToDB<String>(pEntity.Video));
                sqldb.AddInParameter(dbCommand, "@ImageName", DbType.String, CommonHelper.ToDB<String>(pEntity.ImageName));
                sqldb.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, CommonHelper.ToDB<Boolean>(pEntity.IsActive));
                sqldb.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.CreatedBy)));
                sqldb.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.CreatedDate));
                sqldb.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.ModifiedBy)));
                sqldb.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.ModifiedDate));

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = CommonHelper.ConvertTo<Int32>(sqldb.ExecuteScalar(dbCommand));

                if (responseObjectForAnything.ResultObjectID == -1)
                {
                    responseObjectForAnything.ResultCode = Constants.ALERT_NAME_EXISTS;
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

        public ResponseObjectForAnything UpdateSortOrderID(string pSortedRowIDs, int pUserID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("HomeSlider_UpdateSortOrderID");
                sqldb.AddInParameter(dbCommand, "@SortedRowIDs", DbType.String, CommonHelper.ToDB<string>(pSortedRowIDs));
                sqldb.AddInParameter(dbCommand, "@UserID", DbType.Int32, CommonHelper.ToDB<Int32>(pUserID));

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = Convert.ToInt32(sqldb.ExecuteScalar(dbCommand));

            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "UpdateSortOrderID", "E");
                ExceptionManagerRepository.PublishException(exLog);

            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything SelectActiveSlides()
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            List<HomeSlider> homeSliderList = new List<HomeSlider>();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("HomeSlider_GetActive");
                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            HomeSlider entity = new HomeSlider();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Title = CommonHelper.FromDB<String>(iReader["Title"]);
                            entity.FileTypeID = CommonHelper.FromDB<Int32>(iReader["FileTypeID"]);
                            entity.ImageName= CommonHelper.FromDB<String>(iReader["ImageName"]);
                            entity.IsActive = CommonHelper.FromDB<Boolean>(iReader["IsActive"]);
                            homeSliderList.Add(entity);
                        }
                    }
                }

                if (!iReader.IsClosed)
                    iReader.Close();

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObject = homeSliderList;
            }
            catch (Exception ex)
            {
                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                responseObjectForAnything.ResultMessage = ex.Message;
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "SelectActiveSlides", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }
    }
}
