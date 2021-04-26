using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.DataAccess
{
    public interface IBlobContainerFactory
    {
        public BlobContainerClient GetBlobContainerClient();
    }
}
