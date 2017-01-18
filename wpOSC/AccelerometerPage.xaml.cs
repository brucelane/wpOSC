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
    public partial class AccelerometerPage : PhoneApplicationPage
    {
        private Accelerometer accelerometer;
        private float minX, maxX, minY, maxY, minZ, maxZ;
        public AccelerometerPage()
        {
            InitializeComponent();
            if (Accelerometer.IsSupported)
            {
                if (accelerometer == null)
                {
                    minX = maxX = minY = maxY = minZ = maxZ = 0.0f;
                    accelerometer = new Accelerometer();
                    accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(100);
                    accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
                    try
                    {
                        accelerometer.Start();

                    }
                    catch (AccelerometerFailedException ex)
                    {
                        status.Text = ex.Message;

                    }
                }
                else
                {
                    //accelerometer.Stop();
                    //accelerometer = null;

                }
            }
            else
            {
                status.Text = "Accelerometer not supported";
            }
        }
        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            AccelerometerReading reading = e.SensorReading;
            Vector3 acceleration = reading.Acceleration;
            Dispatcher.BeginInvoke(() =>
            {
                if (acceleration.X < minX) minX = acceleration.X;
                if (acceleration.Y < minY) minY = acceleration.Y;
                if (acceleration.Z < minZ) minZ = acceleration.Z;
                if (acceleration.X > maxX) maxX = acceleration.X;
                if (acceleration.Y > maxY) maxY = acceleration.Y;
                if (acceleration.Z > maxZ) maxZ = acceleration.Z;
                x.Text = "X: " + acceleration.X.ToString("0.00") + " min: " + minX.ToString("0.00") + " max: " + maxX.ToString("0.00");
                y.Text = "Y: " + acceleration.Y.ToString("0.00") + " min: " + minY.ToString("0.00") + " max: " + maxY.ToString("0.00");
                z.Text = "Z: " + acceleration.Z.ToString("0.00") + " min: " + minZ.ToString("0.00") + " max: " + maxZ.ToString("0.00");
                if (Activate.IsChecked == true)
                {
                    Client.CLIENT.AccelerometerX(acceleration.X + 1);
                    Client.CLIENT.AccelerometerY(acceleration.Y + 1);
                    Client.CLIENT.AccelerometerZ(acceleration.Z + 1);
                }
            });

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Activate.IsChecked = false;
            Client.CLIENT.AccelerometerX(0.5f);
            Client.CLIENT.AccelerometerY(0.5f);
            Client.CLIENT.AccelerometerZ(0.5f);

        }
    }
}