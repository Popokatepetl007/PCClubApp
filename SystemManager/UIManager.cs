using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PCClubApp
{
    class UIManager
    {

        public static void WriteInputStream(string name, string data)
        {
            
        }

        public static ImageBrush GetImageFromAsset(string name)
        {
            string file_path = String.Format("ms-appx:///Assets/{0}.png", name);
            BitmapImage im_b = new BitmapImage(new Uri(file_path));
            ImageBrush im_br = new ImageBrush();
            im_br.ImageSource = im_b;
            return im_br;
        }

        public static BitmapImage BitmapImageFromUrl(string image_url)
        {
            /*Trace.WriteLine("------imaData--------");
            ClanREST reqM = new ClanREST();
            string imaData = reqM.GetPicture(image_url);
            Trace.WriteLine(imaData);
            return Base64StringToBitmap(imaData);*/
            return null;
            //return new BitmapImage(new Uri(image_url));
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
