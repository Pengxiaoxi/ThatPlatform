using Grpc.Net.Client;
using System;

namespace ThatPlatform.Grpc.Client
{
    /// <summary>
    /// IGrpcService
    /// </summary>
    public interface IGrpcService
    {
        /// <summary>
        /// GetChannel
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        GrpcChannel GetChannel(string serverAddress);

        /// <summary>
        /// GetClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        T GetClient<T>(string serverAddress) where T : class;


    }
}
