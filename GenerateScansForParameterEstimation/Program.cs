using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibGenerateScans;
using org.COPASI;

namespace GenerateScansForParameterEstimation
{

    class Program
    {
        
        static void Main(string[] args)
        {
            var generator = new ScanGenerator(new GenerateArguments(args));

            if (!generator.IsValid)
                GenerateArguments.PrintUsageAndExit();            
            
            generator.Process();

        }
    }
}
