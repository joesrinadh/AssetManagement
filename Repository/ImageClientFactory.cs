using Infrastructure.Interface.DataAccess;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class ImageClientFactory: IImageClientFactory
    {
        private readonly IConfiguration _configuration;
        public ImageClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets computer vision client
        /// </summary>
        /// <returns>Computer vision client</returns>
        public ComputerVisionClient GetComputerVisionClient()
        {
            var subscriptionKey = _configuration.GetValue<string>("SubscriptionKey");
            subscriptionKey = !string.IsNullOrEmpty(subscriptionKey) ? subscriptionKey : "624c37417c0c4d3ebc0d8ad71866c8d5";
            var serviceEndpoint = _configuration.GetConnectionString("CognitiveServiceConnectionString");
            return new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = serviceEndpoint
            };
        }
    }
}
