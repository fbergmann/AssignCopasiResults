using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using ZedCompareData;

namespace MvcIdentifyability.Models
{
    public class ResultModel
    {
        public DotNet.Highcharts.Highcharts GetChart()
        {
            var pane = ZedUtils.GetPaneForFile(FileName);


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart" + (Index.HasValue ? Index.ToString() : ""))
            .SetTitle(new Title { Text = System.IO.Path.GetFileNameWithoutExtension(FileName) })
            .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Fit" } })
            .SetXAxis(new XAxis { Title = new XAxisTitle { Text = "Parameter" } })
                //.SetTooltip(new Tooltip { BorderWidth = 2, Formatter = @"function() { return '<b> Fit </b><br/> for '+ this.x +': '+ this.y ; }" })
            .InitChart(new Chart
            {
                ClassName = "highchart",
                ZoomType = ZoomTypes.Xy,
                //Animation = new Animation(false),
                PlotShadow = false,
                Width = (Width)
            })
            .SetCredits(new Credits { Enabled = false })
                //.SetCredits(new Credits { Text = "Hullo", Href = "http://frank-fbergmann.blogspot.com" })
            .SetExporting(
                new Exporting
                {
                    Enabled = true,                  
                })
             .SetLegend(new Legend { Enabled = false })
             .SetPlotOptions(new PlotOptions { Line = new PlotOptionsLine { Marker = new PlotOptionsLineMarker { Enabled = true, Symbol = "circle" } } });

            ;
            var series = new List<Series>();
            foreach (var item in pane.CurveList)
            {
                var values = item.To2DArray<object>();
                string label = item.Label.Text;
                series.Add(new Series
                {
                    Data = new Data(values),
                    Name = label,
                    Color = System.Drawing.Color.FromArgb(0x4572A7)
                });

            }
            chart.SetSeries(series.ToArray());
            return chart;
        }
        public string FileName { get; set; }
        public int? Index { get; set; }
        public double? Width { get; set; }
    }
}