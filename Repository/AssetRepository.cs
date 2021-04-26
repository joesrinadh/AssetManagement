using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interface.DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Repository.Models;
using Domain = Models;
namespace Repository
{
    public class AssetRepository : RepositoryBase<Asset>, IAssetRepository
    {
        private ASMContext _context;

        public AssetRepository(ASMContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Asset details
        /// </summary>
        /// <param name="Id">Asset Id</param>
        /// <returns>Asset details</returns>
        public List<Domain.Asset> GetAssetDetails(string Id)
        {
            return _context.Assets.Include(a => a.AssetType).Where(a => a.AssetId == Id || a.MainAssetId == Id).ToList().ToDomainModel();
        }

        /// <summary>
        /// Get all asset details
        /// </summary>
        /// <returns>All asset details</returns>
        public List<Domain.Asset> GetAllAssets()
        {
            return _context.Assets.Where(a => a.MainAssetId == null).ToList().ToDomainModel();
        }

        /// <summary>
        /// Add asset details
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <returns>Count of saved assets</returns>
        public Task<int> AddAsset(Domain.Asset asset)
        {
            _context.Assets.Add(new Asset()
            {
                AssetId = asset.AssetId,
                AssetName = asset.AssetName,
                AssetPath = asset.AssetPath,
                AssetTypeId = (int)asset.AssetType,
                MainAssetId = asset.MainAssetId,
                Metadata = asset.Metadata
            });

            return _context.SaveChangesAsync();
        }
    }
}
