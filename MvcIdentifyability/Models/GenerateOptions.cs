using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using LibGenerateScans;

namespace MvcIdentifyability.Models
{
    public class GenerateOptions
    {
        public bool DisableOtherPlots { get; set; }
        public bool DisableOtherTasks { get; set; }
        public bool UseHooke { get; set; }
        public int Iterations { get; set; }
        public int ScanInterval { get; set; }

        [Display(Name = "Levenberg Tolerance")]                
        public double LMTolerance { get; set; }
        public double LowerMultiplier { get; set; }
        public double Modulation { get; set; }
        public double UpperMultiplier { get; set; }

        public GenerateOptions()
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
        }

        public static ValidationResult ValidateDouble(string possibleDouble, ValidationContext pValidationContext)
        {
            double value;
            if (double.TryParse(possibleDouble, out value))
                return ValidationResult.Success;
            return new ValidationResult("Number must be double", new List<string> { "LMTolerance" });      
            
        }

        public GenerateArguments ToArgs()
        {
            return new GenerateArguments
                       {
                           DisableOtherPlots = this.DisableOtherPlots,
                           DisableOtherTasks = this.DisableOtherTasks,
                           Iterations = this.Iterations,
                           LMTolerance = this.LMTolerance,
                           LowerMultiplier = this.LowerMultiplier,
                           Modulation = this.Modulation,
                           ScanInterval = (uint) this.ScanInterval,
                           UpperMultiplier = this.UpperMultiplier,
                           UseHooke = this.UseHooke
                       };
        }
    }
}