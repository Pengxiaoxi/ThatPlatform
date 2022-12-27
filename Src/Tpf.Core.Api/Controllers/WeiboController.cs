using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Tpf.Common.BaseWebApi;
using Tpf.Utils;

namespace Tpf.Core.Api.Controllers
{
    public class WeiboController : BaseApiController
    {
        public WeiboController()
        {

        }

        #region Public Method
        [HttpGet]
        public BlogResult<MyBlogResponse> MyMblog(string sinceId = null)
        {
            var url = "https://weibo.com/ajax/statuses/mymblog";
            var queryString = $"uid=5643507638&page=1&feature=0&since_id={sinceId}"; // 4801037951572866
            var headerDic = new Dictionary<string, string>()
            {
                { "authority", "weibo.com" },
                { "client-version", "v2.37.21" },
                { "cookie", "PC_TOKEN=4e873477ec; login_sid_t=3f7553de797b4f2ac165ecc747bc2b0a; cross_origin_proto=SSL; WBStorage=4d96c54e|undefined; _s_tentry=passport.weibo.com; Apache=5720993667034.307.1672152014144; SINAGLOBAL=5720993667034.307.1672152014144; ULV=1672152014146:1:1:1:5720993667034.307.1672152014144:; wb_view_log=1920*10801; SUB=_2A25Or3OMDeRhGeNI71EU8CnKyDSIHXVt3eJErDV8PUNbmtAfLRDSkW9NSHjDOgqVin9tn5cmpx9UE9dXWw_BjUjY; SUBP=0033WrSXqPxfM725Ws9jqgMF55529P9D9WW0O.KnBnC.aY2z.GFL5hgv5JpX5KzhUgL.Fo-cShefehMce0n2dJLoIEXLxKqL1-BL12-LxKqLBo-LBK.LxK-LB--L1hMLxKqL1-eLB-2LxK-LBK.L1-2t; ALF=1703688027; SSOLoginState=1672152029; XSRF-TOKEN=qhUO32KSIpfYXrOD80PAo7f4; WBPSESS=A1d-VVNhK7mlhVlwsl9N8iZe7X83bvoQEVdH12ah0JmUDBgEOVJRGuvg6qOMgxMy_MN0FeJnq2FMTiox2p1Qk779VC3Grdf_Y9-xNyUqVUTIBsw41knELujZVVDhrCBM2kp8zwOxbdU1-yH3u9Vogw==" },
                { "server-version", "v2022.12.27.1" },
                { "traceparent", "00-cd0074b935b03b7f33e48edb10c858ec-e7c9860c6c37b113-00" },
                { "x-requested-with", "XMLHttpRequest" },
                { "x-xsrf-token", "qhUO32KSIpfYXrOD80PAo7f4" }
            };
            return WebApiHelper.HttpGet<BlogResult<MyBlogResponse>>(url, queryString, headerDic);
        }

        [HttpPost]
        public BlogResult<object> Destroy(string blogId)
        {
            var url = "https://weibo.com/ajax/statuses/destroy";
            var reuqestBody = new DestroyBlogRequst(blogId);
            var headerDic = new Dictionary<string, string>()
            {
                { "authority", "weibo.com" },
                { "client-version", "v2.37.21" },
                { "cookie", "PC_TOKEN=4e873477ec; login_sid_t=3f7553de797b4f2ac165ecc747bc2b0a; cross_origin_proto=SSL; WBStorage=4d96c54e|undefined; _s_tentry=passport.weibo.com; Apache=5720993667034.307.1672152014144; SINAGLOBAL=5720993667034.307.1672152014144; ULV=1672152014146:1:1:1:5720993667034.307.1672152014144:; wb_view_log=1920*10801; SUB=_2A25Or3OMDeRhGeNI71EU8CnKyDSIHXVt3eJErDV8PUNbmtAfLRDSkW9NSHjDOgqVin9tn5cmpx9UE9dXWw_BjUjY; SUBP=0033WrSXqPxfM725Ws9jqgMF55529P9D9WW0O.KnBnC.aY2z.GFL5hgv5JpX5KzhUgL.Fo-cShefehMce0n2dJLoIEXLxKqL1-BL12-LxKqLBo-LBK.LxK-LB--L1hMLxKqL1-eLB-2LxK-LBK.L1-2t; ALF=1703688027; SSOLoginState=1672152029; XSRF-TOKEN=qhUO32KSIpfYXrOD80PAo7f4; WBPSESS=A1d-VVNhK7mlhVlwsl9N8iZe7X83bvoQEVdH12ah0JmUDBgEOVJRGuvg6qOMgxMy_MN0FeJnq2FMTiox2p1Qk779VC3Grdf_Y9-xNyUqVUTIBsw41knELujZVVDhrCBM2kp8zwOxbdU1-yH3u9Vogw==" },
                { "server-version", "v2022.12.27.1" },
                { "traceparent", "00-cd0074b935b03b7f33e48edb10c858ec-e7c9860c6c37b113-00" },
                { "x-requested-with", "XMLHttpRequest" },
                { "x-xsrf-token", "qhUO32KSIpfYXrOD80PAo7f4" }
            };
            return WebApiHelper.HttpPost<BlogResult<object>>(url, reuqestBody, headerDic);
        }

        [HttpGet]
        public BlogResult<object> BatchDestoryAutomatic(string sinceId)
        {
            var curPageBlogsResult = this.MyMblog(sinceId);
            if (curPageBlogsResult.Ok != 1)
            {
                return new BlogResult<object>() { Ok = -1, Message = curPageBlogsResult.Message, ErrorCode = curPageBlogsResult.ErrorCode };
            }
            if (curPageBlogsResult.Data == null
                || curPageBlogsResult.Data.List == null
                || curPageBlogsResult.Data.List.Count == 0)
            {
                return new BlogResult<object>() { Ok = 1, Message = "全部删除完成" };
            }

            var curPageBlogs = curPageBlogsResult.Data.List;
            sinceId = curPageBlogs.LastOrDefault().IdStr;
            foreach (var blog in curPageBlogs)
            {
                //if (blog.IdStr == sinceId)
                //{
                //    continue;
                //}

                this.Destroy(blog.IdStr);
                System.Console.WriteLine($"Destroy Successful, MBlog Id: {blog.IdStr}");
            }

            this.BatchDestoryAutomatic(sinceId);

            return new BlogResult<object>() { Ok = 1 };
        }
        #endregion
    }


    #region Dto
    public class BlogResult<T> where T : class
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("ok")]
        public int Ok { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

    }

    public class MyBlogResponse
    {
        [JsonProperty("since_id")]
        public string SinceId { get; set; }

        [JsonProperty("list")]
        public List<BlogDetail> List { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

    }

    public class BlogDetail
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("idstr")]
        public string IdStr { get; set; }

        [JsonProperty("text_raw")]
        public string TextRaw { get; set; }

    } 

    public class DestroyBlogRequst
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        public DestroyBlogRequst()
        {

        }

        public DestroyBlogRequst(string id)
        {
            this.Id = id;
        }
    }
    #endregion

}
