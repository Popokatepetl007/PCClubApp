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
        private const string main_URL = "http://5.129.77.65:8123";
        //private const string main_URL = "http://192.168.0.62:8123";
        //private const string main_URL = "http://10.0.0.7:8123";
        private const string DATA = @"{""login"":""master"", ""password"": ""master""}";
        private static string user_token;
        public IRequestDelegateLogin req_delegate_login;
        public IRequestDelegateShop req_delegate_shop;
        public IRequestDelegateProfile req_delegate_profile;
        public IRequestDelegateSuccessResult req_delegate_success;
        public IRequestDelegateContentList req_delegate_contentList;
        public IRequestChat req_delegate_chat;


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

        public ClanREST(IRequestDelegateSuccessResult iDelegate)
        {
            this.req_delegate_success = iDelegate;
        }

        public ClanREST(IRequestDelegateContentList iDelegate)
        {
            this.req_delegate_contentList = iDelegate;
        }

        public ClanREST(IRequestChat iDelegate)
        {
            this.req_delegate_chat = iDelegate;
        }
        public ClanREST()
        {

        }

        private HttpWebRequest CreateRequest(string request_method, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(main_URL + request_method);
            request.Method = method;
            request.ContentType = "application/json";
            
            if (ClanREST.user_token != null)
            {
                Trace.WriteLine("user token " + ClanREST.user_token);
                request.Headers["Authorization"] = "Bearer_" + ClanREST.user_token;
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
                if (req_delegate_success != null)
                {
                    req_delegate_success.ErrorResult("bad");
                }
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

        public void ReservationPlace(string start, string end, string compId)
        {
            string post_data = "{\"start\":\"{start}\", \"end\": \"{end}\", \"computerId\": {cId}}".Replace("{start}", start).Replace("{end}", end).Replace("{cId}", "5");
            Trace.WriteLine(post_data);
            this.RUN_request("/desktop/reservation",
                (data) => {
                    req_delegate_success.SuccessResult();
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
                    string useData = data;
                    string ydata = "{\"result\":" + useData + "}";
                    Trace.WriteLine(ydata);
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

        public void BuyProduct(int id)
        {
            string post_data = "{\"count\": 1, \"productId\": {p}}".Replace("{p}", id.ToString());
            this.RUN_request(
                "/desktop/product/buy",
                (data) =>
                {

                },
                method: "POST",
                post_data: post_data
                );
        }

        public void ProfileData()
        {
            //this.GetPicture();
            this.RUN_request(
                "/profile",
                (data) =>
                {
                    string ydata = "{\"result\":" + data + "}";
                    dynamic jOb = JObject.Parse(ydata);
                    /*Trace.WriteLine(jOb.id);
                    Trace.WriteLine(jOb.Name);*/
                    req_delegate_profile.ProfileResult(new ProfileData(jOb.result));
                }
            );
        }

        public string GetPicture(string pathUrl)
        {
            string result = "";
            RUN_request(pathUrl,
                (data) =>
                {
                    Trace.WriteLine(data);
                    result = data;
                }
            );
            return result;
        }

        public void GetContent()
        {
            this.RUN_request("/desktop/content/list?computerId=5",
                (data) =>
                {
                    Trace.WriteLine("----Content-----");
                    Trace.WriteLine(data);
                    string ydata = "{\"result\":" + data + "}";
                    dynamic jOb = JObject.Parse(ydata);
                    JArray resultJsonArray = jOb.result;
                    List<GameUnit> contentList = new List<GameUnit>();
                    foreach (dynamic i in resultJsonArray)
                    {
                        contentList.Add(new GameUnit(i));
                    }
                    req_delegate_contentList.ContentResult(contentList);
                });
        }

        public void RegistrationComp(string mac, int number, int clubId)
        {
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { macAddress = mac, clubId = clubId, number = number });
            Trace.WriteLine(post_data);
            this.RUN_request(
                "/computer",
                (data) =>
                {

                },
                method: "POST",
                post_data: post_data
                );
        }

        public void PostChatMessage(string content)
        {
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { clubId = ProfileManager.clubId, content = content });
            this.RUN_request("/chat/user/send",
                (data) =>
                {
                    Trace.WriteLine("----POST Chat MessageResult-----");
                    Trace.WriteLine(data);
                },
                method: "POST",
                post_data: post_data
                );
        }

        public void GetMessgeByID(int id)
        {
            this.RUN_request(String.Format("/chat/message/{0}", id),
                (data) =>
                {
                    dynamic jOb = JObject.Parse(data);
                    req_delegate_chat.MessgateResult(new ChatMessage(jOb));
                });
        }


        public static string UserToken
        { get => "Bearer_" + user_token; }

    }

    

}
