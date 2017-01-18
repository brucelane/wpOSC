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
    public partial class CompassPage : PhoneApplicationPage
    {
        Compass compass;
        double magneticHeading;
        double trueHeading;
        double headingAccuracy;
        Vector3 rawMagnetometerReading;
        bool isDataValid;

        bool calibrating = false;
        private float minX, maxX, minY, maxY, minZ, maxZ;
        public CompassPage()
        {
            InitializeComponent();
            if (!Compass.IsSupported)
            {
                // The device on which the application is running does not support
                // the compass sensor. Alert the user and hide the
                // application bar.
                status.Text = "device does not support compass";
                //ApplicationBar.IsVisible = false;
            }
            else
            {
                minX = maxX = minY = maxY = minZ = maxZ = 0.0f;
                // Instantiate the compass.
                compass = new Compass();

                // Specify the desired time between updates. The sensor accepts
                // intervals in multiples of 20 ms.
                compass.TimeBetweenUpdates = TimeSpan.FromMilliseconds(100);


                compass.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);
                //compass.Calibrate += new EventHandler<CalibrationEventArgs>(compass_Calibrate);
                try
                {
                    
                    compass.Start();
                    
                }
                catch (InvalidOperationException)
                {
                    status.Text = "unable to start compass.";
                }
            }
        }
        void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            // Note that this event handler is called from a background thread
            // and therefore does not have access to the UI thread. To update 
            // the UI from this handler, use Dispatcher.BeginInvoke() as shown.
            // Dispatcher.BeginInvoke(() => { statusTextBlock.Text = "in CurrentValueChanged"; });


            isDataValid = compass.IsDataValid;

            trueHeading = e.SensorReading.TrueHeading;
            magneticHeading = e.SensorReading.MagneticHeading;
            headingAccuracy = Math.Abs(e.SensorReading.HeadingAccuracy);
            rawMagnetometerReading = e.SensorReading.MagnetometerReading;
            Dispatcher.BeginInvoke(() =>
            {
                if (rawMagnetometerReading.X < minX) minX = rawMagnetometerReading.X;
                if (rawMagnetometerReading.Y < minY) minY = rawMagnetometerReading.Y;
                if (rawMagnetometerReading.Z < minZ) minZ = rawMagnetometerReading.Z;
                if (rawMagnetometerReading.X > maxX) maxX = rawMagnetometerReading.X;
                if (rawMagnetometerReading.Y > maxY) maxY = rawMagnetometerReading.Y;
                if (rawMagnetometerReading.Z > maxZ) maxZ = rawMagnetometerReading.Z;
                x.Text = "X: " + rawMagnetometerReading.X.ToString("0.00") + " min: " + minX.ToString("0.00") + " max: " + maxX.ToString("0.00");
                y.Text = "Y: " + rawMagnetometerReading.Y.ToString("0.00") + " min: " + minY.ToString("0.00") + " max: " + maxY.ToString("0.00");
                z.Text = "Z: " + rawMagnetometerReading.Z.ToString("0.00") + " min: " + minZ.ToString("0.00") + " max: " + maxZ.ToString("0.00");
                if (Activate.IsChecked == true)
                {
                    Client.CLIENT.CompassX(rawMagnetometerReading.X + 1);
                    Client.CLIENT.CompassY(rawMagnetometerReading.Y + 1);
                    Client.CLIENT.CompassZ(rawMagnetometerReading.Z + 1);
                }
            });
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Activate.IsChecked = false;
            Client.CLIENT.CompassX(0.5f);
            Client.CLIENT.CompassY(0.5f);
            Client.CLIENT.CompassZ(0.5f);

        }
    }
}