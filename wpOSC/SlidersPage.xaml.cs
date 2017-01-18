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
    public partial class SlidersPage : PhoneApplicationPage
    {
        public SlidersPage()
        {
            InitializeComponent();
        }
        private void br_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 1;
            int val = (int)e.NewValue;
            Client.CLIENT.Slider( name, val);

        }
        private void bg_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 2;
            int val = (int)e.NewValue;
            Client.CLIENT.Slider( name, val);
        }

        private void bb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 3;
            int val = (int)e.NewValue;
            Client.CLIENT.Slider(name, val);

        }

        private void r_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 4;
            int val = (int)e.NewValue;
            Client.CLIENT.Slider(name, val);

        }

        private void g_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 5;
            int val = (int)e.NewValue;
            Client.CLIENT.Slider(name, val);

        }

        private void b_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int name = 6;
            int val = (int)e.NewValue;
            Client.CLIENT.Slider(name, val);

        }

 
    }
}