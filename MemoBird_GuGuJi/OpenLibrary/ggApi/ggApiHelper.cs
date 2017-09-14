using System;

namespace MemoBird_GuGuJi.OpenLibrary.ggApi
{
    public static class ggApiHelper
    {
        public static string UserBind(string memobirdId, string useridentifying)
        {
            Apis apis = new Apis(GGConfig.ak, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string url = GGConfig.url + "/home/setuserbind";
            string res = apis.UserBind(url, memobirdId, useridentifying);
            return res;
        }
        public static string PrintPaper(string memobirdID, string userID, string printcontent)
        {
            Apis apis = new Apis(GGConfig.ak, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string url = GGConfig.url + "/home/printpaper";
            string res = apis.PrintPaper(url, memobirdID, userID, printcontent);
            return res;
        }
        public static string GetPrintStatus(string printcontentID)
        {
            Apis apis = new Apis(GGConfig.ak, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string url = GGConfig.url + "/home/getprintstatus";
            string res = apis.GetPrintStatus(url, printcontentID);
            return res;
        }
    }
}
