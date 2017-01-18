using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace wpOSC
{
    public partial class ActiveClipSlidersPage : PhoneApplicationPage
    {
        public ActiveClipSlidersPage()
        {
            InitializeComponent();
        }

        private void s1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 1;
            int val = (int)e.NewValue;
            Client.CLIENT.ActiveClipSlider(name, val);
        }

        private void s2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 2;
            int val = (int)e.NewValue;
            Client.CLIENT.ActiveClipSlider(name, val);

        }

        private void s3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 3;
            int val = (int)e.NewValue;
            Client.CLIENT.ActiveClipSlider(name, val);

        }

        private void s4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 4;
            int val = (int)e.NewValue;
            Client.CLIENT.ActiveClipSlider(name, val);

        }

        private void s5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 5;
            int val = (int)e.NewValue;
            Client.CLIENT.ActiveClipSlider(name, val);

        }

        private void s6_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 6;
            int val = (int)e.NewValue;
            Client.CLIENT.ActiveClipSlider(name, val);

        }
    }
}