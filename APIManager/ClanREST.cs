using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;


namespace PCClubApp

{
    public class DataObject
    {
        public string Name { get; set; }
    }

    public class ClanREST
    {

        private const string main_URL = "http://192.168.0.62:8123";
        private string urlParameters = "?api_key=123";
        private const string DATA = @"{""login"":""master"", ""password"": ""master""}";
        private static string user_token;
        public IRequestDelegateLogin req_delegate_login;
        public IRequestDelegateShop req_delegate_shop;
        public IRequestDelegateProfile req_delegate_profile;


        public ClanREST(IRequestDelegateLogin iDelegate)
        {
            this.req_delegate_login = iDelegate;
        }

        public ClanREST(IRequestDelegateShop iDelegate)
        {
            this.req_delegate_shop = iDelegate;
        }

        public ClanREST(IRequestDelegateProfile iDelegate)
        {
            this.req_delegate_profile = iDelegate;
        }

        private HttpWebRequest CreateRequest(string request_method, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(main_URL + request_method);
            request.Method = method;
            request.ContentType = "application/json";
            
            if (user_token != null)
            {
                Trace.WriteLine("user token " + user_token);
                request.Headers["Authorization"] = "Bearer_" + user_token;
            }
            return request;
        }


        private void RUN_request(string req_method, Action<string> succes_method, string method = "GET", string post_data = "")
        {
            HttpWebRequest request = CreateRequest(req_method, method);
            request.ContentLength = post_data.Length;
            if (post_data != "")
            {
                using (Stream webStream = request.GetRequestStream())
                using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                {
                    requestWriter.Write(post_data);
                }
            }
                
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    succes_method(response);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("--------ERROR---------");
                Trace.WriteLine(e.Message);
            }
        }



        public void Login(string login, string password)
        {
            //TODO: - reform post string
            string post_data = "{\"login\":\"{login}\", \"password\": \"{password}\"}".Replace("{login}", login).Replace("{password}", password);
            this.RUN_request("/login",
                (data) => {
                        dynamic result = JObject.Parse(data);
                        string token = result.token;
                        user_token = result.token;
                        req_delegate_login.LoginResult(true);
                    },
                method: "POST",
                post_data: post_data
                );
        }

        public void ShopList()
        {
            this.RUN_request(
                "/desktop/product/list",
                (data) =>
                {
                    string pData = PsevdaShop.ShopItems();
                    string useData = pData;
                    string ydata = "{\"result\":" + useData + "}";
                    dynamic jOb = JObject.Parse(ydata);
                    JArray resultJsonArray = jOb.result;
                    
                    List<ShopUnit> shopList = new List<ShopUnit>();
                    foreach (var i in resultJsonArray)
                    {
                        shopList.Add(new ShopUnit(i));
                    }
                    req_delegate_shop.ShopListResult(shopList);
                    Trace.WriteLine(shopList);
                }
            );
        }

        public void ProfileData()
        {
            this.RUN_request(
                "/profile",
                (data) =>
                {
                    Trace.WriteLine(data);
                    string ydata = "{\"result\":" + data + "}";
                    dynamic jOb = JObject.Parse(ydata);
                    /*Trace.WriteLine(jOb.id);
                    Trace.WriteLine(jOb.Name);*/
                    req_delegate_profile.ProfileResult(new ProfileData(jOb.result));
                }
            );
        }

    }

    

}
