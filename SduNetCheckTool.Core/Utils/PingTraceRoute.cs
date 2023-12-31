﻿using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace SduNetCheckTool.Core.Utils
{
    internal class PingTraceRoute : IDisposable
    {
        private string targetIP;
        private int maxHops;
        private int timeout;
        private List<string> resultList;

        /// <summary>
        /// 初始化一个<see cref="PingTraceRoute"/>类
        /// </summary>
        /// <param name="targetIP">目标IP</param>
        /// <param name="maxHops">最大跃数</param>
        /// <param name="timeout">超时时间(ms)</param>
        public PingTraceRoute(string targetIP, int maxHops, int timeout)
        {
            this.targetIP = targetIP;
            this.maxHops = maxHops;
            this.timeout = timeout;
            resultList = new List<string>();
        }

        public string GetResults() => string.Join("\n", resultList);
        public void Trace()
        {
            resultList.Clear();

            // 创建Ping实例
            Ping pingSender = new Ping();
            PingOptions pingOptions = new PingOptions();
            // 创建缓冲区
            byte[] buffer = Encoding.ASCII.GetBytes("test");
            // 初始化TTL值
            pingOptions.Ttl = 1;

            resultList.Add($"正在跟踪路由到 {targetIP} 的路径，最多 {maxHops} 个跳点");

            for (int i = 1; i <= maxHops; i++)
            {
                // 发送Ping请求
                PingReply reply = pingSender.Send(targetIP, timeout, buffer, pingOptions);

                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add($"{i}\t{reply.RoundtripTime} ms\t{reply.Address}");
                    break;
                }

                if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add($"{i}\t{reply.RoundtripTime} ms\t{reply.Address}");
                    pingOptions.Ttl++;
                }
                else
                {
                    resultList.Add($"{i}\t*\t请求超时。");
                }
            }
        }

        public void Dispose()
        {
            resultList.Clear();
        }
    }
}
