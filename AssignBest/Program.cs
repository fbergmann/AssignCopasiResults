using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using LibCopasiResults;

namespace AssignBest
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = culture;


            if (args.Length < 3)
            {
                Console.WriteLine("Need three arguments: <resultFileName> <originalCopasiFileName> <outputFileName> [-r || --removeLists]");
                Environment.Exit(-1);
            }

            var result = ResultParser.FromFile(args[0]);

            var last = result.LastOrDefault();
            if (last == null)
            {
                Console.WriteLine("Couldn't find any data in the file ... ");
                Environment.Exit(-1);
            }

            last.LoadCopasi(args[1]);

            last.CheckDataAgainstModel();

            last.SetStartValues(last.Data.Last());

            last.SaveCPS(args[2]);

            if (args.Length > 3 && (args[3] == "-r" || args[3] == "--removeLists"))
            {
                var xml = File.ReadAllText(args[2]);
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                
                var settings = new XmlWriterSettings{ Indent = true, IndentChars=" "};
                var stream = new StringWriter();
                var writer = XmlWriter.Create(stream, settings);

                var elements = doc.GetElementsByTagName("ListOfModelParameterSets");
                if (elements.Count > 0)
                {
                    var current = elements[0];
                    current.ParentNode.RemoveChild(current);
                }


                doc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                var result1 = stream.ToString();
                result1 = result1.Replace("\t", "&#x09;");

                File.WriteAllText(args[2], result1);
            }

            Console.WriteLine("done ...");

        }
    }
}
