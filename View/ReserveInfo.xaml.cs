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
using Windows.UI.Popups;

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace PCClubApp.View
{
    public sealed partial class ReserveInfo : UserControl, IRequestDelegateSuccessResult
    {

        private ClanREST req;

        public ReserveInfo()
        {
            this.InitializeComponent();
            req = new ClanREST(this);
            DateStartField.MinDate = DateTime.Now;
        }

        private string CondDataPicker(DateTime date, string time)
        {
            return date.ToString("yyyy-MM-dd") + "T" + time + ".000Z";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                string start = CondDataPicker(DateStartField.Date.Value.DateTime, TimeStartPicker.Time.ToString());
                string end = CondDataPicker(DateEndField.Date.Value.DateTime, TimeEndPicker.Time.ToString());
                string compId = ProfileManager.ComputersList[CompListBox.SelectedIndex].Id.ToString();

                req.ReservationPlace(start, end, compId);
            }
            catch {
                ShowPopOver("Данные не заполенны");
            }
            
        }

        public void OnActive()
        {
            CompListBox.Items.Clear();
            if (ProfileManager.ComputersList == null) return;
            foreach(var i in ProfileManager.ComputersList)
            {
                CompListBox.Items.Add(i);
            }
            CompListBox.SelectedIndex = 0;
        }

        void IRequestDelegateSuccessResult.SuccessResult()
        {
            ShowPopOver("Успешно");
        }

        async System.Threading.Tasks.Task ShowAsPopAsync(MessageDialog dialog)
        {
            var result = await dialog.ShowAsync();
        }

        void IRequestDelegateSuccessResult.ErrorResult(string message)
        {
            ShowPopOver("Произошла ошибка при бронировании");
        }

        private void ShowPopOver(string messagetext)
        {
            var dialog = new MessageDialog(messagetext, "Бронирование");
            dialog.Commands.Add(new UICommand("Ok", delegate (IUICommand command)
            {

            }));

            _ = ShowAsPopAsync(dialog);
        }


    }
}
