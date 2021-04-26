using Infrastructure.Interface.Business;
using Infrastructure.Interface.DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BlobManager: IBlobManager
    {
        public IBlobRepository _blobRepository;
        public BlobManager(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="fileModel">File data</param>
        /// <returns>Return uploaded URL</returns>
        public Task<string> UploadFile(FileModel fileModel)
        {
            return _blobRepository.UploadFile(fileModel);
        }
    }
}
