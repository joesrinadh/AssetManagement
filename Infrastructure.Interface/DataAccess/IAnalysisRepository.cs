using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.DataAccess
{
    public interface IAnalysisRepository
    {
        public Task<string> AnalyzeImage(string url);
    }
}
