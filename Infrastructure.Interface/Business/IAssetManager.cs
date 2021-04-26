using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.Business
{
   public interface IAssetManager
    {
        public List<Asset> GetAssetDetails(string Id);
        public List<Asset> GetAllAssets();
        public Task<int> AddAsset(Asset asset);
    }
}
