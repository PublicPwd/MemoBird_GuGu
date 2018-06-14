using System;

namespace MemoBird_GuGu.Utils.WebApi
{
    class WebApiHelper
    {
        /*
         * if publish, change to 
         * private static WebApi webApi = new WebApi("your ak");
         */
        //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
        private static WebApi webApi = new WebApi(FileX.LoadConfig("ak.txt"));
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        #region Public Functions

        public static string PrintPaper(string printContent, string memobirdID)
        {
            if (string.IsNullOrEmpty(printContent))
            {
                throw new Exception();
            }
            if (string.IsNullOrEmpty(memobirdID))
            {
                throw new Exception();
            }
            string str = SetUserBind(memobirdID);
            string userID = Parsing.GetValueFromJsonString(str, "showapi_userid");
            return webApi.PrintPaper(DateTime.Now.ToCustomString(), printContent, memobirdID, userID);
        }

        public static string GetPrintStatus(string printContentID)
        {
            if (string.IsNullOrEmpty(printContentID))
            {
                throw new Exception();
            }
            return webApi.GetPrintStatus(DateTime.Now.ToCustomString(), printContentID);
        }

        public static string PrintPaperFromHtml(string printHtml, string memobirdID)
        {
            if (string.IsNullOrEmpty(printHtml))
            {
                throw new Exception();
            }
            if (string.IsNullOrEmpty(memobirdID))
            {
                throw new Exception();
            }
            string str = SetUserBind(memobirdID);
            string userID = Parsing.GetValueFromJsonString(str, "showapi_userid");
            return webApi.PrintPaperFromHtml(DateTime.Now.ToCustomString(), printHtml, memobirdID, userID);
        }

        #endregion

        #region Private Functions

        private static string SetUserBind(string memobirdID)
        {
            if (string.IsNullOrEmpty(memobirdID))
            {
                throw new Exception();
            }
            return webApi.SetUserBind(DateTime.Now.ToCustomString(), memobirdID, "0");
        }

        #endregion
    }

    static class CustomFormat
    {
        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string ToCustomString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
