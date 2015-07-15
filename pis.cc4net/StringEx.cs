using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net
{
    public static class StringEx
    {
        public static String ToString(this string value, int len)
        {
            if (value == null)
            {
                value = "";
            }
           
            if(value.Length > len)
            {
                return value.Substring(0, len);
            }
            else
            {
                for(int i=value.Length;i<len;i++)
                {
                    value += " ";
                }
                return value;
            }
           
        }
    }
}
