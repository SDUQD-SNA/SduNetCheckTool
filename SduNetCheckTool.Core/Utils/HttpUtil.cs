using System;
using System.Net.Http;

namespace SduNetCheckTool.Core.Utils
{
    public static class HttpUtil
    {
        public static HttpResponseMessage GetHttpResponse(string url)
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = null,
                UseProxy = false
            }; //禁用代理
            var client = new HttpClient(httpClientHandler)
            {
                Timeout = TimeSpan.FromMilliseconds(3000) //超时时间 3,000 ms
            };
            try
            {
                return client.GetAsync(url).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
