using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace _NVP
{
    public class Connection
    {
        private Merchant _merchant;

        public Connection(Merchant merchant)
        {
            this._merchant = merchant;
        }

        // [Snippet] howToConfigureSslCert - start
        // This snippet will be provided soon
        // [Snippet] howToConfigureSslCert - end

        public String SendTransaction(String data)
        {
            string body = String.Empty;

            // code to validate certificat in an SSL conversation
            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                if (_merchant.IgnoreSslErrors)
                {
                    // allow any old dodgy certificate...
                    return true;
                }
                else
                {
                    return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
                }
            };

            // [Snippet] howToConfigureProxy - start
            if (_merchant.UseProxy)
            {
                WebProxy proxy = new WebProxy(_merchant.ProxyHost, true);
                if (!String.IsNullOrEmpty(_merchant.ProxyUser))
                {
                    if (String.IsNullOrEmpty(_merchant.ProxyDomain))
                    {
                        proxy.Credentials = new NetworkCredential(_merchant.ProxyUser, _merchant.ProxyPassword);
                    }
                    else
                    {
                        proxy.Credentials = new NetworkCredential(_merchant.ProxyUser, _merchant.ProxyPassword, _merchant.ProxyDomain);
                    }
                }
                WebRequest.DefaultWebProxy = proxy;
            }
            // [Snippet] howToConfigureProxy - end

            // [Snippet] howToSetURL - start
            // Create the web request
            HttpWebRequest request = WebRequest.Create(_merchant.GatewayUrl) as HttpWebRequest;
            // [Snippet] howToSetURL - end

            // [Snippet] howToPut - start
            // Set type to PUT
            request.Method = "POST";
            // [Snippet] howToPut - end

            // [Snippet] howToSetHeaders - start
            request.ContentType = "application/x-www-form-urlencoded; charset=iso-8859-1";
            // [Snippet] howToSetHeaders - end

            //request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
            //request.Credentials = new NetworkCredential("", apiPassword);
            //request.PreAuthenticate = true;

            // [Snippet] howToSetCredentials - start
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_merchant.Username + ":" + _merchant.Password));
            request.Headers.Add("Authorization", "Basic " + credentials);
            // [Snippet] howToSetCredentials - end

            // Create a byte array of the data we want to send
            byte[] utf8bytes = Encoding.UTF8.GetBytes(data);
            byte[] iso8859bytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("iso-8859-1"), utf8bytes);

            // Set the content length in the request headers
            request.ContentLength = iso8859bytes.Length;

            // Ignore format error checks before sending body
            request.ServicePoint.Expect100Continue = false;

            try
            {
                // [Snippet] executeSendTransaction - start
                // Write data
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(iso8859bytes, 0, iso8859bytes.Length);
                }
                // [Snippet] executeSendTransaction - end

                // Get response
                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        // Get the response stream
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
                        body = reader.ReadToEnd();
                    }
                }
                catch (WebException wex)
                {
                    StreamReader reader = new StreamReader(wex.Response.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
                    body = reader.ReadToEnd();
                }
                return body;
            }
            catch (Exception ex)
            {
                return ex.Message + "\n\naddress:\n" + request.Address.ToString() + "\n\nheader:\n" + request.Headers.ToString() + "data submitted:\n" + data;
            }
        }
    }
}
