using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Utils
{
    /// <summary>
    /// 腾讯云数据库签名
    /// </summary>
    public class TencentCloundSignatrue
    {
        // 密钥参数
        private static string SECRET_ID = "AKIDz8krbsJ5yKBZQpn74WFkmLPx3*******";
        private static string SECRET_KEY = "Gu5t9xGARNpq86cd98joQYCN3*******";

        private static string service = "mongodb";
        private static string endpoint = "mongodb.tencentcloudapi.com";
        private static string region = "ap-guangzhou";
        private static string action = "";
        private static string version = "2019-07-25";
        private static string requestPayload = "";

        /// <summary>
        /// GenerateRequestDto
        /// </summary>
        /// <param name="SecretID">密钥ID</param>
        /// <param name="SecretKey">密钥KEY</param>
        /// <param name="Service"></param>
        /// <param name="Endpoint"></param>
        /// <param name="Region"></param>
        /// <param name="Version"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        public static TCRequestHeaderDto GenerateRequestDto(string SecretID, string SecretKey, string Service, string Endpoint, string Region, string Version, string Action = null, object RequestPayload = null)
        {
            var requestDto = new TCRequestHeaderDto
            {
                SecretID = SecretID,
                SecretKey = SecretKey,
                Service = Service,
                Endpoint = Endpoint,
                Region = Region,
                Version = Version,
                Action = Action,
                RequestPayload = JsonConvert.SerializeObject(RequestPayload, 
                    Formatting.None, 
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                //RequestPayload = ""
            };
            return requestDto;
        }

        protected static void Set(TCRequestHeaderDto dto)
        {
            SECRET_ID = dto?.SecretID;
            SECRET_KEY = dto?.SecretKey;
            service = dto?.Service;
            endpoint = dto?.Endpoint;
            region = dto?.Region;
            version = dto?.Version;
            action = dto?.Action;
            requestPayload = dto?.RequestPayload;
        }

        public static Dictionary<string, string> GenerateHeaders(TCRequestHeaderDto dto)
        {
            Set(dto);

            // 此处由于示例规范的原因，采用时间戳2019-02-26 00:44:25，此参数作为示例，如果在项目中，您应当使用：
            // DateTime date = DateTime.UtcNow;
            // 注意时区，建议此时间统一采用UTC时间戳，否则容易出错
            //DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1551113065);

            DateTime date = DateTime.UtcNow;
            //string requestPayload = "{\"Limit\": 1, \"Filters\": [{\"Values\": [\"\\u672a\\u547d\\u540d\"], \"Name\": \"instance-name\"}]}";

            Dictionary<string, string> headers = BuildHeaders(SECRET_ID, SECRET_KEY, service
                , endpoint, region, action, version, date, requestPayload);

            return headers;
        }

        protected static string SHA256Hex(string s)
        {
            using (SHA256 algo = SHA256.Create())
            {
                byte[] hashbytes = algo.ComputeHash(Encoding.UTF8.GetBytes(s));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashbytes.Length; ++i)
                {
                    builder.Append(hashbytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected static byte[] HmacSHA256(byte[] key, byte[] msg)
        {
            using (HMACSHA256 mac = new HMACSHA256(key))
            {
                return mac.ComputeHash(msg);
            }
        }

        protected static Dictionary<String, String> BuildHeaders(string secretid,
            string secretkey, string service, string endpoint, string region,
            string action, string version, DateTime date, string requestPayload)
        {
            string datestr = date.ToString("yyyy-MM-dd");
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long requestTimestamp = (long)Math.Round((date - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero) / 1000;
            // ************* 步骤 1：拼接规范请求串 *************
            string algorithm = "TC3-HMAC-SHA256";
            string httpRequestMethod = "POST";
            string canonicalUri = "/";
            string canonicalQueryString = "";
            string contentType = "application/json";
            string canonicalHeaders = "content-type:" + contentType + "; charset=utf-8\n" + "host:" + endpoint + "\n";
            string signedHeaders = "content-type;host";
            string hashedRequestPayload = SHA256Hex(requestPayload);
            string canonicalRequest = httpRequestMethod + "\n"
                + canonicalUri + "\n"
                + canonicalQueryString + "\n"
                + canonicalHeaders + "\n"
                + signedHeaders + "\n"
                + hashedRequestPayload;

            // ************* 步骤 2：拼接待签名字符串 *************
            string credentialScope = datestr + "/" + service + "/" + "tc3_request";
            string hashedCanonicalRequest = SHA256Hex(canonicalRequest);
            string stringToSign = algorithm + "\n" 
                + requestTimestamp.ToString() + "\n" 
                + credentialScope + "\n" 
                + hashedCanonicalRequest;

            // ************* 步骤 3：计算签名 *************
            byte[] tc3SecretKey = Encoding.UTF8.GetBytes("TC3" + secretkey);
            byte[] secretDate = HmacSHA256(tc3SecretKey, Encoding.UTF8.GetBytes(datestr));
            byte[] secretService = HmacSHA256(secretDate, Encoding.UTF8.GetBytes(service));
            byte[] secretSigning = HmacSHA256(secretService, Encoding.UTF8.GetBytes("tc3_request"));
            byte[] signatureBytes = HmacSHA256(secretSigning, Encoding.UTF8.GetBytes(stringToSign));
            string signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

            // ************* 步骤 4：拼接 Authorization *************
            string authorization = algorithm + " "
                + "Credential=" + secretid + "/" + credentialScope + ", "
                + "SignedHeaders=" + signedHeaders + ", "
                + "Signature=" + signature;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", authorization);
            headers.Add("Host", endpoint);
            headers.Add("Content-Type", contentType);
            //headers.Add("Content-Type", contentType + "; charset=utf-8");
            headers.Add("X-TC-Timestamp", requestTimestamp.ToString());
            headers.Add("X-TC-Version", version);
            headers.Add("X-TC-Action", action);
            headers.Add("X-TC-Region", region);
            headers.Add("X-TC-Language", "zh-CN");

            Console.WriteLine($"headers：{JsonConvert.SerializeObject(headers)}");

            return headers;
        }


        public static long ToTimestamp()
        {
            //DateTime startTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //DateTime nowTime = DateTime.UtcNow;
            //long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            //return unixTime;

            DateTimeOffset expiresAtOffset = DateTimeOffset.Now;
            var totalSeconds = expiresAtOffset.ToUnixTimeMilliseconds();
            return totalSeconds;

        }

    }

    /// <summary>
    /// 腾讯MongoDb云数据库 api接口请求头参数
    /// </summary>
    public class TCRequestHeaderDto
    {
        public string SecretID { get; set; }

        public string SecretKey { get; set; }

        public string Service { get; set; }
        public string Endpoint { get; set; }
        public string Region { get; set; }
        public string Action { get; set; }
        public string Version { get; set; }

        /// <summary>
        /// POST参数JSON字符串
        /// </summary>
        public string RequestPayload { get; set; }
    }
}
