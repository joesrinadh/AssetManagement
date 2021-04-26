using Infrastructure.Interface.Business;
using Infrastructure.Interface.DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class AssetManager: IAssetManager
    {
        public IAssetRepository _assetRepository;

        public AssetManager(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        /// <summary>
        /// Get asset details by Id
        /// </summary>
        /// <param name="Id">Asset id</param>
        /// <returns>Returns asset details</returns>
        public List<Asset> GetAssetDetails(string Id)
        {
            return _assetRepository.GetAssetDetails(Id);
        }

        /// <summary>
        /// Get all asset details
        /// </summary>
        /// <returns>All asset details</returns>
        public List<Asset> GetAllAssets()
        {
            return _assetRepository.GetAllAssets();
        }

        /// <summary>
        /// Add asset
        /// </summary>
        /// <param name="asset">Asset to be added</param>
        /// <returns>Count of saved assets</returns>
        public Task<int> AddAsset(Asset asset)
        {
            return _assetRepository.AddAsset(asset);
        }
    }
}
