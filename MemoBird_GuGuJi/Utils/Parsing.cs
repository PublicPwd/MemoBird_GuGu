using System.Linq;
using System.Xml.Linq;

namespace MemoBird_GuGu.Utils
{
    class Parsing
    {
        /// <summary>
        /// 从 Json 字符串中获取相关信息
        /// </summary>
        /// <param name="jsonString">Json 字符串</param>
        /// <param name="field">需要获取的字段名</param>
        /// <returns></returns>
        public static string GetValueFromJsonString(string jsonString, string field)
        {
            int index = jsonString.IndexOf(field);
            string value = jsonString.Substring(index, jsonString.Length - index).Replace(field + "\":", string.Empty);
            if (value[0].Equals('\"'))
            {
                value = value.Substring(1, value.Length - 1);
            }
            value = value.Substring(0, value.IndexOfAny(new char[] { ',', '\"', '}' }));
            return value;
        }

        /// <summary>
        /// 把 Xaml 格式字符串转换成 Html 格式字符串
        /// </summary>
        /// <param name="xamlString"></param>
        /// <returns></returns>
        public static string XamlToHtml(string xamlString)
        {
            XElement body = XElement.Parse(xamlString);

            body.Name = "body";
            body.Attributes().ToList().ForEach(a =>
            {
                a.Remove();
            });
            body.SetAttributeValue("style", "max-width:384px;");

            foreach (var p in body.Elements())
            {
                p.Name = "p";
                p.Attributes().ToList().ForEach(a =>
                {
                    a.Remove();
                });

                foreach (var div in p.Elements())
                {
                    div.Name = "span";
                    string fontSize = string.Empty;
                    string fontFamily = string.Empty;
                    div.Attributes().ToList().ForEach(a =>
                    {
                        if (a.Name == "FontSize")
                        {
                            fontSize = a.Value;
                        }
                        else if (a.Name == "FontFamily")
                        {
                            fontFamily = a.Value;
                        }
                        a.Remove();
                    });
                    string style = string.Empty;
                    if (fontSize.Length > 0)
                    {
                        style = style + "font-size:" + fontSize + "px;";
                    }
                    if (fontFamily.Length > 0)
                    {
                        style = style + "font-family:" + fontFamily + ";";
                    }
                    if (style.Length > 0)
                    {
                        div.SetAttributeValue("style", style);
                    }
                }
            }
            return body.ToString();
        }
    }
}
