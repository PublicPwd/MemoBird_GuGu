using MemoBird_GuGu.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace MemoBird_GuGu.Utils
{
    class CheckUpdate
    {
        private static string ResponseString { get; set; }

        public static void Check()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    webClient.Headers.Add("User-Agent", ProgramInfo.UserAgent);
                    webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
                    webClient.DownloadStringAsync(new Uri(ProgramInfo.UpdateUrl));
                }
            }
            catch { }
        }

        private static void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                ResponseString = e.Result;
                string tagName = GetTagName();
                if (!IsNewVersion(tagName))
                {
                    return;
                }
                if (MessageBox.Show(Application.Current.FindResource("haveanewversion").ToString(), string.Empty, MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
                string url = GetBrowserDownloadUrl();
                string savePath = FileX.GetSaveFilePath(Path.GetFileName(url));
                if (savePath.Length == 0)
                {
                    return;
                }
                DownloadFile(url, savePath);
            }
            catch { }
        }

        private static string GetTagName()
        {
            string tagName = "0.0.0.0";
            int head = ResponseString.IndexOf("\"tag_name\"");
            Regex regex = new Regex("[0-9]+.[0-9]+.[0-9]+.[0-9]+");
            tagName = regex.Match(ResponseString, head, 30).Value;
            return tagName;
        }

        private static string GetBrowserDownloadUrl()
        {
            string url = string.Empty;
            int head = ResponseString.IndexOf("\"browser_download_url\"");
            string str = ResponseString.Substring(head);
            head = str.IndexOf("http");
            str = str.Substring(head);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\"')
                {
                    url = stringBuilder.ToString();
                    break;
                }
                stringBuilder.Append(str[i]);
            }
            return url;
        }

        private static bool IsNewVersion(string version)
        {
            bool bRe = false;
            List<string> localVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.').ToList();
            List<string> remoteVersion = version.Split('.').ToList();
            for (int i = 0; i < 4; i++)
            {
                if (int.Parse(remoteVersion[i]) > int.Parse(localVersion[i]))
                {
                    bRe = true;
                    break;
                }
            }
            return bRe;
        }

        private static void DownloadFile(string url, string fileName)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                webClient.DownloadFileAsync(new Uri(url), fileName);
            }
        }

        private static void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show(Application.Current.FindResource("downloadsuccess").ToString());
        }
    }
}
