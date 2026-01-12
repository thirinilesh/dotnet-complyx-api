using ComplyX_Businesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
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
        public string  FormType (string FormType)
        {
            if (FormType == "24Q")
                return "24Q";

            if (FormType == "26Q")
                return "26Q";

            if (FormType == "27Q")
                return "27Q";
            if (FormType == "27EQ")
                return "27EQ";
            throw new ValidationException("Invalid FormType. Allowed values: 24Q, 26Q, 27Q, 27EQ");
        }
        public string FinancialYear (string Year)
        {
            if (Year == "2024-25")
                return "2024-25";

            if (Year == "2025-26")
                return "2025-26";
            throw new ValidationException("Invalid FormType. Allowed values: 2024-25 , 2025-26");             
        }
        public enum ReturnType
        {
            REGULAR = 1,
            CORRECTION = 2
        }
        public enum TdsReturnStatus
        {
            DRAFT = 1,
            VALIDATED = 2,
            FILED = 3,
            REVISED = 4,
            REJECTED = 5
        }
    }
}
