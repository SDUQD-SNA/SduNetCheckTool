using System;
using System.Diagnostics;
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
                //Timeout = TimeSpan.FromMilliseconds(5000) //超时时间 3,000 ms 
                //山大镜像站访问会出现超时情况
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.102 Safari/537.36 Edg/104.0.1293.70");
            try
            {
                return client.SendAsync(requestMessage).Result;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
                return null;
            }
        }
    }
}
