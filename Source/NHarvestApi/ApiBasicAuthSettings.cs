using System;
using System.Text;

namespace NHarvestApi
{
    public class ApiBasicAuthSettings
    {
        public ApiBasicAuthSettings(string subdomain, string username, string password, int maxResponseContentBufferSize = 256000, string httpAcceptValue = "application/json",
            string requestHeaderUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")
        {
            Guard.IsNotNullOrEmpty(subdomain, "subdomain");
            Guard.IsNotNullOrEmpty(username, "username");
            Guard.IsNotNullOrEmpty(password, "password");
            Guard.IsNotNullOrEmpty(httpAcceptValue, "httpAcceptValue");
            Guard.IsNotNullOrEmpty(requestHeaderUserAgent, "requestHeaderUserAgent");

            Subdomain = subdomain;
            Username = username;
            MaxResponseContentBufferSize = maxResponseContentBufferSize;
            HttpAcceptValue = httpAcceptValue;
            RequestHeaderUserAgent = requestHeaderUserAgent;
            BasicAuthBase64Value = GetBasicAuthBase64Value(username, password);
            BaseUri = new Uri("https://" + subdomain + ".harvestapp.com");
        }

        public string BasicAuthBase64Value { get; protected set; }

        public Uri BaseUri { get; protected set; }

        public int UserId { get; set; }

        public string Subdomain { get; protected set; }

        public string Username { get; protected set; }

        public int MaxResponseContentBufferSize { get; protected set; }

        public string HttpAcceptValue { get; protected set; }
        
        public string RequestHeaderUserAgent { get; protected set; }

        public Uri GetUriFromBase(string relativePath)
        {
            return new Uri(BaseUri, relativePath);
        }

        private static string GetBasicAuthBase64Value(string username, string password)
        {
            // https://github.com/harvesthq/harvest_api_samples/blob/master/harvest_api_sample.cs
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
        }
    }
}