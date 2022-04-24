using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Tpf.Utils
{
    public class FileHelper
    {
        #region 通过URL从远程服务器下载文件
        /// <summary>
        /// 通过URL从远程服务器下载文件
        /// </summary>
        public static void DownloadWebServerFile(string Url, string FileDirectory, string FileName)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                SaveBinaryFile(responseStream, FileDirectory, FileName);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw ex;
            }
        }



        #endregion


        #region 将二进制文件保存到磁盘
        /// <summary>
        /// 将二进制文件保存到磁盘
        /// </summary>
        /// <param name="FileStream">FileStream</param>
        /// <param name="FileDirectory">保存路径</param>
        /// <param name="FileName">文件名</param>
        private static void SaveBinaryFile(Stream FileStream, string FileDirectory, string FileName)
        {
            var filePath = Path.Combine(FileDirectory, FileName);
            try
            {
                if(!Directory.Exists(FileDirectory))
                {
                    Directory.CreateDirectory(FileDirectory);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                //创建本地文件写入流
                byte[] bArr = new byte[1024];
                int iTotalSize = 0;
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                int size = FileStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    iTotalSize += size;
                    fs.Write(bArr, 0, size);
                    size = FileStream.Read(bArr, 0, (int)bArr.Length);
                }
                fs.Close();
                FileStream.Close();
            }
            catch(Exception ex)
            {
                File.Delete(filePath);
                throw ex;
            }
        }
        #endregion
    }


}
