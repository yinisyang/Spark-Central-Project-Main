using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace SparkAPI.MessageHandlers
{
    public class APIKeyMessageHandler : DelegatingHandler
    {

        private const string APIKey = "254a2c54-5e21-4e07-b2aa-590bc545a520";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            var checkApiKeyExists = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);
            if (checkApiKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(APIKey))
                {
                    validKey = true;
                }
            }

            if (!validKey)
            {
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");
            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);
            return response;
        }
    }
}