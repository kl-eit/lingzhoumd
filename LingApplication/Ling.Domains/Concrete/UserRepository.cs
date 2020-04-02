using Ling.Domains.Abstract;
using System;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using System.Data.Common;
using System.Data;
using Ling.Common;
using Microsoft.Extensions.Configuration;

namespace Ling.Domains.Concrete
{
    public class UserRepository : DBContext, IUserRepository
    {
        public string _connectionString = "";
        public UserRepository(IConfiguration iConfiguration) : base(iConfiguration)
        {

        }

        public ResponseObjectForAnything Authentication(string pUserName, string pPassword)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            //UserLoginViewModel entity = null;
            //int recordCount = 0;
            //try
            //{
            //    DbCommand dbCommand = sqldb.GetStoredProcCommand("User_Authentication");
            //    sqldb.AddInParameter(dbCommand, "@UserName", DbType.String, CommonHelper.ToDB<String>(pUserName));
            //    sqldb.AddInParameter(dbCommand, "@Password", DbType.String, CommonHelper.ToDB<String>(pPassword));

            //    IDataReader iReader = sqldb.ExecuteReader(dbCommand);

            //    if (!iReader.Equals(null))
            //    {
            //        using (iReader)
            //        {
            //            while (iReader.Read())
            //            {
            //                entity = new UserLoginViewModel();
            //                entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);

            //                if (entity.ID <= 0)
            //                {
            //                    responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
            //                    responseObjectForAnything.ResultMessage = "Login Failed. Please try again!";
            //                    return responseObjectForAnything;
            //                }

            //                entity.Name = CommonHelper.FromDB<String>(iReader["Name"]);
            //                entity.UserName = CommonHelper.FromDB<String>(iReader["UserName"]);
            //                entity.Email = CommonHelper.FromDB<String>(iReader["Email"]);
            //                entity.Role = CommonHelper.FromDB<String>(iReader["Role"]);
            //                entity.Avatar = CommonHelper.FromDB<String>(iReader["Avatar"]);
            //                recordCount++;
            //            }
            //            if (recordCount == 0)
            //            {
            //                responseObjectForAnything.ResultCode = Constants.RESPONSE_INVALID;
            //                responseObjectForAnything.ResultMessage = "You are not active user!!";
            //                return responseObjectForAnything;
            //            }
            //        }
            //    }

            //    if (!iReader.IsClosed)
            //        iReader.Close();

            //    responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
            //    responseObjectForAnything.ResultObject = entity;

            //}
            //catch (Exception ex)
            //{
            //    responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
            //    responseObjectForAnything.ResultMessage = ex.Message;
            //    ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "UserAuthentication", "E");
            //    ExceptionManagerRepository.PublishException(exLog);
            //}
            return responseObjectForAnything;

        }

        public ResponseObjectForAnything Delete(int pID)
        {
            throw new NotImplementedException();
        }

        public ResponseObjectForAnything Select(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            throw new NotImplementedException();
        }

        public ResponseObjectForAnything SelectByID(int pID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            UserProfileViewModel model = new UserProfileViewModel();
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("[eitlingzhaoumd].[User_S_By_ID]");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pID));
                IDataReader iReader = sqldb.ExecuteReader(dbCommand);
                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            model.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            model.RoleID = CommonHelper.FromDB<Int32>(iReader["RoleID"]);
                            model.Name = CommonHelper.FromDB<String>(iReader["Name"]);
                            model.UserName = CommonHelper.FromDB<String>(iReader["UserName"]);
                            model.PhoneNumber = CommonHelper.FromDB<String>(iReader["PhoneNumber"]);
                            model.Email = CommonHelper.FromDB<String>(iReader["Email"]);
                            model.Avatar = CommonHelper.FromDB<String>(iReader["Avatar"]);
                            model.RoleName = CommonHelper.FromDB<String>(iReader["RoleName"]);
                            model.Password = CommonHelper.FromDB<String>(iReader["Password"]);
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

        public ResponseObjectForAnything Upsert(Users pEntity)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            try
            {
                pEntity.CreatedDate = pEntity.ModifiedDate = DateTime.Now;

                DbCommand dbCommand = sqldb.GetStoredProcCommand("[eitlingzhaoumd].[User_Upsert]");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.ID));
                sqldb.AddInParameter(dbCommand, "@RoleID", DbType.Int32, CommonHelper.ToDB<Int32>(pEntity.RoleID));
                sqldb.AddInParameter(dbCommand, "@Name", DbType.String, CommonHelper.ToDB<String>(pEntity.Name));
                sqldb.AddInParameter(dbCommand, "@UserName", DbType.String, CommonHelper.ToDB<String>(pEntity.UserName));
                sqldb.AddInParameter(dbCommand, "@Email", DbType.String, CommonHelper.ToDB<String>(pEntity.Email));
                sqldb.AddInParameter(dbCommand, "@Password", DbType.String, CommonHelper.ToDB<String>(pEntity.Password));
                sqldb.AddInParameter(dbCommand, "@PhoneNumber", DbType.String, CommonHelper.ToDB<String>(pEntity.PhoneNumber));
                sqldb.AddInParameter(dbCommand, "@Avatar", DbType.String, CommonHelper.ToDB<String>(pEntity.Avatar));
                sqldb.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, CommonHelper.ToDB<Boolean>(pEntity.IsActive));
                sqldb.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.CreatedBy)));
                sqldb.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.CreatedDate));
                sqldb.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, CommonHelper.ToDB<Int32>(Convert.ToInt32(pEntity.ModifiedBy)));
                sqldb.AddInParameter(dbCommand, "@ModifiedDate", DbType.DateTime, CommonHelper.ToDB<DateTime>(pEntity.ModifiedDate));

                responseObjectForAnything.ResultCode = Constants.RESPONSE_SUCCESS;
                responseObjectForAnything.ResultObjectID = CommonHelper.ConvertTo<Int32>(sqldb.ExecuteScalar(dbCommand));
                if (responseObjectForAnything.ResultObjectID == -1)
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONSE_EXISTS;
                }
                if (responseObjectForAnything.ResultObjectID == -2)
                {
                    responseObjectForAnything.ResultCode = Constants.RESPONCE_EMAIL_EXISTS;
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

        public ResponseObjectForAnything UserAuthentication(string pUsername, string pEncryptedPassword)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            UserLoginViewModel entity = null;
            int recordCount = 0;
            try
            {

                DbCommand dbCommand = sqldb.GetStoredProcCommand("[eitlingzhaoumd].[User_Authentication]");
                sqldb.AddInParameter(dbCommand, "@UserName", DbType.String, CommonHelper.ToDB<String>(pUsername));
                sqldb.AddInParameter(dbCommand, "@Password", DbType.String, CommonHelper.ToDB<String>(pEncryptedPassword));

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new UserLoginViewModel();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);

                            if (entity.ID <= 0)
                            {
                                responseObjectForAnything.ResultCode = Constants.RESPONSE_ERROR;
                                responseObjectForAnything.ResultMessage = "Login Failed. Please try again!";
                                return responseObjectForAnything;
                            }

                            entity.Name = CommonHelper.FromDB<String>(iReader["Name"]);
                            entity.UserName = CommonHelper.FromDB<String>(iReader["UserName"]);
                            entity.Email = CommonHelper.FromDB<String>(iReader["Email"]);
                            entity.Role = CommonHelper.FromDB<String>(iReader["Role"]);

                            recordCount++;
                        }
                        if (recordCount == 0)
                        {
                            responseObjectForAnything.ResultCode = Constants.RESPONSE_INVALID;
                            responseObjectForAnything.ResultMessage = "You are not active user!!";
                            return responseObjectForAnything;
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
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "UserAuthentication", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }

        public ResponseObjectForAnything ReInitUserSession(int pUserID)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            UserLoginViewModel entity = null;
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("[eitlingzhaoumd].[User_S_By_ID]");
                sqldb.AddInParameter(dbCommand, "@ID", DbType.Int32, CommonHelper.ToDB<Int32>(pUserID));

                IDataReader iReader = sqldb.ExecuteReader(dbCommand);

                if (!iReader.Equals(null))
                {
                    using (iReader)
                    {
                        while (iReader.Read())
                        {
                            entity = new UserLoginViewModel();
                            entity.ID = CommonHelper.FromDB<Int32>(iReader["ID"]);
                            entity.Name = CommonHelper.FromDB<string>(iReader["FirstName"]);
                            entity.UserName = CommonHelper.FromDB<string>(iReader["UserName"]);
                            entity.Email = CommonHelper.FromDB<string>(iReader["Email"]);
                            entity.Role = CommonHelper.FromDB<string>(iReader["RoleName"]);
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
                ExceptionLog exLog = new ExceptionLog(ex.Message, ex.StackTrace, this.ToString(), "ReInitUserSession", "E");
                ExceptionManagerRepository.PublishException(exLog);
            }
            return responseObjectForAnything;
        }
    }
}
