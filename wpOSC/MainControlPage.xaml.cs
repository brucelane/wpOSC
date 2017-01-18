using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text;
using System.Windows.Input;
using Bespoke.Common.Osc;
using System.Diagnostics;

namespace wpOSC
{
    

    public partial class MainControlPage : PhoneApplicationPage
    {
       
        public MainControlPage()
        {
            InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //controls.Parse(json);
        }

        private void liste_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            switch (liste.SelectedIndex)
            {
                case 0:
                    NavigationService.Navigate(new Uri("/PadPage.xaml", UriKind.Relative));
                    break;
                case 1:
                    NavigationService.Navigate(new Uri("/AccelerometerPage.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/GyropscopePage.xaml", UriKind.Relative));
                    break;
                case 3:
                    NavigationService.Navigate(new Uri("/CompassPage.xaml", UriKind.Relative));
                    break;
                case 4:
                    NavigationService.Navigate(new Uri("/LayersPage.xaml", UriKind.Relative));
                    break;
                case 5:
                    NavigationService.Navigate(new Uri("/CompositionSlidersPage.xaml", UriKind.Relative));
                    break;
                case 6:
                    NavigationService.Navigate(new Uri("/ActiveClipSlidersPage.xaml", UriKind.Relative));
                    break;
                case 7:
                    NavigationService.Navigate(new Uri("/SlidersPage.xaml", UriKind.Relative));
                    break;
            }
        }
        
    }
}