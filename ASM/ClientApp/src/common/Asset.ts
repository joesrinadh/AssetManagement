export interface IAsset{
    AssetId: string,
    AssetName: string
    AssetPath: string
    MainAssetId?: string | null
    Metadata: string
    AssetType: AssetType
}

export enum AssetType{
    Image = 1,
    Video = 2
}