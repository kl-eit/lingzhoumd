using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Ling.Domains.Concrete
{
    public class DBContext
    {
        public SqlDatabase sqldb { get; set; }

        public static Database dbStatic { get; set; }

        public DBContext()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            sqldb = (SqlDatabase)factory.Create("LingDBConnectionString");
            dbStatic = factory.Create("LingDBConnectionString");
        }
    }
}
