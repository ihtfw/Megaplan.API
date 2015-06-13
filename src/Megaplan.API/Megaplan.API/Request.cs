namespace Megaplan.API
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class Request
    {
        private readonly byte[] postData;

        private readonly string requestMethod;

        private readonly string accessId;

        private readonly string secretKey;
        
        private HttpWebRequest request;

        readonly string endPoint;

        public Request(string requestMethod, string host, string requestHost, byte[] postData)
        {
            this.postData = postData;
            this.requestMethod = requestMethod.ToUpper();
            
            endPoint = "https://" + host + requestHost;
        }

        public Request(string requestMethod, string host, string requestHost, byte[]postData, string accessId, string secretKey)
            : this(requestMethod, host, requestHost,postData)
        {
            this.accessId = accessId;
            this.secretKey = secretKey;
        }

        private void Sign(HttpWebRequest request)
        {
            if (string.IsNullOrEmpty(accessId))
                return;
            
            DateTime requestDate = DateTime.UtcNow;

            string requestDateRfc = requestDate.ToString("r", CultureInfo.InvariantCulture);
            string signature = string.Join("\n", request.Method, "", request.ContentType, requestDateRfc, endPoint.Replace(@"https://", ""));
            var hash = Convert.ToBase64String(Encoding.UTF8.GetBytes(Hashes.HMACSHA1(signature, secretKey)));
            request.SetRawHeader("Date", requestDateRfc);
            request.SetRawHeader("UserAgent", "SdfApi_Request");
            request.Headers["X-Authorization"] = accessId + ":" + hash;
        }

        private async Task Create()
        {
            request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = requestMethod;
            if (postData != null)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.SetRawHeader("ContentLenght", postData.Length.ToString());
                using (var stream = await request.GetRequestStreamAsync())
                {
                    stream.Write(postData, 0, postData.Length);
                }
            }
            switch (requestMethod)
            {
                case "POST":
                    request.Accept = "application/json";
                    break;
                case "GET":
                    request.Accept = "application/json";
                    break;
                case "PUT":
                    break;
                default:
                    throw new NotSupportedException(requestMethod);
            }

            Sign(request);
        }

        public async Task<HttpWebResponse> GetResponse()
        {
            await Create();
            return (HttpWebResponse)(await Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), null));
        }
    }
}