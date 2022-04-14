using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Grpc
{
    /// <summary>
    /// GrpcService
    /// </summary>
    public class GrpcService : IGrpcService
    {
        private string ServerAddress = string.Empty;
        private GrpcChannel grpcChannel = null;

        public GrpcService()
        {
            if (string.IsNullOrWhiteSpace(ServerAddress))
            {
                ServerAddress = ""; // TODO: get from default config
            }
        }

        public void SetServer(string serverAddress)
        {
            this.ServerAddress = serverAddress;
        }

        public GrpcChannel GetChannel(string serverAddress = null)
        {
            if (!string.IsNullOrWhiteSpace(serverAddress))
            {
                grpcChannel = GrpcChannel.ForAddress(ServerAddress);
                return grpcChannel;
            }

            if (grpcChannel == null)
            {
                grpcChannel = GrpcChannel.ForAddress(ServerAddress);
            }
            return grpcChannel;
        }

        public T GetClient<T>() where T : class
        {
            var client = this.GetChannel().CreateGrpcService<T>();
            return client;
        }

        public T GetClient<T>(string serverAddress) where T : class
        {
            var client = this.GetChannel().CreateGrpcService<T>();
            return client;
        }
    }
}
