using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class PagedListCriteria
    {

            public int Skip { get; set; } = 0;
            public int Take { get; set; } = 10;
             public int Count { get; set; }
             public string? SearchText { get; set; }
            public IList<string>? OrderBy { get; set; }
            public IList<string>? Filters { get; set; }  // Will contain JSON strings
        
    }
}
