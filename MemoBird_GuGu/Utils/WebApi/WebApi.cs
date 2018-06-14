using System;
using System.Collections.Generic;

namespace MemoBird_GuGu.Utils.WebApi
{
    class WebApi
    {
        private string AK { get; set; }

        public WebApi(string ak)
        {
            if (string.IsNullOrEmpty(ak))
            {
                throw new Exception();
            }
            AK = ak;
        }

        public string SetUserBind(string timeStamp, string memobirdID, string userIdentifying)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"ak", AK },
                {"timestamp", timeStamp },
                {"memobirdID", memobirdID },
                {"useridentifying", userIdentifying }
            };
            return HttpHelper.Post("http://open.memobird.cn/home/setuserbind", keyValuePairs);
        }

        public string PrintPaper(string timeStamp, string printContent, string memobirdID, string userID)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"ak", AK },
                {"timestamp", timeStamp },
                {"printcontent", printContent },
                {"memobirdID", memobirdID },
                {"userID", userID }
            };
            return HttpHelper.Post("http://open.memobird.cn/home/printpaper", keyValuePairs);
        }

        public string GetPrintStatus(string timeStamp, string printContentID)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"ak", AK },
                {"timestamp", timeStamp },
                {"printcontentid", printContentID }
            };
            return HttpHelper.Post("http://open.memobird.cn/home/getprintstatus", keyValuePairs);
        }

        public string PrintPaperFromHtml(string timeStamp, string printHtml, string memobirdID, string userID)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"ak", AK },
                {"timestamp", timeStamp },
                {"printHtml", printHtml },
                {"memobirdID", memobirdID },
                {"userID", userID }
            };
            return HttpHelper.Post("http://open.memobird.cn/home/printpaperFromHtml", keyValuePairs);
        }
    }
}
