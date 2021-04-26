using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.Business
{
    public interface IAnalysisManager
    {
        public Task<string> AnalyzeImage(string url);
    }
}
