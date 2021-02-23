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
using System.Drawing;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;
using System.Runtime.InteropServices.WindowsRuntime;

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
        //private const string main_URL = "http://10.0.0.13:8123";
        private const string DATA = @"{""login"":""master"", ""password"": ""master""}";
        private static string user_token;
        public IRequestDelegateLogin req_delegate_login;
        public IRequestDelegateShop req_delegate_shop;
        public IRequestDelegateProfile req_delegate_profile;
        public IRequestDelegateSuccessResult req_delegate_success;
        public IRequestDelegateContentList req_delegate_contentList;
        public IRequestChat req_delegate_chat;
        public IRequestNews req_delegate_news;


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

        public ClanREST(IRequestNews iDelegate)
        {
            this.req_delegate_news = iDelegate;
        }
        public ClanREST()
        {

        }

        private JArray ParseList(string inputData)
        {
            string ydata = "{\"result\":" + inputData + "}";
            dynamic jOb = JObject.Parse(ydata);
            JArray resultJsonArray = jOb.result;
            
            return resultJsonArray;
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

        private Stream RUN_Stream(string req_method)
        {
            HttpWebRequest request = CreateRequest(req_method, "GET");

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                {
                    //Image img = Image.FromStream(responseStream);
                    return webStream;
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
                return null;
            }
        }


        private async System.Threading.Tasks.Task RUN_requestAsync(string req_method, Action<string> succes_method, string method = "GET", string post_data = "")
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
                WebResponse webResponse = await request.GetResponseAsync();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    succes_method(response);
                }
            }
            catch (WebException e)
            {
                Trace.WriteLine("--------ERROR--------- " + req_method);
                
                using (Stream webStream = e.Response.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    
                    try
                    {
                        dynamic result = JObject.Parse(response);
                        if ((int)result.errorCode == 3)
                        {
                            UIManager.LogOutAction();
                            _ = UIManager.ShoeNeedAdminAsync((string)result.errorMessage);
                        }
                    }
                    catch (Exception)
                    { }
                }
                if (req_delegate_success != null)
                {
                    req_delegate_success.ErrorResult("bad");
                }
            }
        }


        public void Login(string login, string password)
        {   
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { login = login, password = password });
            _ = this.RUN_requestAsync("/login",
                (data) => {
                        dynamic result = JObject.Parse(data);
                        string token = result.token;
                        user_token = result.token;
                        int user_id = result.id;
                        string user_role = result.role;
                        req_delegate_login.LoginResult(true, user_id, ProfileManager.GetUserRoleFromString(user_role));
                    },
                method: "POST",
                post_data: post_data
                );
        }

        public void LogOut()
        {
            user_token = null;
            WSManager.CloseWS();
        }

        public void ReservationPlace(string start, string end, string compId)
        {
  
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { start = start, end = end, computerId = compId });
            Trace.WriteLine(post_data);
            _ = this.RUN_requestAsync("/desktop/reservation",
                (data) => {
                    req_delegate_success.SuccessResult();
                },
                method: "POST",
                post_data: post_data
                );
        }
        

        public void ShopList()
        {
            _ = this.RUN_requestAsync(
                "/desktop/product/list",
                (data) =>
                {                    
                    List<ShopUnit> shopList = new List<ShopUnit>();
                    foreach (var i in this.ParseList(data))
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
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { count = 1, productId = id});
            _ = this.RUN_requestAsync(
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
            _ = this.RUN_requestAsync(
                "/profile",
                (data) =>
                {
                    string ydata = "{\"result\":" + data + "}";
                    dynamic jOb = JObject.Parse(ydata);
                    /*Trace.WriteLine(jOb.id);
                    Trace.WriteLine(jOb.Name);*/
                    ProfileData pd = new ProfileData(jOb.result);
                    ProfileManager.profile_data = pd;
                    if (req_delegate_profile != null)
                    {
                        req_delegate_profile.ProfileResult(pd);
                    }
                }
            );
        }

        public ImageSource GetPicture(string pathUrl)
        {

            /*Stream sr = RUN_Stream(pathUrl);*/
            HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(
               main_URL + pathUrl);

            if (ClanREST.user_token != null)
            {
                lxRequest.Headers["Authorization"] = "Bearer_" + ClanREST.user_token;
            }

            String lsResponse = string.Empty;
            using (HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse())
            {
                using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                {
                    
                    Byte[] lnByte = reader.ReadBytes(0);
                    MemoryStream lxMS = new MemoryStream();
                    Byte[] lnBuffer = reader.ReadBytes(1024);
                    while (lnBuffer.Length > 0)
                    {
                        lxMS.Write(lnBuffer, 0, lnBuffer.Length);
                        lnBuffer = reader.ReadBytes(1024);
                    }

                    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                    {
                        stream.AsStreamForWrite().Write(lxMS.ToArray(), 0, lxMS.ToArray().Length);
                        Trace.WriteLine(stream.Size);
                        stream.Seek(0);
                        BitmapImage image = new BitmapImage();
                        image.SetSource(stream);
                        Trace.WriteLine(image.PixelWidth);
                        return image;
                    }
                }
            }
        }

        public void GetContent()
        {
            _ = this.RUN_requestAsync(String.Format("/desktop/content/list?computerId={0}", ProfileManager.compId),
                (data) =>
                {
                    Trace.WriteLine("----Content-----");
                    Trace.WriteLine(data);

                    List<GameUnit> contentList = new List<GameUnit>();
                    foreach (dynamic i in this.ParseList(data))
                    {
                        contentList.Add(new GameUnit(i));
                    }
                    req_delegate_contentList.ContentResult(contentList);
                });
        }

        public void GetNews()
        {
            _ = this.RUN_requestAsync(
                "/desktop/news/list",
                (data) =>
                {
                    Trace.WriteLine("--NEWS--");
                    Trace.WriteLine(data);

                    List<NewsUnit> newsList = new List<NewsUnit>();
                    foreach (dynamic i in this.ParseList(data))
                    {
                        newsList.Add(new NewsUnit(i));
                    }
                    req_delegate_news.NewsResult(newsList);
                }
            );
        }

        public void RegistrationComp(string mac, int number, int clubId)
        {
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { macAddress = mac, clubId = clubId, number = number });
            Trace.WriteLine(post_data);
            _ = this.RUN_requestAsync(
                "/computer",
                (data) =>
                {
                    dynamic jOb = JObject.Parse(data);
                    StorageManager sm = new StorageManager();
                    int compId = jOb.id;
                    int clubIdIn = jOb.club.id;
                    int compNumber = jOb.number;
                    sm.SetClubIdValue(clubIdIn);
                    sm.SetCompIdValue(compId);
                    sm.SetCompNumberValue(compNumber);

                },
                method: "POST",
                post_data: post_data
                );
        }

        public void PostChatMessage(string content)
        {
            string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(new { clubId = ProfileManager.clubId, content = content });
            Trace.WriteLine(post_data);

            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.UTF8;
            byte[] unicodeBytes = unicode.GetBytes(post_data);
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);
            _ = this.RUN_requestAsync("/chat/user/send",
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
            _ = this.RUN_requestAsync(String.Format("/chat/message/{0}", id),
                (data) =>
                {
                    dynamic jOb = JObject.Parse(data);
                    Trace.WriteLine("--MESSAGE RESPONCE READY---");
                    req_delegate_chat.MessgateResult(new ChatMessage(jOb));
                });
        }


        public static string UserToken
        { get => "Bearer_" + user_token; }

    }

    public static class WebRequestExtensions
    {
        public static WebResponse GetResponseWithoutException(this WebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            try
            {
                return request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                return e.Response;
            }
        }
    }

}
