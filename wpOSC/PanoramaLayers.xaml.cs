using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;

namespace wpOSC
{
    public partial class PanoramaLayers : PhoneApplicationPage
    {
        public PanoramaLayers()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadJson();
        }

        async private void LoadJson()
        {
            try
            {
                var res = Application.GetResourceStream(new Uri("Json/general.json", UriKind.Relative));
                var reader = new StreamReader(res.Stream);
                var txt = await reader.ReadToEndAsync();

                controls.Parse(txt);
                controls.OnActionHandler += controls_OnActionHandler;
            }
            catch { }
        }

        void controls_OnActionHandler(string cmd, object value)
        {
            Client.CLIENT.sendMessage(cmd, 1);//Convert.ToInt16(value));
            if (cmd.Contains("connect")) NavigationService.Navigate(new Uri("/ActiveClipPage.xaml", UriKind.Relative));
        }
    }
}