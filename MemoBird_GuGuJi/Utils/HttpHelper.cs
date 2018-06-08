using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Windows;

namespace MemoBird_GuGu.Utils
{
    class HttpHelper
    {
        public static string Post(string url, Dictionary<string, string> parameters)
        {
            string response = string.Empty;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    NameValueCollection values = new NameValueCollection();
                    foreach (var key in parameters.Keys)
                    {
                        values.Add(key, parameters[key]);
                    }
                    byte[] buffer = webClient.UploadValues(url, values);
                    response = Encoding.UTF8.GetString(buffer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return response;
        }
    }
}
