using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils
{
    public class RestRequestMaker
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RelativeApiUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        internal T Get<T>(string RelativeApiUrl,  Dictionary<string, string> Headers = null) where T : new()
            {
            T Result = new T();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RestClient Client = new RestClient($"{RelativeApiUrl}");
                RestRequest Request = new RestRequest(Method.GET);
                Request.AddHeader("Content-Type", "Application/json");
                if(Headers != null)
                {
                    foreach (var item in Headers)
                    {
                        Request.AddHeader(item.Key, item.Value);
                    }
                }
                IRestResponse<T> Response = Client.Execute<T>(Request);
                //get the response in the .Data
                Result = Response.Data;


            }
            catch (Exception ex)
            {

                throw ex;
                //return Result;
            }

            return Result;

            }

        internal T Post<T>(string RelativeApiUrl, object RequestObject, Dictionary<string, string> Headers = null) where T : new()
        {
            T Result = new T();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RestClient Client = new RestClient($"{RelativeApiUrl}");
                RestRequest Request = new RestRequest(Method.POST);
                Request.AddHeader("Content-Type", "Application/json");
                if (Headers != null)
                {
                    foreach (var item in Headers)
                    {
                        Request.AddHeader(item.Key, item.Value);
                    }
                }
                Request.AddJsonBody(RequestObject); // this will serialize whatever the request object is before request is executed.
                IRestResponse<T> Response = Client.Execute<T>(Request);
                //get the response in the .Data
                Result = Response.Data;


            }
            catch (Exception ex)
            {

                throw ex;
                //return Result;
            }

            return Result;

        }
    }
}
