using ComplyX_Businesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Helper
{
    public class Commanfield
    {
        public enum PANStatus
        {
            VALID,          // PAN is valid
            INVALID,        // PAN is invalid
            NOT_AVAILABLE,  // PAN is not provided
            UNKNOWN         // PAN status unknown
        }
        public enum DeducteeType
        {
            EMPLOYEE,
            VENDOR,
            PENSIONER,
            OTHER
        }
        public  bool IsValidPAN(string pan)
        {
            if (string.IsNullOrWhiteSpace(pan))
                return false;

            // Remove spaces and convert to uppercase
            pan = pan.Trim().ToUpper();

            // PAN format: 5 letters + 4 digits + 1 letter
            var panRegex = new Regex("^[A-Z]{5}[0-9]{4}[A-Z]{1}$");

            return panRegex.IsMatch(pan);
        }
        public string GetDeducteeType(Employees employee)
        {
            if (employee == null)
                return "OTHER";

            if (employee.IsPensioner)
                return "PENSIONER";

            if (employee.IsVendor)
                return "VENDOR";

            return "EMPLOYEE";
        }
    }
}
