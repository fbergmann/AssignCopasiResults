using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCopasiResults
{
    public static class CRUtils
    {
        public static string SanitizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return name;
            return name.Replace("\"", "");
        }

        public static string AsString(this List<string> list)
        {
            return ToString(list);
        }

        public static string ToString(List<string> list)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(list[i]);
                if (i + 1 < list.Count)
                    builder.Append(", ");
            }
            return builder.ToString();
        }
    }
}
