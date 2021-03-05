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
using System.Diagnostics;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PCClubApp.View
{
    public sealed partial class CompSettingDialog : ContentDialog, IRequestDelegateSuccessResult
    {
        private ClanREST req;
        public CompSettingDialog()
        {
            this.InitializeComponent();
            macAddressBox.Text = SystemController.GetMacAddress();
            IsSecondaryButtonEnabled = false;
            macAddressBox.IsEnabled = false;
            ClubBox.IsEnabled = false;
            req = new ClanREST(this);
            req.GetClubList((result) =>
            {
                ClubBox.IsEnabled = true;
                activeRing.IsActive = false;
                foreach (var i in result)
                {
                    ClubBox.Items.Add(i);
                }
            });
        }

        public void ErrorResult(string message)
        {
            
        }

        public void SuccessResult()
        {
            
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
           if (ClubBox.SelectedIndex >= 0) req.RegistrationComp(macAddressBox.Text, int.Parse(numberBox.Text), (ClubBox.SelectedItem as ClubUnit).Id);
        }

        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            Func<string, bool> ActCBox = (string textBox) => {
                return textBox != "";
            };

            TextBox currentTextBox = sender as TextBox;
           
            foreach (char i in currentTextBox.Text)
            {
                if (!Char.IsNumber(i))
                {
                    currentTextBox.Text = currentTextBox.Text.Replace(i.ToString(), "");
                }
            }

            IsSecondaryButtonEnabled = ActCBox(macAddressBox.Text) && ActCBox(numberBox.Text) && ClubBox.SelectedIndex >= 0;
        }

        private void ClubBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsSecondaryButtonEnabled = numberBox.Text != "" && ClubBox.SelectedIndex >= 0;
        }
    }
}
