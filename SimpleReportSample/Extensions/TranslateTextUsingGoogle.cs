using MSXML2;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleReportSample.Extensions
{
    public class TranslateTextUsingGoogle
    {
        public static string GetTranslate(string textToTranslate, string translateTo)
        {
            var translateFrom = "en";
            string URL = $"https://translate.google.com/m?hl={translateFrom}&sl={translateFrom}&tl={translateTo}&ie=UTF-8&prev=_m&q={textToTranslate}";
            //string URL = $"https://translate.google.com/m?hl={translateFrom}&sl={translateFrom}&tl={translateTo}&ie=UTF-8&prev=_m&q={textToTranslate}";
            ServerXMLHTTP srv = new ServerXMLHTTP();
            srv.open("GET", URL, false, null, null);
            srv.send("");


            GetTranslateWebRequest(textToTranslate);

            if (srv.status == 200)
            {
                string markupResponse;
                markupResponse = srv.responseText;

                return RegexExecute(markupResponse, "class=\\\"result-container\\\">(.+?)</div>");
            }

            return string.Empty;
        }

        public static string GetTranslateWebRequest(string textToTranslate, string translateTo = "ru")
        {
            var translateFrom = "en";
            string URL = $"https://translate.google.com/m?hl={translateFrom}&sl={translateFrom}&tl={translateTo}&ie=UTF-8&prev=_m&q={textToTranslate}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string html = reader.ReadToEnd();
            // Extract out trans":"...[Extracted]...","from the JSON string
            string result = Regex.Match(html, "class=\\\"result-container\\\">(.+?)</div>", RegexOptions.Singleline).Groups[1].Value;
            reader.Close();
            dataStream.Close();
            response.Close();

            return result;
        }

        public static string GetTranslateForAmount(string textToTranslate, string langTo)
        {
            textToTranslate = textToTranslate.Replace("Dollars", "Rubles").Replace("Cents", "Kopeek");

            return GetTranslateWebRequest(textToTranslate, langTo).Replace("без копеек", "ноль копеек");
        }

            private static string RegexExecute(string str, string reg, int matchIndex = 0, int subMatchIndex = 1)
        {
            Regex rx = new Regex(reg, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (rx.IsMatch(str))
            {
                return rx.Matches(str)[matchIndex].Groups[subMatchIndex].Value;
            }
            else
            {
                return rx.Matches(str)[matchIndex].Groups[subMatchIndex].Value;
            }
        }
    }
}
