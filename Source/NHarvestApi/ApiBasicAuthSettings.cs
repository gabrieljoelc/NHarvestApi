using System;
using System.Text;

namespace NHarvestApi
{
    public class ApiBasicAuthSettings
    {
        public ApiBasicAuthSettings(string subdomain, string credentialsValue = null, int maxResponseContentBufferSize = 256000, string httpAcceptValue = "application/json",
            string requestHeaderUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")
        {
            Guard.IsNotNullOrEmpty(subdomain, "subdomain");
            Guard.IsNotNullOrEmpty(httpAcceptValue, "httpAcceptValue");
            Guard.IsNotNullOrEmpty(requestHeaderUserAgent, "requestHeaderUserAgent");

            Subdomain = subdomain;
            CredentialsValue = credentialsValue;
            MaxResponseContentBufferSize = maxResponseContentBufferSize;
            HttpAcceptValue = httpAcceptValue;
            RequestHeaderUserAgent = requestHeaderUserAgent;
            BaseUri = new Uri("https://" + subdomain + ".harvestapp.com");
        }

        public string CredentialsValue { get; protected internal set; }

        public Uri BaseUri { get; protected set; }
        
        public string Subdomain { get; protected set; }

        public string Username { get; set; }

        public int MaxResponseContentBufferSize { get; protected set; }

        public string HttpAcceptValue { get; protected set; }
        
        public string RequestHeaderUserAgent { get; protected set; }

        public Uri GetUriFromBase(string relativePath)
        {
            return new Uri(BaseUri, relativePath);
        }

        public void SetCredentials(string username, string password)
        {
            Username = username;
            // https://github.com/harvesthq/harvest_api_samples/blob/master/harvest_api_sample.cs
            CredentialsValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
        }
    }
}