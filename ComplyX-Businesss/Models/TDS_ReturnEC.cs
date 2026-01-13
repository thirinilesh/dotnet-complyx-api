using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class TDSReturnEntry
    {
        public int ReturnEntryId { get; set; }
        public int EntryID { get; set; }
        public int ReturnID { get; set; }
        public virtual TDSEntry? TDSEntry { get; set; }
        public virtual TDSReturn? TDSReturn { get; set; }

    }
    public class TDSReturnChallan
    {
        public int ReturnChallanID { get; set; }
        public int ChallanID { get; set; }
        public int ReturnID { get; set; }
        public virtual TDSChallan? TDSChallan { get; set; }
        public virtual TDSReturn? TDSReturn { get; set; }
    }
}
