using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplyX_Businesss.Models
{
    public class ImportModel
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; } = default!;
    }
}
