using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ThatPlatform.Tools.UtilLibrary
{
    public static class WebApiHelper
    {
        public const string ContentType_AppJson = "application/json";

        /// <summary>
        /// WebApi  POST<T>
        /// </summary>
        /// <typeparam name="T">return of Type</typeparam>
        /// <param name="url">post The URL.</param>
        /// <param name="bodyObj">Post  body  object.</param>
        /// <returns></returns>
        public static T HttpPost<T>(string url, object bodyObj = null, Dictionary<string, string> headers = null) where T : class
        {
            string bodyJson = "";
            if (bodyObj != null)
            {
                bodyJson = JsonConvert.SerializeObject(bodyObj,
                    Formatting.None,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            var ret = HttpPost(url, bodyJson, headers);
            return JsonConvert.DeserializeObject<T>(ret);
        }

        /// <summary>
        /// WebApi  POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="headers"></param>
        /// <param name="timeout"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string postDataStr, Dictionary<string, string> headers = null, int timeout = 120000, string contentType = ContentType_AppJson)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;  //"application/x-www-form-urlencoded"; "application/json"
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.Timeout = timeout;
            if (headers != null && headers.Keys.Count > 0)
            {
                foreach (var k in headers)
                {
                    request.Headers.Add(k.Key, k.Value);
                }
            }

            using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
            {
                requestStream.Write(postDataStr);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string retString = "";
            using (StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                retString = myStreamReader.ReadToEnd();
            }
            return retString;
        }

        /// <summary>
        /// WebApi  GET<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T HttpGet<T>(string url, string queryString = "", Dictionary<string, string> headers = null) where T : class
        {
            var ret = HttpGet(url, queryString, headers);
            return JsonConvert.DeserializeObject<T>(ret);
        }

        /// <summary>
        /// WebApi  GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string HttpGet(string url, string queryString = "", Dictionary<string, string> headers = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (string.IsNullOrEmpty(queryString) ? "" : "?") + queryString);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                if (headers != null && headers.Keys.Count > 0)
                {
                    foreach (var k in headers)
                        request.Headers.Add(k.Key, k.Value);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                throw new Exception($"{Environment.NewLine} class:WebHttpUtil.HttpGet url:{url} query:{queryString}  ex:{ex.Message}");
            }
        }

    }
}
