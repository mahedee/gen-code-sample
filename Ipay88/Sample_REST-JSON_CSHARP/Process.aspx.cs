using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web;

namespace _API
{
    public partial class Process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String result = null;
            String gatewayCode = null;
            String response = null;

            // get the request form and make sure to UrlDecode each value in case special characters used
            NameValueCollection formData = new NameValueCollection();
            foreach (String key in Request.Form)
            {
                formData.Add(key, HttpUtility.UrlDecode(Request.Form[key]));
            }

            // get merchant information from web.config
            Merchant merchant = new Merchant();

            // [Snippet] howToConfigureURL - start
            StringBuilder url = new StringBuilder();
            if (!merchant.GatewayHost.StartsWith("http"))
                url.Append("https://");
            url.Append(merchant.GatewayHost);
            url.Append("/api/rest/version/");
            url.Append(merchant.Version);
            url.Append("/merchant/");
            url.Append(merchant.MerchantId);
            url.Append("/order/");
            url.Append(formData["order.id"]);
            url.Append("/transaction/");
            url.Append(formData["transaction.id"]);
            merchant.GatewayUrl = url.ToString();
            // [Snippet] howToConfigureURL - end

            // remove these two fields as they are passed in URL for REST/JSON implementation
            formData.Remove("order.id");
            formData.Remove("transaction.id");

            // [Snippet] howToConvertFormData -- start
            String data = Json.BuildJsonFromNVC(formData);
            // [Snippet] howToConvertFormData -- end

            // open connection
            Connection connection = new Connection(merchant);

            // send request/get results
            String operation = formData["apiOperation"];
            if (operation.Equals("RETRIEVE_TRANSACTION")) {
                response = connection.GetTransaction();
            } else {
                response = connection.SendTransaction(data);
            }

            // now convert JSON result string into a NameValueCollection
            NameValueCollection respValues = new NameValueCollection();
            respValues = Json.BuildNVCFromJson(response);

            // get overall success of transaction
            result = respValues["result"];

            // Form error string if error is triggered
            if (result != null && result.Equals("ERROR"))
            {
                String errorMessage = null;
                String errorCode = null;

                String failureExplanations = respValues["explanation"];
                String supportCode = respValues["supportCode"];

                if (failureExplanations != null)
                {
                    errorMessage = failureExplanations;
                }
                else if (supportCode != null)
                {
                    errorMessage = supportCode;
                }
                else
                {
                    errorMessage = "Reason unspecified.";
                }

                String failureCode = respValues["failureCode"];
                if (failureCode != null)
                {
                    errorCode = "Error (" + failureCode + ")";
                }
                else
                {
                    errorCode = "Error (UNSPECIFIED)";
                }

                // now add the values to result fields in panels
                lblErrorCode.Text = errorCode;
                lblErrorMessage.Text = errorMessage;
                pnlError.Visible = true;
            }

            // error or not display what response values can
            gatewayCode = respValues["response.gatewayCode"];
            if (gatewayCode == null)
            {
                gatewayCode = "Response not received.";
            }
            lblGateWayCode.Text = gatewayCode;
            lblResult.Text = result;

            // build table of NVP results and add to panel for results
            HtmlTable dTable = new HtmlTable();
            dTable.Width = "100%";
            dTable.CellPadding = 2;
            dTable.CellSpacing = 0;
            dTable.Border = 1;
            dTable.BorderColor = "#cccccc";

            int shade = 0;
            foreach (String key in respValues)
            {
                HtmlTableRow dtRow = new HtmlTableRow();
                if (++shade % 2 == 0) dtRow.Attributes.Add("class", "shade");

                HtmlTableCell dtLeft = new HtmlTableCell();
                HtmlTableCell dtRight = new HtmlTableCell();

                dtLeft.Align = "right";
                dtLeft.Width = "50%";
                dtLeft.InnerHtml = "<strong><i>" + key + ":</i></strong>";  // add field name to table

                dtRight.Width = "50%";
                dtRight.InnerText = respValues[key]; // add value to table

                dtRow.Controls.Add(dtLeft);
                dtRow.Controls.Add(dtRight);
                dTable.Controls.Add(dtRow);
            }

            pnlFieldResults.Controls.Add(dTable);

            // add the raw response to the form
            txtRawResponse.Text = response;
        }
    }
}