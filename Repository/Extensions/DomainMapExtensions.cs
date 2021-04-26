using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = Models;

namespace Repository.Extensions
{
    internal static class DomainMapExtensions
    {
        internal static List<Domain.Asset> ToDomainModel(this List<Asset> assetModelList)
        {
            if (assetModelList == null)
                return null;

            List<Domain.Asset> asset = assetModelList.Select(assetModel => new Domain.Asset()
            {
                AssetId = assetModel.AssetId,
                AssetName = assetModel.AssetName,
                AssetType = (Domain.AssetType)assetModel.AssetTypeId,
                AssetPath = assetModel.AssetPath,
                MainAssetId = assetModel.MainAssetId,
                Metadata = assetModel.Metadata
            }).ToList();

            return asset;
        }

        internal static Domain.Asset ToDomainModel(this Asset assetModel)
        {
            if (assetModel == null)
                return null;

            Domain.Asset asset = new Domain.Asset()
            {
                AssetId = assetModel.AssetId,
                AssetName = assetModel.AssetName,
                AssetType = (Domain.AssetType)assetModel.AssetTypeId,
                AssetPath = assetModel.AssetPath,
                MainAssetId = assetModel.MainAssetId,
                Metadata = assetModel.Metadata
            };

            return asset;
        }
    }
}
