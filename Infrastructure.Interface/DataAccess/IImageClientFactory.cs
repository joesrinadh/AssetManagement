using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.DataAccess
{
    public interface IImageClientFactory
    {
        public ComputerVisionClient GetComputerVisionClient();
    }
}
