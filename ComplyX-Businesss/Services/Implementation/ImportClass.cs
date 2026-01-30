
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Formats.Asn1;
using System.Globalization;
using ExcelDataReader;
using CsvHelper;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using ComplyX_Businesss.Helper;
using ComplyX.Shared.Data;
using AppContext = ComplyX_Businesss.Helper.AppContext;

namespace ComplyX.BusinessLogic
{
    public class ImportClass : ImportServices
    {
        private readonly AppContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
   
        public ImportClass(AppContext context , UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }
        public async Task<ManagerBaseResponse<ImportModel>> UploadEmployeeImportFile(string userId, ImportModel request)
        {
            var response = new ManagerBaseResponse<ImportModel>();

            try
            {
                if (request.File == null || request.File.Length == 0)
                {
                    return new ManagerBaseResponse<ImportModel>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "No file uploaded."
                    };
                }

                // File type validation
                var allowedExtensions = new[] { ".xls", ".xlsx", ".csv" };
                var fileExtension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new ManagerBaseResponse<ImportModel>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Invalid file type. Only Excel and CSV files are allowed."
                    };
                }

                // File size validation (max 5MB)
                const long maxFileSizeInBytes = 5 * 1024 * 1024;
                if (request.File.Length > maxFileSizeInBytes)
                {
                    return new ManagerBaseResponse<ImportModel>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "File size exceeds the 5MB limit."
                    };
                }
                List<string> headers = [];

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    headers = await ReadExcelFileHeaders(request.File);
                else if (fileExtension == ".csv")
                    headers = ReadCsvFileHeaders(request.File);
                 
                //List<CustomerImportMapping> mappings = [];
                //foreach (var header in headers)
                //{
                //    if (!await _unitOfWork.CustomerImportMappingRepository.AnyAsync(x => x.CustomerId == request.CustomerId
                //                                && x.ImportName == request.MapName && x.SourceField == header))
                //    {
                //        mappings.Add(new CustomerImportMapping
                //        {
                //            CustomerId = request.CustomerId,
                //            ImportName = request.MapName,
                //            SourceField = header,
                //            DestinationField = "no mapping"
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = 500;
                response.Message = "Upload failed: " + ex.Message;
            }
            return response;

        }


        //Check Excel file Headers
        private async Task<List<string>> ReadExcelFileHeaders(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var reader = ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();
            var table = result.Tables[0];
            // Extract headers
            var headers = table.Rows[0].ItemArray.Select(x => x.ToString()?.Trim() ?? "").ToList();
            return headers;
        }

        private static List<string> ReadCsvFileHeaders(IFormFile file)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                HeaderValidated = null,
                IgnoreBlankLines = true
            };
            using var stream = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
            using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);

            csv.Read();
            csv.ReadHeader();
            // Map original headers to lowercase
            var originalHeaders = csv.HeaderRecord;
            var headers = originalHeaders?.Select(h => h.Trim()).ToList();
            return headers;
        }
    }
}
