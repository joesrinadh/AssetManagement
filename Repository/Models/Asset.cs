using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Asset
    {
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public int AssetTypeId { get; set; }
        public string AssetPath { get; set; }
        public string MainAssetId { get; set; }
        public string Metadata { get; set; }

        public virtual AssetType AssetType { get; set; }
    }
}
