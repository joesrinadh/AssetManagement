using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Infrastructure.Interface.DataAccess
{
    public interface IAssetRepository
    {
        public List<Asset> GetAssetDetails(string Id);
        public List<Asset> GetAllAssets();
        public Task<int> AddAsset(Asset asset);
    }
}
