using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLWebAPI.Models
{
    public class IncomePredictionResults
    {
        [JsonProperty("age")]
        public string Age { get; set; }
        public string WorkClass { get; set; }
        public string FnlWgt { get; set; }

        [JsonProperty("education")]
        public string Education { get; set; }
        public string EducationNum { get; set; }

        [JsonProperty("marital-status")]
        public string MaritalStatus { get; set; }

        public string Occupation { get; set; }

        [JsonProperty("relationship")]
        public string Relationship { get; set; }

        [JsonProperty("race")]
        public string Race { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }
        public string CapitalGain { get; set; }
        public string CapitalLoss { get; set; }

        public string HoursPerWeek { get; set; }
        public string NativeCountry { get; set; }

        [JsonProperty("Scored Labels")]
        public string MLPrediction { get; set; }

        [JsonProperty("Scored Probabilities")]
        public string MLConfidence { get; set; }
        public string MLResponseTime { get; set; }
    }

    public class Results
    {
        public List<IncomePredictionResults> output1 { get; set; }
    }

    public class RootObject
    {
        public Results Results { get; set; }
    }
}