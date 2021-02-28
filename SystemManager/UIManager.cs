using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using Windows.Media.Core;

namespace PCClubApp
{
    class UIManager
    {

        public static Action LogOutAction;

        public static void WriteInputStream(string name, string data)
        {
            
        }

        public static async System.Threading.Tasks.Task ShoeNeedAdminAsync(string messageText = "")
        {
            var dialog = new MessageDialog("Необходимо обратиться к Администратору:\n" + messageText);
            _ = await dialog.ShowAsync();
        }

        public static ImageBrush GetImageFromAsset(string name)
        {
            string file_path = String.Format("ms-appx:///Assets/{0}.png", name);
            BitmapImage im_b = new BitmapImage(new Uri(file_path));
            ImageBrush im_br = new ImageBrush();
            im_br.ImageSource = im_b;
            return im_br;
        }

        

        public static ImageSource BitmapImageFromUrl(string image_url)
        {
            ClanREST reqM = new ClanREST();
            return  reqM.GetPicture(image_url);
        }

        public static MediaSource GetMediaSourceByUrl(string media_url, string content_type)
        {
            ClanREST reqM = new ClanREST();
            return reqM.GetVideoSource(media_url, content_type);
        }

        public static BitmapImage Base64StringToBitmap(string imageData)
        {
            /*byte[] byteBuffer = Encoding.UTF8.GetBytes(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;

            BitmapImage bitmapImage = new BitmapImage();
            
            bitmapImage.SetSource(memoryStream.AsRandomAccessStream());

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;*/
            BitmapImage bitmapImage = new BitmapImage();
            return bitmapImage;
        }

    }
}
