using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class AssetType
    {
        public AssetType()
        {
            Assets = new HashSet<Asset>();
        }

        public int AssetTypeId { get; set; }
        public string AssetDesc { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
