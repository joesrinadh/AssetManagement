using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.DataAccess
{
    public interface IBlobRepository
    {
        public Task<string> UploadFile(FileModel fileModel);
    }
}
