using System;
using System.Collections.Generic;
using System.Linq;

namespace LibCopasiResults
{
    public class FittingItem
    {
        public string Name { get; set; }
        public double StartValue { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public List<string> AffectedExperiments { get; set; }


        public FittingItem()
        {
            AffectedExperiments = new List<string>();
        }

        public override string ToString()
        {
            return string.Format ("{0} = {1} (min: {2}, max: {3}, experiments: {4})", 
                Name, StartValue, LowerBound, UpperBound, AffectedExperiments.AsString());
        }

    }
}
