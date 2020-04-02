using Ling.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Domains.Concrete
{
    public class DBContext
    {
        public SqlDatabase sqldb { get; set; }

        public static Database dbStatic { get; set; }

        private static string _connectionString = "";

        public DBContext(IConfiguration iConfiguration)
        {
            ConfigurationHelper _configuration = new ConfigurationHelper(iConfiguration);
            _connectionString = _configuration.GetConnectionString();

            sqldb = new SqlDatabase(_connectionString);
            dbStatic = new SqlDatabase(_connectionString);
        }
    }
}