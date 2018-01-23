using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLWebAPI.Models
{
    public class IncomePredictionResults
    {
        public string Age { get; set; }
        public string WorkClass { get; set; }
        public string FnlWgt { get; set; }

        public string Education { get; set; }
        public string EducationNum { get; set; }
        public string MaritalStatus { get; set; }

        public string Occupation { get; set; }
        public string Relationship { get; set; }
        public string Race { get; set; }

        public string Sex { get; set; }
        public string CapitalGain { get; set; }
        public string CapitalLoss { get; set; }

        public string HoursPerWeek { get; set; }
        public string NativeCountry { get; set; }
        public string MLPrediction { get; set; }
        public string MLConfidence { get; set; }
        public string MLResponseTime { get; set; }
    }
}