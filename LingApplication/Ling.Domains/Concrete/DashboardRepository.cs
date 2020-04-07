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

        public ResponseObjectForAnything Upsert(DashboardViewModel pEntity)
        {
            throw new NotImplementedException();
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
    }
}
