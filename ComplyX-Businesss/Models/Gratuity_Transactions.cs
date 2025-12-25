using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class Gratuity_Transactions
    {
        public  int GratuityID { get; set; }
        public int EmployeeID { get; set; }
        public decimal LastDrawnSalary { get; set; }
        public int YearsOfService { get; set; }
        public decimal GratuityAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaidDate { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Employees? Employees { get; set; }
    }
}
