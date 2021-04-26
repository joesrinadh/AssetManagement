import { AssetType } from '../common/Asset';

class UploadFilesService {
    upload(file: any, assetId?: string) {
        let formData = new FormData();
        formData.append("file", file);
        formData.append("fileName", file.name);
        if (assetId) {
            formData.append("assetId", assetId);
        }
        formData.append("assetType", assetType(file.name).toString());

        const requestOptions = {
            method: 'POST',
            body: formData
        };

        return fetch('api/asset/UploadAsset', requestOptions);
    }
}

const getExtension = (filename: string) => {
    const parts = filename.split('.');
    return parts[parts.length - 1];
};

const assetType = (fileName: string) => {
    var ext = getExtension(fileName);
    let assetType: number = AssetType.Image;
    switch (ext.toLowerCase()) {
        case 'jpg':
        case 'gif':
        case 'bmp':
        case 'png':
            assetType = AssetType.Image;
            break;
        case 'm4v':
        case 'avi':
        case 'mpg':
        case 'mp4':
            assetType = AssetType.Video;
            break;
    }
    return assetType;
}

export default new UploadFilesService();