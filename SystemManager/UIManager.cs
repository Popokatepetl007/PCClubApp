using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PCClubApp
{
    class UIManager
    {
        public static ImageBrush GetImageFromAsset(string name)
        {
            string file_path = String.Format("ms-appx:///Assets/{0}.png", name);
            BitmapImage im_b = new BitmapImage(new Uri(file_path));
            ImageBrush im_br = new ImageBrush();
            im_br.ImageSource = im_b;
            return im_br;
        }
    }
}
