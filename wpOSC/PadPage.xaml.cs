using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace wpOSC
{
    public partial class PadPage : PhoneApplicationPage
    {
        public PadPage()
        {
            InitializeComponent();
            

        }


        private void ContentPanel_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            Client.CLIENT.MousePosition((int)e.ManipulationOrigin.X, (int)e.ManipulationOrigin.Y, 1);
        }

        //private void ContentPanel_DoubleTap(object sender, GestureEventArgs e)
        //{
        //    Client.CLIENT.sendMessage("/fade", 1);
        //    NavigationService.Navigate(new Uri("/ReymentaPage.xaml", UriKind.Relative));

        //}

        private void ContentPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            float x = ((float)e.ManipulationOrigin.X / 800);
            float y = ((float)e.ManipulationOrigin.Y / 480);
            Client.CLIENT.ActiveClipPositionX(x);
            Client.CLIENT.ActiveClipPositionY(y);
            status.Text = "x: " + e.ManipulationOrigin.X + " y: " + e.ManipulationOrigin.Y + " tx: " + x + " ty: " + y;
        }

       
    }
}