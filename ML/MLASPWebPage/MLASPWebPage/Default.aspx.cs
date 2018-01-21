using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MLASPWebPage
{

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrediction_Click(object sender, EventArgs e)
        {

            InvokeRequestResponseService().Wait();
        }


        private async Task InvokeRequestResponseService()
        {
            string inAge = this.txtAge.Text;
            string inEducation = this.ddlEducation.SelectedItem.ToString();
            string inMaritalStatus = this.ddlMaritalStatus.SelectedItem.ToString();
            string inRelationship = this.ddlRelationship.SelectedItem.ToString();
            string inRace = this.ddlRace.SelectedItem.ToString();
            string inSex = this.ddlSex.SelectedItem.ToString();


            using(var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"age", "education", "marital-status", "relationship", "race", "sex"},
                                Values = new string[,] {  { inAge, inEducation, inMaritalStatus, inRelationship, inRace, inSex },  
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };


                const string apiKey = "QjBI1INingF4w2AsUxi2kT14VUmTTI0onfori/OJswSiFKnLsZqlLedRl+1A4s2HD2hy4h/y1Y782ihl0ZePhw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/9f5a46e7452b4199929a1fb90f226160/services/c283330d7dd44270b7565790e2b1f24a/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                //For Web form In a nutshell, you have to use the ConfigureAwait(false) extension to avoid the deadlock:
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

      
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    //Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

    }

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; } //Two dimentional array
    }
}