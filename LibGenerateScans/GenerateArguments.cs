using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGenerateScans
{

    public class GenerateArguments
    {
        public string FileName { get; set; }
        public double LowerMultiplier { get; set; }
        public double UpperMultiplier { get; set; }
        public double Modulation { get; set; }
        public double LMTolerance { get; set; }
        public int Iterations { get; set; }
        public bool DisableOtherPlots { get; set; }
        public bool DisableOtherTasks { get; set; }
        public bool UseHooke { get; set; }
        public string Prefix { get; set; }
        public uint ScanInterval { get; set; }
        /// <summary>
        /// Initializes a new instance of the Arguments class.
        /// </summary>
        public GenerateArguments()
        {
            LowerMultiplier = 0.5;
            UpperMultiplier = 2;
            Modulation = 0.01;
            LMTolerance = 1e-6;
            Iterations = 50;
            ScanInterval = 40;
            DisableOtherPlots = true;
            DisableOtherTasks = true;
            UseHooke = false;
            Prefix = "out_";

        }

        public void PrintStatus()
        {
            Console.WriteLine("Filename          : {0}", Path.GetFileName(FileName));
            Console.WriteLine("Lower Multiplier  : {0}", LowerMultiplier);
            Console.WriteLine("Upper Multiplier  : {0}", UpperMultiplier);
            Console.WriteLine("Modulation        : {0}", Modulation);
            Console.WriteLine("LM Toleratnce     : {0}", LMTolerance);
            Console.WriteLine("Iterations        : {0}", Iterations);
            Console.WriteLine("Disable Plots     : {0}", DisableOtherPlots);
            Console.WriteLine("Disable Tasks     : {0}", DisableOtherTasks);
            Console.WriteLine("Output Prefix     : {0}", Prefix);
            Console.WriteLine("Use Hooke & Jeeves: {0}", UseHooke);
            Console.WriteLine();
        }
        public static void PrintUsageAndExit()
        {
            Console.WriteLine("This program generates scans over each parameter of a parameter estimation. ");
            Console.WriteLine();
            Console.WriteLine("Usage: -f | --file <filename>");
            Console.WriteLine("       -l | --lower <multilier for current parameter value>");
            Console.WriteLine("       -u | --upper <multilier for current parameter value>");
            Console.WriteLine("       -m | --modulation <levenberg marquardt modulation>");
            Console.WriteLine("       -e | --tolerance <levenberg marquardt tolerance>");
            Console.WriteLine("       -i | --iteration <iteration for parameter estimation run>");
            Console.WriteLine("       -s | --steps <numer of steps for the scan>");
            Console.WriteLine("       -o | --output-prefix <prefix for output files and reports>");
            Console.WriteLine("       -p | --dont-disable-plots");
            Console.WriteLine("       -t | --dont-disable-other-tasks");
            Console.WriteLine("       -h | --help | /?");
            Console.WriteLine();
            Environment.Exit(0);
        }
        public GenerateArguments(string[] args)
            : this()
        {
            var list = new List<string>(args);
            for (int i = 0; i < list.Count; i++)
            {
                var current = list[i];
                var currentLow = current.Trim().ToLowerInvariant();
                var next = i + 1 < list.Count ? list[i + 1] : null;
                var haveNext = next != null;
                if (haveNext && (currentLow == "-f" || currentLow == "--file"))
                    FileName = next;
                else if (haveNext && (currentLow == "-o" || currentLow == "--ioutput-prefix"))
                    Prefix = next;
                else if (haveNext && (currentLow == "-l" || currentLow == "--lower"))
                    LowerMultiplier = double.Parse(next);
                else if (haveNext && (currentLow == "-u" || currentLow == "--upper"))
                    UpperMultiplier = double.Parse(next);
                else if (haveNext && (currentLow == "-m" || currentLow == "--modulation"))
                    Modulation = double.Parse(next);
                else if (haveNext && (currentLow == "-e" || currentLow == "--tolerance"))
                    LMTolerance = double.Parse(next);
                else if (haveNext && (currentLow == "-i" || currentLow == "--iteration"))
                    Iterations = int.Parse(next);
                else if (haveNext && (currentLow == "-s" || currentLow == "--steps"))
                    ScanInterval = uint.Parse(next);
                else if (currentLow == "-p" || currentLow == "--dont-disable-plots")
                    DisableOtherPlots = false;
                else if (currentLow == "-t" || currentLow == "--dont-disable-other-tasks")
                    DisableOtherTasks = false;
                else if (currentLow == "-j" || currentLow == "--use-hooke-and-jeeves")
                    UseHooke = true;
                else if (currentLow == "/?" || currentLow == "-h" || currentLow == "--help")
                    PrintUsageAndExit();
            }
        }

        public bool IsValid { get { return File.Exists(FileName); } }

    }

}
