using System;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SimpleReportSample.Extensions
{
    public class TranslateText
    {
        /// <summary>
        /// Lock this object to mark part of code for single thread execution
        /// </summary>
        public static object SyncObject = new object();

        /// <summary>
        /// URL для API
        /// </summary>
        public static string urlApi = @"https://translate.api.cloud.yandex.net/translate/v2/translate";

        /// <summary>
        /// Яндекс IAM_TOKEN
        /// </summary>
        public static string IAM_TOKEN { get; set; }

        /// <summary>
        /// Яндекс OAuth_TOKEN
        /// </summary>
        public static string OAuth_TOKEN { get; set; }

        /// <summary>
        /// Значение folder_id
        /// </summary>
        public static string FolderID { get; set; }

        /// <summary>
        /// Переводим текст
        /// </summary>
        /// <param name="textToTranslate"></param>
        /// <param name="langTo"></param>
        /// <returns></returns>
        public static List<string> GetTranslate(string[] textToTranslate, string langTo)
        {
            List<string> result = new List<string>();
            var json = JsonConvert.SerializeObject(new
            { 
                folder_id = FolderID,
                texts = textToTranslate,
                targetLanguageCode = langTo
            });

            using (Stream responseStream = GetResponce(json).GetResponseStream())
            {
                var responceInString = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
                var convertedResonce = JsonConvert.DeserializeObject<Result>(responceInString);

                foreach (var item in convertedResonce.translations)
                {
                    result.Add(item.text);
                }
            }

            return result;
        }

        public static string GetTranslateForAmount(string textToTranslate, string langTo)
        {
            string result = string.Empty;

            textToTranslate = textToTranslate.Replace("Dollars", "Rubles").Replace("Cents", "Kopeek");

            var json = JsonConvert.SerializeObject(new
            {
                folder_id = FolderID,
                texts = textToTranslate,
                targetLanguageCode = langTo
            });

            using (Stream responseStream = GetResponce(json).GetResponseStream())
            {
                var responceInString = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
                var convertedResonce = JsonConvert.DeserializeObject<Result>(responceInString);
                result = convertedResonce.translations[0].text;
            }

            return result.Replace("Ни Копейки", "Ноль Копеек");
        }

        public static HttpWebResponse GetResponce(string json)
        {
            var request = (HttpWebRequest)WebRequest.Create(urlApi);

            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Bearer " + IAM_TOKEN;

            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            // Делаем запрос и получаем ответ сервера
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response;
        }

        private class Result
        { 
            public Translation[] translations { get; set; }
        }

        /// <summary>
        /// JSON ответ из примера или из конвертера https://json2csharp.com
        /// </summary>
        private class Translation
        {
            public string detectedLanguageCode { get; set; }
            public string text { get; set; }
        }


        /// <summary>
        /// Получение IAM_Token из OAuth_TOKEN
        /// </summary>
        /// <param name="OAuth_TOKEN"></param>
        /// <returns></returns>
        public static string GetIAM_Token(string OAuth_TOKEN)
        {
            string IAM_Token = string.Empty;

            /*
                           Нужно выполнить команды через PowerShell
              
                           $yandexPassportOauthToken = "<OAuth-Token>"
                           $Body = @{ yandexPassportOauthToken = "$yandexPassportOauthToken" } | ConvertTo-Json -Compress
                           Invoke-RestMethod -Method 'POST' -Uri 'https://iam.api.cloud.yandex.net/iam/v1/tokens' -Body $Body -ContentType 'Application/json' | Select-Object -ExpandProperty iamToken
                                         
                    */

            string[] arrCommands =
            {
                string.Format("$yandexPassportOauthToken = \"{0}\"", OAuth_TOKEN),
                "$Body = @{ yandexPassportOauthToken = \"$yandexPassportOauthToken\" } | ConvertTo-Json -Compress",
                "Invoke-RestMethod -Method 'POST' -Uri 'https://iam.api.cloud.yandex.net/iam/v1/tokens' -Body $Body -ContentType 'Application/json' | Select-Object -ExpandProperty iamToken"
            };

            Runspace runspace = RunspaceFactory.CreateRunspace();//Формируем пространство повершел для выполнения команд
            runspace.Open(); // открытие процесса
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(arrCommands[0]); //добавление 1 строки с командой
            pipeline.Commands.AddScript(arrCommands[1]); //добавление 2 строки с командой
            pipeline.Commands.AddScript(arrCommands[2]); //добавление 3 строки с командой
            pipeline.Commands.Add("Out-String"); // эта команда форматирует вывод. Без нее возвращаются реальные объекты.

            Collection<PSObject> results = pipeline.Invoke();// Выполняем все команды
            runspace.Close(); // закрытие процесса

            // Проверка статуса ответа. Преобразование в строку.
            IAM_Token = results[0].BaseObject.ToString().Trim();

            return IAM_Token;
        }

    }
}
