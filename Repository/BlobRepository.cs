using Azure.Storage.Blobs;
using Infrastructure.Interface.DataAccess;
using Models;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class BlobRepository: IBlobRepository
    {
        private readonly IBlobContainerFactory _blobContainerFactory;
        public BlobRepository(IBlobContainerFactory blobContainerFactory)
        {
            _blobContainerFactory = blobContainerFactory;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="fileModel">File data</param>
        /// <returns>Return uploaded URL</returns>
        public async Task<string> UploadFile(FileModel fileModel)
        {
            BlobContainerClient containerClient = _blobContainerFactory.GetBlobContainerClient();
            BlobClient blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + fileModel.FileName);
            await blobClient.UploadAsync(fileModel.File.OpenReadStream());
            return blobClient.Uri.ToString();
        }
    }
}
