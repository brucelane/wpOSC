using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using System.Windows.Input;

namespace wpOSC
{
    public partial class GyropscopePage : PhoneApplicationPage
    {
        private Gyroscope gyroscope;
        private float minX, maxX, minY, maxY, minZ, maxZ;
        public GyropscopePage()
        {
            InitializeComponent();
            if (Gyroscope.IsSupported)
            {
                if (gyroscope == null)
                {
                    minX = maxX = minY = maxY = minZ = maxZ = 0.0f;
                    gyroscope = new Gyroscope();
                    gyroscope.TimeBetweenUpdates = TimeSpan.FromMilliseconds(100);
                    gyroscope.CurrentValueChanged += gyroscope_CurrentValueChanged;
                    try
                    {
                        gyroscope.Start();
                    }
                    catch (InvalidOperationException ex)
                    {
                        status.Text = ex.Message;
                    }
                }
            }
            else
            {
                status.Text = "Gyroscope not supported";
            }
        }
        void gyroscope_CurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            Vector3 rate = e.SensorReading.RotationRate;
            
                Dispatcher.BeginInvoke(() =>
                {
                    if (rate.X < minX) minX = rate.X;
                    if (rate.Y < minY) minY = rate.Y;
                    if (rate.Z < minZ) minZ = rate.Z;
                    if (rate.X > maxX) maxX = rate.X;
                    if (rate.Y > maxY) maxY = rate.Y;
                    if (rate.Z > maxZ) maxZ = rate.Z;
                    x.Text = "X: " + rate.X.ToString("0.00") + " min: " + minX.ToString("0.00") + " max: " + maxX.ToString("0.00");
                    y.Text = "Y: " + rate.Y.ToString("0.00") + " min: " + minY.ToString("0.00") + " max: " + maxY.ToString("0.00");
                    z.Text = "Z: " + rate.Z.ToString("0.00") + " min: " + minZ.ToString("0.00") + " max: " + maxZ.ToString("0.00");
                    if (Activate.IsChecked == true)
                    {
                        Client.CLIENT.GyroscopeX(rate.X + 1);
                        Client.CLIENT.GyroscopeY(rate.Y + 1);
                        Client.CLIENT.GyroscopeZ(rate.Z + 1);
                    }
                });
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Activate.IsChecked = false;
            Client.CLIENT.GyroscopeX(0.5f);
            Client.CLIENT.GyroscopeY(0.5f);
            Client.CLIENT.GyroscopeZ(0.5f);

        }
    }
}