using Azure.Storage.Blobs;
using Infrastructure.Interface.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BlobContainerFactory: IBlobContainerFactory
    {
        private readonly IConfiguration _configuration;
        public BlobContainerFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get blob container client instance
        /// </summary>
        /// <returns>Blob container client instance</returns>
        public BlobContainerClient GetBlobContainerClient()
        {
            var storageConnection = _configuration.GetConnectionString("BlobConnectionString");
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnection);
            return blobServiceClient.GetBlobContainerClient("assetmanagement");
        }
    }
}
