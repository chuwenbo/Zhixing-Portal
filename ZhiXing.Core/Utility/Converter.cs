using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiXing.Core.Utility
{
    public class Converter
    {
        public static int ToInt32(object obj)
        {
            int intVal = -1;

            if (obj == null || obj == DBNull.Value)
            {
                return intVal;
            }

            Int32.TryParse(obj.ToString(), out intVal);

            return intVal;
        }

        public static string ToStr(object obj)
        {
            string stringVal = string.Empty;

            if (obj == null || obj == DBNull.Value)
            {
                return stringVal;
            }

            return obj.ToString();
        }
    }
}
