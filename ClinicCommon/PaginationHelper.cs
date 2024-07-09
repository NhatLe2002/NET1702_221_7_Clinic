using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicCommon
{
    public static class PaginationHelper
    {
        public static List<T> GetPaged<T>(List<T> source, int pageIndex, int pageSize)
        {
            return source.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();
        }

        public static int GetTotalPages<T>(List<T> source, int pageSize)
        {
            return (int)Math.Ceiling(source.Count / (double)pageSize);
        }
    }
}
