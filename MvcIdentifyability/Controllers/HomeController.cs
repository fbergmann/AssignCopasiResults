using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using LibGenerateScans;
using MvcIdentifyability.Models;
using ZedCompareData;
using ZedGraph;
using Chart = DotNet.Highcharts.Options.Chart;
using Legend = DotNet.Highcharts.Options.Legend;
using XAxis = DotNet.Highcharts.Options.XAxis;
using YAxis = DotNet.Highcharts.Options.YAxis;

namespace MvcIdentifyability.Controllers
{
  
    public class HomeController : Controller
    {
        public ActionResult Index(GenerateOptions options = null)
        {
            return View(options);
        }

        public ActionResult Test()
        {
            var charts = new List<DotNet.Highcharts.Highcharts>();
            var files = System.IO.Directory.GetFiles(@"C:\Users\Frank\Dropbox\copasi\2012-11-30_sven\", "*_high.txt");
            for (int i = 0; i < files.Length; i++)
            {
                charts.Add(new ResultModel { FileName = files[i], Width = 350, Index = i }.GetChart());    
            }
            
            var container = new DotNet.Highcharts.Container(charts);
            return View(container);
        }

        public ActionResult PartialPlot(ResultModel model = null)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.FileName))
                return new HttpNotFoundResult();

            DotNet.Highcharts.Highcharts chart = model.GetChart();
            return PartialView(chart);
        }

        public ActionResult Plot()
        {
            return View(new ResultModel { FileName = @"C:\Users\Frank\Dropbox\copasi\2012-11-30_sven\out_001_update_high.txt", Index = 1 });
        }

        public ActionResult Plots()
        {
            return View(new ResultModel { FileName = @"C:\Users\Frank\Dropbox\copasi\2012-11-30_sven\out_001_update_high.txt", Index = 1 });
        }


        [HttpPost]
        public ActionResult Generate(GenerateOptions options, HttpPostedFileBase copasifile = null, HttpPostedFileBase datafile = null)
        {
            if (copasifile == null)
                return RedirectToAction("Index", options);

            var guid = Guid.NewGuid().ToString();
            var localRoot = Server.MapPath("~/Uploads");
            string rootDir = Path.Combine(localRoot, guid);
            string archive = Path.Combine(rootDir, "archive.zip");
            Directory.CreateDirectory(rootDir);
            string copasiFileName = Path.Combine(rootDir, Path.GetFileName(copasifile.FileName));
            copasifile.SaveAs(copasiFileName);
            if (datafile != null)
                datafile.SaveAs(Path.Combine(rootDir, Path.GetFileName(datafile.FileName)));

            var args = options.ToArgs();
            args.FileName = copasiFileName;

            var stringWriter = new StringWriter();
            var generator = new ScanGenerator(args) { Out = stringWriter };
            generator.Process();

            var files = Directory.GetFiles(rootDir, "*");
            CreateArchive(archive, files);

            return new FilePathResult(archive, "application/zip") { FileDownloadName = "archive.zip" };

            //return View();
        }


        public static string CreateArchive(string fileName, IEnumerable<string> files)
        {
            var fsOut = System.IO.File.Create(fileName);
            var zipStream = new ZipOutputStream(fsOut);
            zipStream.SetLevel(9);

            foreach (string filename in files)
            {
                var fi = new FileInfo(filename);
                string entryName = ((FileInfo)fi).Name;
                entryName = ZipEntry.CleanName(entryName);
                var newEntry = new ZipEntry(entryName);
                ((ZipEntry)newEntry).DateTime = ((FileInfo)fi).LastWriteTime;
                ((ZipEntry)newEntry).Size = ((FileInfo)fi).Length;
                zipStream.PutNextEntry(((ZipEntry)newEntry));
                var buffer = new byte[4096];
                using (var streamReader = System.IO.File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }

            zipStream.IsStreamOwner = true;
            zipStream.Close();
            return fileName;
        }

        public ActionResult About()
        {

            return View();
        }

    }
}
