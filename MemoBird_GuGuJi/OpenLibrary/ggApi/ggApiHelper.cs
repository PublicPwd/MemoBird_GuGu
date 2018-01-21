using System;

namespace MemoBird_GuGu.OpenLibrary.ggApi
{
    public static class ggApiHelper
    {
        public static string UserBind(string memobirdId, string useridentifying)
        {
            Apis apis = new Apis(GGConfig.ak, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string url = GGConfig.url + "/home/setuserbind";
            return apis.UserBind(url, memobirdId, useridentifying);
        }

        public static string PrintPaper(string memobirdID, string userID, string printcontent)
        {
            Apis apis = new Apis(GGConfig.ak, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string url = GGConfig.url + "/home/printpaper";
            return apis.PrintPaper(url, memobirdID, userID, printcontent);
        }

        public static string GetPrintStatus(string printcontentID)
        {
            Apis apis = new Apis(GGConfig.ak, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string url = GGConfig.url + "/home/getprintstatus";
            return apis.GetPrintStatus(url, printcontentID);
        }
    }
}
