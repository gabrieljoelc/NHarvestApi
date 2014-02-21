using System;
using APeAye;

namespace NHarvestApi
{
    public class HarvestApiBasicAuthSettings : ApiBasicAuthSettings
    {
        public HarvestApiBasicAuthSettings(string subdomain, string credentialsValue = null,
            int maxResponseContentBufferSize = 256000, string httpAcceptValue = "application/json",
            string requestHeaderUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")
            : base(new Uri("https://" + subdomain + ".harvestapp.com"), credentialsValue, maxResponseContentBufferSize, httpAcceptValue, requestHeaderUserAgent)
        {
            Guard.IsNotNullOrEmpty(subdomain, "subdomain");
            Guard.IsNotNullOrEmpty(httpAcceptValue, "httpAcceptValue");
            Guard.IsNotNullOrEmpty(requestHeaderUserAgent, "requestHeaderUserAgent");

            Subdomain = subdomain;
            CredentialsValue = credentialsValue;
            MaxResponseContentBufferSize = maxResponseContentBufferSize;
            HttpAcceptValue = httpAcceptValue;
            RequestHeaderUserAgent = requestHeaderUserAgent;
        }
    }
}