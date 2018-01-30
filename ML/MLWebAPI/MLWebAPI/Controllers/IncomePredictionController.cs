using MLWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MLWebAPI.Controllers
{
    //Allow *ALL* Cross-Origin Resource Sharing CORS Requests
    [EnableCors("*", "*", "*")]
    public class IncomePredictionController : ApiController
    {
        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        public static string outMLResultData = "";

        [HttpGet()]
        public async Task<IncomePredictionResults> GetPrediction()
        {
            //Prepare a new ML Response Data Structure for the result
            IncomePredictionResults incomePredictionResults = new IncomePredictionResults();

            //Parse the input parameters from the request
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            //Validate number of Input parameters (TODO: Add more validations)

            if (nvc.Count < 14)
            {

            }

            //Extract input values
            string inAge = nvc[0];
            string inWorkClass = nvc[1];
            string inFnlwgt = nvc[2];
            string inEducation = nvc[3];
            string inEducationNum = nvc[4];
            string inMaritalStatus = nvc[5];
            string inOccupation = nvc[6];
            string inRelationship = nvc[7];
            string inRace = nvc[8];
            string inSex = nvc[9];
            string inCapitalGain = nvc[10];
            string inCapitalLoss = nvc[11];
            string inHoursPerWeek = nvc[12];
            string inNativeCountry = nvc[13];


            //using (var client = new HttpClient())
            //{
            //    var scoreRequest = new
            //    {

            //        Inputs = new Dictionary<string, StringTable>() {
            //            {
            //                "input1",
            //                new StringTable()
            //                {
            //                    ColumnNames = new string[] {"age", "education", "marital-status", "relationship", "race", "sex"},
            //                    Values = new string[,] {  { inAge, inEducation, inMaritalStatus, inRelationship, inRace, inSex}}
            //                }
            //            },
            //        },
            //        GlobalParameters = new Dictionary<string, string>()
            //        {
            //        }
            //    };

            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "age", inAge
                                            },
                                            {
                                                "education", inEducation
                                            },
                                            {
                                                "marital-status", inMaritalStatus
                                            },
                                            {
                                                "relationship", inRelationship
                                            },
                                            {
                                                "race", inRace
                                            },
                                            {
                                                "sex", inSex
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };


                var sw = new Stopwatch();

                const string apiKey = "QjBI1INingF4w2AsUxi2kT14VUmTTI0onfori/OJswSiFKnLsZqlLedRl+1A4s2HD2hy4h/y1Y782ihl0ZePhw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/9f5a46e7452b4199929a1fb90f226160/services/c283330d7dd44270b7565790e2b1f24a/execute?api-version=2.0&format=swagger");
                //client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/9f5a46e7452b4199929a1fb90f226160/services/c283330d7dd44270b7565790e2b1f24a/execute?api-version=2.0&details=true");

                //Time the ML Web Service Call
                sw.Start();
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
                sw.Stop();
                string elapsed = sw.Elapsed.TotalSeconds.ToString();

                //Check Status of Azure ML Web Service Call
                if (response.IsSuccessStatusCode)
                {
                    //Read the http response
                    string MLResp = await response.Content.ReadAsStringAsync(); //.ConfigureAwait(false);

                    //Todo: Model bind
                    var  responseObj = JsonConvert.DeserializeObject<RootObject>(MLResp);

                    //Parse ML Web Service Response and return a populated IncomePredictionResults response recored
                    //incomePredictionResults = ParseMLResponse(MLResp);

                    incomePredictionResults = responseObj.Results.output1[0];
                    //Update for ML Service Response Time
                    incomePredictionResults.MLResponseTime = elapsed;
                }
                else
                {
                    incomePredictionResults.MLPrediction = response.ReasonPhrase.ToString();
                }

                client.Dispose();
                //return Ok(incomePredictionResults);

                return incomePredictionResults;
            }

        }

        private IncomePredictionResults ParseMLResponse(string result)
        {
            var cleaned = result.Replace("\"",string.Empty);
            cleaned = cleaned.Replace("[", string.Empty);
            cleaned = cleaned.Replace("]", string.Empty);

            string[] mlResultsArr = cleaned.Split(",".ToCharArray()); //Convert  ","  to character array

            IncomePredictionResults incomePredictionResults = new IncomePredictionResults();

            for (int i = 0; i < mlResultsArr.Length; i++)
            {
                switch(i)
                {
                    case 0:
                        incomePredictionResults.Age = mlResultsArr[i].ToString(); break;
                    case 1:
                        incomePredictionResults.Education = mlResultsArr[i].ToString(); break;
                    case 2:
                        incomePredictionResults.MaritalStatus = mlResultsArr[i].ToString(); break;
                    case 3:
                        incomePredictionResults.Relationship = mlResultsArr[i].ToString(); break;
                    case 4:
                        incomePredictionResults.Race = mlResultsArr[i].ToString(); break;
                    case 5:
                        incomePredictionResults.Sex = mlResultsArr[i].ToString(); break;
                    default:
                        break;
                }
            }

            return incomePredictionResults;
            //throw new NotImplementedException();
        }
    }
}
