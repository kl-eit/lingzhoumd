using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Common
{
    public class CommonHelper
    {
        // THIS IS USED WHEN CONVERTING DATA GETTING FROM DATABASE 
        public static T FromDB<T>(object value)
        {
            return value == DBNull.Value ? default(T) : (T)value;
        }

        // THIS IS USED WHEN WE HAVE PASS PERAMETER IN DB
        public static object ToDB<T>(T value)
        {
            return value == null ? (object)DBNull.Value : value;
        }

        public static T ConvertTo<T>(object value)
        {
            return value == null ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
