using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using ProtoBuf.Grpc.Client;
using System;
using System.Net.Http;
using Tpf.Infrastructure.CommonAttributes;

namespace Tpf.Grpc.Client
{
    /// <summary>
    /// GrpcService
    /// https://docs.microsoft.com/zh-cn/aspnet/core/grpc/client?view=aspnetcore-5.0
    /// https://docs.microsoft.com/zh-cn/aspnet/core/grpc/code-first?view=aspnetcore-5.0
    /// </summary>
    [DependsOn(typeof(IGrpcService))]
    public class GrpcService : IGrpcService
    {
        private string ServerAddress = string.Empty;
        private GrpcChannel grpcChannel = null;

        /// <summary>
        /// 默认配置
        /// </summary>
        private MethodConfig defaultMethodConfig = new MethodConfig
        {
            Names = { MethodName.Default },
            // 重试次数：3
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = 3,
                InitialBackoff = TimeSpan.FromSeconds(1),
                MaxBackoff = TimeSpan.FromSeconds(5),
                BackoffMultiplier = 1.5,
                RetryableStatusCodes = { StatusCode.Unavailable }
            }
        };

        /// <summary>
        /// Ctor
        /// </summary>
        public GrpcService()
        {
            
        }

        /// <summary>
        /// GetClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        public T GetClient<T>(string serverAddress) where T : class
        {
            var client = this.GetChannel(serverAddress).CreateGrpcService<T>();
            return client;
        }

        /// <summary>
        /// GetChannel
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        public GrpcChannel GetChannel(string serverAddress)
        {
            if (!string.IsNullOrWhiteSpace(serverAddress))
            {
                if (this.ServerAddress != serverAddress)
                {
                    this.ServerAddress = serverAddress.Trim();
                    this.GetChannelByOptions();
                }
                return grpcChannel;
            }

            if (grpcChannel == null)
            {
                this.GetChannelByOptions();
            }
            return grpcChannel;
        }

        /// <summary>
        /// GetChannelByOptions
        /// </summary>
        private void GetChannelByOptions()
        {
            this.grpcChannel = GrpcChannel.ForAddress(this.ServerAddress, new GrpcChannelOptions
            {
                //HttpClient = null,
                //ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } },
                HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }
            });
        }

        


    }
}
