using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.ModelTools
{
    public static class MetaModelHelpers
    {
        public static string GetForeignKeyId(this string EntityName)
        {

            return string.Concat(EntityName, "Id");        
        }
    }
}
