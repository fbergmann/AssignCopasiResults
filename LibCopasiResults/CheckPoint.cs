using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCopasiResults
{
    public class CheckPoint
    {
        public int FunctionEvaluations { get; set; }
        public double BestValue { get; set; }
        public double[] Parameters { get; set; }

        public override string ToString()
        {
            return string.Format("Evals: {0}, Best: {1}, Params: {2}", 
                FunctionEvaluations, 
                BestValue, 
                (Parameters == null ? "none" : Parameters.Length.ToString()));
        }

    }
}
