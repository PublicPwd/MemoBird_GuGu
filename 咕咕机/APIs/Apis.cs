using System;
using System.Collections.Generic;
using System.Text;

namespace MemoBird.APIs
{
    public class Apis
    {
        public string ak;
        public string timestamp;
        public Apis(string ak, string timestamp)
        {
            this.ak = ak;
            this.timestamp = timestamp;
        }
        /// <summary>
        /// 第三方软件用户绑定设备
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="memobirdId">咕咕机设备Guid</param>
        /// <param name="useridentifying">第三方用户id</param>
        public string UserBind(string url, string memobirdId, string useridentifying)
        {

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("ak", ak);
                parameters.Add("timestamp", timestamp);
                parameters.Add("memobirdID", memobirdId);
                parameters.Add("useridentifying", useridentifying);
                var res = HttpWebResponseUtility.CreatePostHttpResponse(url, parameters, null, null, Encoding.UTF8, null);
                var resstr = res.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(resstr);
                string resstring = sr.ReadToEnd();
                return resstring;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        /// <summary>
        /// 打印纸条（图片为单色点位图）
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="memobirdId">咕咕机设备ID</param>
        /// <param name="userId">注册用户ID</param>
        /// <param name="printcontent">打印内容</param>
        /// <returns></returns>
        public string PrintPaper(string url, string memobirdID, string userID, string printcontent)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ak", ak);
            parameters.Add("timestamp", timestamp);
            parameters.Add("memobirdID", memobirdID);
            parameters.Add("userID", userID);
            parameters.Add("printcontent", printcontent);
            var res = HttpWebResponseUtility.CreatePostHttpResponse(url, parameters, null, null, Encoding.UTF8, null);
            var resstr = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(resstr);
            var resstring = sr.ReadToEnd();
            return resstring;
        }
        /// <summary>
        /// 获取纸条打印状态
        /// </summary>
        /// <param name="url"></param>
        /// <param name="printcontentID"></param>
        /// <returns></returns>
        public string GetPrintStatus(string url, string printcontentID)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ak", ak);
            parameters.Add("timestamp", timestamp);
            parameters.Add("printcontentID", printcontentID);
            var res = HttpWebResponseUtility.CreatePostHttpResponse(url, parameters, null, null, Encoding.UTF8, null);
            var resstr = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(resstr);
            var resstring = sr.ReadToEnd();
            return resstring;

        }
    }
}
