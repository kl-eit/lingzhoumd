using Ling.Domains.Abstract;
using System;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using System.Data.Common;
using System.Data;
using Ling.Common;

namespace Ling.Domains.Concrete
{
    public class UserRepository : DBContext, IUserRepository
    {
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
            throw new NotImplementedException();
        }

        public int SelectTotalCount(string pSearchText = "")
        {
            throw new NotImplementedException();
        }

        public ResponseObjectForAnything Upsert(Users pEntity)
        {
            throw new NotImplementedException();
        }
        public ResponseObjectForAnything UserAuthentication(string pUsername, string pEncryptedPassword)
        {
            ResponseObjectForAnything responseObjectForAnything = new ResponseObjectForAnything();
            UserLoginViewModel entity = null;
            int recordCount = 0;
            try
            {
                DbCommand dbCommand = sqldb.GetStoredProcCommand("User_Authentication");
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

                            entity.FirstName = CommonHelper.FromDB<String>(iReader["FirstName"]);
                            entity.LastName = CommonHelper.FromDB<String>(iReader["LastName"]);
                            entity.UserName = CommonHelper.FromDB<String>(iReader["UserName"]);
                            entity.Email = CommonHelper.FromDB<String>(iReader["Email"]);
                            entity.Role = CommonHelper.FromDB<String>(iReader["Role"]);
                            entity.Avatar = CommonHelper.FromDB<String>(iReader["Avatar"]);
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
    }
}
