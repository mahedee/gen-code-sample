using System;
using System.Collections.Specialized;
using System.Web.Configuration;

namespace _API
{
    public class Merchant
    {
        public Boolean Debug { get; set; }
        public Boolean UseSsl { get; set; }
        public Boolean IgnoreSslErrors { get; set; }

        public String GatewayHost { get; set; }
        public String Version { get; set; }
        public String GatewayUrl { get; set; }

        public Boolean UseProxy { get; set; }
        public String ProxyHost { get; set; }
        public String ProxyUser { get; set; }
        public String ProxyPassword { get; set; }
        public String ProxyDomain { get; set; }

        public String MerchantId { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }

        public Merchant()
        {
            NameValueCollection appSettings = WebConfigurationManager.AppSettings;

            this.Debug = Convert.ToBoolean(appSettings["Debug"]);
            this.UseSsl = Convert.ToBoolean(appSettings["UseSSL"]);
            this.IgnoreSslErrors = Convert.ToBoolean(appSettings["IgnoreSslErrors"]);

            this.GatewayHost = appSettings["GatewayHost"];
            this.Version = appSettings["Version"];

            this.UseProxy = Convert.ToBoolean(appSettings["UseProxy"]);
            this.ProxyHost = appSettings["ProxyHost"];
            this.ProxyUser = appSettings["ProxyUser"];
            this.ProxyPassword = appSettings["ProxyPassword"];
            this.ProxyDomain = appSettings["ProxyDomain"];

            this.MerchantId = appSettings["MerchantId"];
            this.Username = appSettings["Username"];
            this.Password = appSettings["Password"];
        }
    }
}