using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorkShop10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tile : Page
    {
       

        public Tile()
        {
            this.InitializeComponent();
           
        }
      

        private void btnShowToast_Click(object sender, RoutedEventArgs e)
        {
            // build toast
            var template = ToastTemplateType.ToastText01;
            var xml = ToastNotificationManager.GetTemplateContent(template);
            xml.DocumentElement.SetAttribute("launch", "Args");
            // set value
            var text = xml.CreateTextNode(txtText.Text);
            var elements = xml.GetElementsByTagName("text");
            elements[0].AppendChild(text);
            // show toast
            var toast = new ToastNotification(xml);
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(toast);
        }

        private async void btnShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Information", "Warning")
            {
                Options = MessageDialogOptions.AcceptUserInputAfterDelay,
                CancelCommandIndex = 1
            };

            messageDialog.Commands.Add(new UICommand() { Id = 0, Label = "Accept" });

            messageDialog.Commands.Add(new UICommand() { Id = 1, Label = "Cancel" });
            var result = await messageDialog.ShowAsync();
            var id = result.Id;
        }

        private void btnShareText_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareTextHandler);
        }
        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Share Text Example";
            request.Data.Properties.Description = "A demonstration that shows how to share text.";
            request.Data.SetText("Hello World!");
        }
    }
}
