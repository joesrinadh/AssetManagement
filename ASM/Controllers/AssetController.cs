using Azure.Storage.Blobs;
using Business;
using Infrastructure.Interface.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ASM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetManager _assetManager;
        private readonly IConfiguration _configuration;
        private readonly IBlobManager _blobManager;
        private readonly IAnalysisManager _analysisManager;

        public AssetController(IAssetManager assetManager, IConfiguration configuration, IBlobManager blobManager, IAnalysisManager analysisManager)
        {
            _assetManager = assetManager;
            _configuration = configuration;
            _blobManager = blobManager;
            _analysisManager = analysisManager;
        }

        /// <summary>
        /// Get All asset details
        /// </summary>
        /// <returns>All asset details</returns>
        [HttpGet]
        [Route("GetAllAssets")]
        public List<Asset> GetAllAssets()
        {
            return _assetManager.GetAllAssets();
        }

        /// <summary>
        /// Get Asset Details
        /// </summary>
        /// <param name="Id">Asset Id</param>
        /// <returns>Asset Details</returns>
        [HttpGet]
        public List<Asset> Get(string Id)
        {
            return _assetManager.GetAssetDetails(Id);
        }

        /// <summary>
        /// Upload Asset
        /// </summary>
        [HttpPost]
        [Route("UploadAsset")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAsset([FromForm]FileModel fileModel)
        {
            if (fileModel == null)
                return StatusCode(500, "Failed to upload file.");
            try
            {
                string assetId = Guid.NewGuid().ToString();
                string uploadedURL = await _blobManager.UploadFile(fileModel);
                string metadata = null;
                if (fileModel.AssetType == AssetType.Image)
                {
                    metadata = await _analysisManager.AnalyzeImage(uploadedURL);
                }

                int count = await _assetManager.AddAsset(new Asset()
                {
                    AssetId = assetId,
                    AssetName = fileModel.FileName,
                    AssetPath = uploadedURL.ToString(),
                    MainAssetId = fileModel.AssetId,
                    AssetType = fileModel.AssetType,
                    Metadata = metadata
                });

                if (count > 0)
                {
                    return Ok(uploadedURL);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save asset");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
