namespace MemoBird_GuGuJi.Utils
{
    class Parsing
    {
        /// <summary>
        /// 从 Json 字符串中获取相关信息
        /// </summary>
        /// <param name="str">Json 字符串</param>
        /// <param name="field">需要获取的字段名</param>
        /// <returns></returns>
        public static string GetUserIDFromJsonString(string str, string field)
        {
            int index = str.IndexOf(field);
            string value = str.Substring(index, str.Length - index).Replace(field + "\":", string.Empty);
            if (value[0].Equals('\"'))
            {
                value = value.Substring(1, value.Length - 1);
            }
            char[] parms = { ',', '\"', '}' };
            value = value.Substring(0, value.IndexOfAny(parms));
            return value;
        }
    }
}
