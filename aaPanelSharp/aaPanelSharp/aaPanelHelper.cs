using System;
using System.Collections.Generic;
using System.Text;
using aaPanelSharp.ResponseModels;
using Logging.Net;
using Newtonsoft.Json;
using RestSharp;

namespace aaPanelSharp
{
    internal class aaPanelHelper
    {
        public static RestRequest AddAuthKeys(RestRequest request, string apiKey)
        {
            DateTime now = DateTime.Now;
            long unixTime = ((DateTimeOffset)now).ToUnixTimeSeconds();

            string token = GetMd5(unixTime + GetMd5(apiKey));

            request.AddHeader("User-Agent", "aaPanelApi");

            request.AddParameter("request_token", token);
            request.AddParameter("request_time", unixTime);

            return request;
        }

        public static string GetMd5(string value)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(value);
            byte[] hashedBytes = System.Security.Cryptography.MD5.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return hashedString;
        }

        public static T Post<T>(string url, Dictionary<string, string> parameters, string apiKey)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url);

            request = AddAuthKeys(request, apiKey);

            foreach(string key in parameters.Keys)
            {
                request.AddParameter(key, parameters[key]);
            }

            var response = client.Post(request);

            if(response.IsSuccessful)
            {
                //Logger.Debug(response.Content);
                return JsonConvert.DeserializeObject<T>(response.Content, new _CpuConverter());
            }
            else
            {
                throw new Exception("aaPanel POST request failed: " + response.ErrorMessage);
            }
        }

        public static string PostRaw(string url, Dictionary<string, string> parameters, string apiKey)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url);

            request = AddAuthKeys(request, apiKey);

            foreach (string key in parameters.Keys)
            {
                request.AddParameter(key, parameters[key]);
            }

            var response = client.Post(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception("aaPanel POST request failed: " + response.ErrorMessage);
            }
        }
    }
}
