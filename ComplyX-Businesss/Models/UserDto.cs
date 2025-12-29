using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
     
        public class UserDto : IExampleProvider<IEnumerable<UserDto>>
        {
        private object UserRoles;

        public string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string GSTIN { get; set; } = string.Empty;
        public string PAN { get; set; } = string.Empty;
        /// <summary>
        /// Items Enum: 
        /// `"ACAAudit"`, `"ElectronicDelivery"`, `"Dashboard"`, `"EEOCUser"`, `"SensitiveData"`,
        /// `"GeneratorUser"`, `"DataManagement"`, `"BenAdminUser"`, `"UserDefRules"`, `"PowerUser"`,
        /// `"UploadData"`, `"SetupPage"`, `"RunReports"`, `"BenAdminAdministrator"`, `"BenAdminNoEdit"`, `"RemovePayData"`
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();
            /// <summary>
            /// A valid company ID greater than 0
            /// </summary>
         

        public IEnumerable<UserDto> GetExample()
        {
            return new List<UserDto>
            {
                new UserDto
                {
                    UserName = "testuser",
                Password = "test@1244",
                Email = "user@example.com",
                Phone = "9855623256",
                Address= "Ahmedabad",
                State = "Gujarat",
                GSTIN = "HJ2314",
                PAN = "JHP142304"
                }
            }.ToList();
        }

        public IEnumerable<UserDto> GetExamples() => GetExample();
        }

    public interface IExampleProvider<TSource>
    {/// <summary>
     ///     This method generates and returns an example object of <typeparamref name="TSource" />
     /// </summary>
     /// <returns>The example <typeparamref name="TSource" /> object</returns>
        public TSource GetExample();

        /// <summary>
        ///     This method generates and returns an example object of <typeparamref name="TSource" />
        /// </summary>
        /// <returns>The example <typeparamref name="TSource" /> object</returns>
        public new TSource GetExamples() => GetExample();
    }

}
