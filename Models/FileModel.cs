using Microsoft.AspNetCore.Http;
using Models;

namespace Models
{
    public class FileModel
    {
        public AssetType AssetType { get; set; }
        public string AssetId { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
