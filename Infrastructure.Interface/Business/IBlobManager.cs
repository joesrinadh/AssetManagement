using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.Business
{
    public interface IBlobManager
    {
        public Task<string> UploadFile(FileModel fileModel);
    }
}
