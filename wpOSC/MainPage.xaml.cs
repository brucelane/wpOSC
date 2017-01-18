using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Net.Sockets;
using System.Text;
using System.IO.IsolatedStorage;
using Bespoke.Common.Osc;
using Windows.Networking.Connectivity;
using Microsoft.Phone.Net.NetworkInformation;
using wpOSC.Utils;

namespace wpOSC
{
    public partial class MainPage : PhoneApplicationPage
    {
 
        // Constructor
        public MainPage()
        {
            InitializeComponent();

			//Shows the rate reminder message, according to the settings of the RateReminder.
            //(App.Current as App).rateReminder.Notify();
            RestoreSettings();
            
            //MyIPAddress finder = new MyIPAddress();
            //finder.Find((address) =>
            //{
            //    Dispatcher.BeginInvoke(() =>
            //    {
            //        localIpAddress.Text = address == null ? "Unknown" : address.ToString();
            //    });
            //});
        }

		/// <summary>
        /// Navigates to about page.
        /// </summary>
        private void GoToAbout(object sender, GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.RelativeOrAbsolute));
        }
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            if (connectLabel.Text.Length > 0)
            {
                OscMessage.LittleEndianByteOrder = false;

                try
                {
                    SaveSettings();
                    Client.CLIENT = new Client(serverAddressField.Text, int.Parse(portField.Text));

                    NavigationService.Navigate(new Uri("/MainControlPage.xaml", UriKind.Relative));
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Enter a a valid IP address (e.g. 192.168.1.2) and port (e.g. 7000");
                }
            }
        }
        private void SaveSettings()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("server.ip"))
            {
                settings.Add("server.ip", serverAddressField.Text);
            }
            else
            {
                settings["server.ip"] = serverAddressField.Text;
            }
            if (!settings.Contains("server.port"))
            {
                settings.Add("server.port", portField.Text);
            }
            else
            {
                settings["server.port"] = portField.Text;
            }
        }

        private void RestoreSettings()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string serverIp;
            string port;
            if (!settings.TryGetValue<string>("server.ip", out serverIp))
            {
                serverIp = "192.168.0.117";
            }
            if (!settings.TryGetValue<string>("server.port", out port))
            {
                port = "7000";
            }
            serverAddressField.Text = serverIp;
            portField.Text = port;
        }

        private void serverAddressField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConnectToServer();
            }
        }

        private void portField_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
        

            
        

    }
}
