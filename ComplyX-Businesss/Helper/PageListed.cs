using System;
using System.Collections.Generic;
using System.ComponentModel;
using ComplyX_Businesss.Helper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Helper
{
    public class PageListed<T>
    {
        public PageListed()
        {
            Data = new List<T>();
        }

        public PageListed(List<T> data, int totalCount)
        {
            Data = data.ToList();
            TotalCount = totalCount;
        }
        public PageListed(List<T> data, int totalCount, int filteredCount)
        {
            Data = data;
            TotalCount = totalCount;
            FilteredCount = filteredCount;
        }

        public List<T> Data { get; set; }

        public int TotalCount { get; set; }

        public int FilteredCount { get; }
    }
    public class SortDescriptor
    {
        public string Field { get; set; } = string.Empty;  // e.g., "FirstName"
        public string Direction { get; set; } = "asc";     // asc or desc
    }
    
       
      
}