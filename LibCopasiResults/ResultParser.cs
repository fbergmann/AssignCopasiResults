using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibCopasiResults
{
    public class ResultParser
    {
        public static List<CopasiResult> ParseContent(string content)
        {
            return ParseStream(new StringReader(content));
        }

        private static bool SkipTo(TextReader reader, string lineStart)
        {
            bool test;
            return SkipTo(reader, lineStart, out test);
        }

        private static bool SkipTo(TextReader reader, string lineStart, out bool flag, Func<string,bool> eval = null)
        {
            string line;
            flag = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim().StartsWith(lineStart))
                {
                    if (eval != null)
                        flag = eval(line);
                    return true;
                }
                continue;
            }
            return  false;
        }
        public static FittingItem ParseItem(string line)
        {
            var item = new FittingItem();

            int lastEq = line.LastIndexOf("=");
            if (lastEq == -1)
                return null;
            item.StartValue = SaveToDouble(line.Substring(lastEq + 1));
            int firstLeq = line.IndexOf("<=");
            if (firstLeq == -1)
                return null;
            item.LowerBound = SaveToDouble(line.Substring(0, firstLeq));
            int lastLeq = line.IndexOf("<=", firstLeq + 2);
            int square = line.IndexOf("]", firstLeq + 2);
            if (square == -1)
                square = line.IndexOf(")", firstLeq + 2);
            int lastSep = line.IndexOf(";", square);
            string substring = line.Substring(lastLeq + 2, lastSep - (lastLeq + 2));
            item.UpperBound = SaveToDouble(substring);
            item.Name = CRUtils.SanitizeName(line.Substring(firstLeq + 2, lastLeq - (firstLeq + 2)).Trim());

            return item;
        }
        private static List<FittingItem> ReadItems(TextReader reader, out bool flag)
        {
            List<FittingItem> result = new List<FittingItem>();
            string line;
            flag = false;
            bool foundStart = SkipTo(reader, "List of ", out flag,
                (target) => target.Contains("Optimization")
                );
            if (!foundStart)
            {
                return result;
            }

            FittingItem current = null;

            while ((line = reader.ReadLine()) != null)
            {
            readNext:
                if (String.IsNullOrWhiteSpace(line))
                    return result;

                current = ParseItem(line);

                if (current == null)
                    break;

                if (flag)
                {
                    result.Add(current);
                    continue;
                }
                
                reader.ReadLine();

                
                readExp:
                    string experiments = reader.ReadLine();
                    bool isExp = !string.IsNullOrWhiteSpace(experiments) && experiments.Substring(0, 6).Trim().Length == 0;
                    if (isExp)
                    {
                        var index = experiments.IndexOf("Affected Cross Validation Experiments");
                        if (index != -1)
                        {
                          experiments = experiments.Substring(0, index);
                          reader.ReadLine();
                        }
                        else
                        {
                          index = experiments.IndexOf("Affected Validation Experiments");
                          if (index != -1)
                          {
                            experiments = experiments.Substring(0, index);
                            reader.ReadLine();
                          }
                        }

                        current.AffectedExperiments.AddRange(experiments.Trim().Split(new[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                        goto readExp;
                    }
                    else
                    {
                        result.Add(current);
                        line = experiments;
                        goto readNext;
                    }
                
            }


            return result;
        }

        public static double SaveToDouble(string doubleString, double defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(doubleString))
                return defaultValue;
            try
            {
                string current = doubleString.Trim().ToLowerInvariant();
                if (current == "inf" || current == "1.#inf")
                    return double.PositiveInfinity;
                else if (current == "-inf" || current == "-1.#inf")
                    return double.NegativeInfinity;
                else if (current == "nan" || current == "1.#nan")
                    return double.NaN;

                return Convert.ToDouble(doubleString);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static CheckPoint ParseValue(string line)
        {
            var result = new CheckPoint();

            int firstTab = line.IndexOf('\t');
            if (firstTab == -1) return null;
            result.FunctionEvaluations = Convert.ToInt32(line.Substring(0, firstTab));
            int open = line.IndexOf('(');
            result.BestValue = SaveToDouble((string)line.Substring(firstTab + 1, open - (firstTab + 1)));
            string[] rawValues = line.Substring(open + 1, line.LastIndexOf(')') - (open + 1)).Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            result.Parameters = (from raw in rawValues select SaveToDouble(raw)).ToArray();
            return result;
        }

        private static List<CheckPoint> ReadValues(TextReader reader)
        {
            var result = new List<CheckPoint>();

            string line;
            bool foundStart = SkipTo(reader, "[Function Eva");
            if (!foundStart)
            {
                return result;
            }

            while ((line = reader.ReadLine()) != null)
            {
                if (String.IsNullOrWhiteSpace(line))
                    return result;

                var item = ParseValue(line);

                if (item == null)
                    break;
                result.Add(item);

            }

            return result;
        }
        public static List<CopasiResult> ParseStream(TextReader reader)
        {

            var list = new List<CopasiResult>();

            while (reader.Peek() != -1)
            {
                bool flag = false;
                var items = ReadItems(reader, out flag);
                var values = ReadValues(reader);

                if (items.Count == 0 || values.Count == 0)
                    continue;

                CopasiResult result = new CopasiResult { FittingItems = items, Data = values, IsOptimization = flag };
                list.Add(result);

            }


            reader.Close();


            return list;

        }
        public static List<CopasiResult> ParseFile(string fileName)
        {
            return ParseStream(new StreamReader(fileName));
        }
        public static List<CopasiResult> FromFile(string fileName)
        {
            return ResultParser.ParseFile(fileName);
        }

    }
}
