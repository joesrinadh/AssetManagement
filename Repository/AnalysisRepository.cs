using Infrastructure.Interface.DataAccess;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class AnalysisRepository: IAnalysisRepository
    {
        private readonly IImageClientFactory _imageClientFactory;
        public AnalysisRepository(IImageClientFactory imageClientFactory)
        {
            _imageClientFactory = imageClientFactory;
        }

        /// <summary>
        /// Return Image metadata
        /// </summary>
        /// <param name="url">Image path</param>
        /// <returns>Meta data in JSON format</returns>
        public async Task<string> AnalyzeImage(string url)
        {
            IList<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

            ComputerVisionClient visionClient = _imageClientFactory.GetComputerVisionClient();
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            if (analysis !=null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(analysis);
            }

            return null;
        }
    }
}
