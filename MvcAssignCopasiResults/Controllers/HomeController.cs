using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using LibCopasiResults;

namespace MvcAssignCopasiResults.Controllers
{
  
    public class HomeController : Controller
    {

        public ActionResult ResultSelected(double selected)
        {
            CopasiResult current = (CopasiResult) Session["current"];
            if (current == null)
                return Content("<h1>Error</h1><p>No files uploaded. This might happen, if the session expired. You will have to <a href='"
                    + Url.Content("~/Home/Index") + 
            "'>start over</a>.</p>");
            var checkPoint = current.Data.Find(cp => cp.BestValue == selected);
            if (checkPoint == null)
                return Content("<h1>Error</h1><p>No data selected</p>");
            return PartialView("_ResultSelected", checkPoint);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Generate(HttpPostedFileBase reportfile = null, 
            HttpPostedFileBase copasifile = null, 
            HttpPostedFileBase datafile = null,
            HttpPostedFileBase datafile1 = null,
            HttpPostedFileBase datafile2 = null
            )
        {
            if (copasifile == null)
                return RedirectToAction("Index");

            var guid = Guid.NewGuid().ToString();
            var localRoot = Server.MapPath("~/Uploads");
            string rootDir = Path.Combine(localRoot, guid);
            string archive = Path.Combine(rootDir, "archive.zip");
            Directory.CreateDirectory(rootDir);
            string reportFileName = Path.Combine(rootDir, Path.GetFileName(reportfile.FileName));
            reportfile.SaveAs(reportFileName);
            string copasiFileName = Path.Combine(rootDir, Path.GetFileName(copasifile.FileName));
            copasifile.SaveAs(copasiFileName);
            string dataFileName = string.Empty;
            if (datafile != null)
            {
                dataFileName = Path.Combine(rootDir, Path.GetFileName(datafile.FileName));
                datafile.SaveAs(dataFileName);
            }
            if (datafile1 != null)
            {
                dataFileName = Path.Combine(rootDir, Path.GetFileName(datafile1.FileName));
                datafile1.SaveAs(dataFileName);
            }
            if (datafile2 != null)
            {
                dataFileName = Path.Combine(rootDir, Path.GetFileName(datafile2.FileName));
                datafile2.SaveAs(dataFileName);
            }

            var results = ResultParser.FromFile(reportFileName);
            
            Session["datadir"] = rootDir;
            Session["reportfile"] = reportFileName;
            Session["copasifile"] = copasiFileName;
            Session["datafile"] = dataFileName;
            Session["results"] = results;
            Session["current"] = results != null && results.Count > 0 ? results[0] : null;


            return RedirectToAction("SelectResult");

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

        public ActionResult SelectResult(int index = 0)
        {
            CopasiResult current = null;
            var results = (List<CopasiResult>) Session["results"];
            if (results != null && results.Count > index && index >= 0)
            {
                current = results[index];
                current.LoadCopasi((string) Session["copasifile"]);
                Session["current"] = current;
            }
            if (current == null)
                return RedirectToAction("Index");
            return View(current);
        }

        public ActionResult AssignCurrentState(double selected)
        {
            var current = (CopasiResult)Session["current"];
            if (current == null)
                return Content("<h1>Error</h1><p>No files uploaded. This might happen, if the session expired. You will have to <a href='"
                    + Url.Content("~/Home/Index") +
            "'>start over</a>.</p>");
            current.LoadCopasi((string)Session["copasifile"]);
            var checkPoint = current.Data.Find(cp => cp.BestValue == selected);
            if (checkPoint == null)
                return Content("<h1>Error</h1><p>No data selected</p>");

            current.SetInitialConditions(checkPoint, new List<string>());
            return GenerateCopasiDownloadFrom(current);
        }

        private ActionResult GenerateCopasiDownloadFrom(CopasiResult current)
        {
            var name = Path.GetTempFileName();
            current.SaveCPS(name);
            Response.AddHeader("Content-Disposition",
                               "attachment; filename=" + System.IO.Path.GetFileName((string) Session["copasifile"]));

            return new FileContentResult(
                fileContents: System.IO.File.ReadAllBytes(name),
                contentType: "application/xml"
                );
        }

        public ActionResult AssignTaskStartValue(double selected)
        {
            var current = (CopasiResult)Session["current"];
            if (current == null)
                return Content("<h1>Error</h1><p>No files uploaded. This might happen, if the session expired. You will have to <a href='"
                    + Url.Content("~/Home/Index") +
            "'>start over</a>.</p>");
            current.LoadCopasi((string)Session["copasifile"]);
            var checkPoint = current.Data.Find(cp => cp.BestValue == selected);
            if (checkPoint == null)
                return Content("<h1>Error</h1><p>No data selected</p>");

            if (current.IsOptimization)
            current.SetOptimizationStartValues(checkPoint);
            else
            current.SetParameterEstimationStartValues(checkPoint);
            return GenerateCopasiDownloadFrom(current);
        }

        public ActionResult PlotSolutionStatistic(double selected)
        {
            var current = (CopasiResult)Session["current"];
            var datafile = (string) Session["datafile"];
            if (!System.IO.File.Exists(datafile))
                return Content("<h1>Error</h1><p>No data file found uploaded. Current Solution Statistic not computed. You will have to <a href='"
                    + Url.Content("~/Home/Index") +
            "'>start over</a>.</p>");
            if (current == null)
                return Content("<h1>Error</h1><p>No files uploaded. This might happen, if the session expired. You will have to <a href='"
                    + Url.Content("~/Home/Index") +
            "'>start over</a>.</p>");
            current.LoadCopasi((string)Session["copasifile"]);
            var checkPoint = current.Data.Find(cp => cp.BestValue == selected);
            if (checkPoint == null)
                return Content("<h1>Error</h1><p>No data selected</p>");

            if (current.IsOptimization)
                current.SetOptimizationStartValues(checkPoint);
            else
                current.SetParameterEstimationStartValues(checkPoint);
            var name = Path.Combine((String)Session["datadir"], "run_temp.cps");
            current.SaveCPS(name);
            Process p = null;
            try
            {
                var info = new ProcessStartInfo {
                    FileName = @"E:\Development\build_master\copasi\ViewCurrentUI\ViewCurrentUI.exe",
                    Arguments = String.Format("\"{0}\" -t png -o c:\\temp --set-solution-statistic --disable-calculate-statistic --disable-randomize-startvalues --clear-targets {1}{2}",
                                              name,
                                              (current.IsParameterEstimation ? " --disable-other-plots -g 910 -r -s 33" : " -g 914 -r -s 32"),
                                              " -q"), 
                    CreateNoWindow = true
                };
                p = Process.Start(info);
                p.WaitForExit(1000*20);                
            }
            catch
            {
            }

            if (p != null)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                    ViewBag.Message = "Something went wrong calculating the solution statistic.";
                    ViewBag.DetailedMessage =
                        "Currently only simple calculations, taking less than 10 seconds are supported.";
                    ViewBag.Url = "javascript:history.back()";
                    ViewBag.Caption = "Go Back...";
                    return PartialView("Error");
                }
            }

            return new FilePathResult("c:\\Temp\\plot1.png", "image/png" );

        }
    }
}
