using Infrastructure.Interface.Business;
using Infrastructure.Interface.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class AnalysisManager : IAnalysisManager
    {
        public IAnalysisRepository _analysisRepository;
        public AnalysisManager(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        /// <summary>
        /// Return Image metadata
        /// </summary>
        /// <param name="url">Image path</param>
        /// <returns>Meta data in JSON format</returns>
        public Task<string> AnalyzeImage(string url)
        {
            return _analysisRepository.AnalyzeImage(url);
        }
    }
}
