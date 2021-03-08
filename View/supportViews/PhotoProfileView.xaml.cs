using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace PCClubApp.View.supportViews
{
    public sealed partial class PhotoProfileView : UserControl
    {
        public Action<ImageSource> picAction;

        public PhotoProfileView()
        {
            this.InitializeComponent();
        }

        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            picAction((sender as Image).Source);
            Visibility = Visibility.Collapsed;
        }
    }
}
