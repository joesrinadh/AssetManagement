using System;
using System.Collections.Generic;

namespace Models
{
    public class Asset
    {
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public AssetType AssetType { get; set; }
        public string AssetPath { get; set; }
        public string MainAssetId { get; set; }
        public string Metadata { get; set; }
    }
}
