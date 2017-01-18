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

namespace wpOSC
{
    

    public partial class TouchDrawPage : PhoneApplicationPage
    {
        private Point oldPosition;
        private String oldText = "";

        public TouchDrawPage()
        {
            InitializeComponent();
        }
        private void keyboardButton_Click(object sender, RoutedEventArgs e)
        {
            keyboardInputBox.Text = "";
            keyboardInputBox.Focus();
        }

        private void mousePad_MouseEnter(object sender, MouseEventArgs e)
        {
            this.oldPosition = e.GetPosition(mousePad);
        }

        private void mousePad_MouseMove(object sender, MouseEventArgs e)
        {
            Point newPosition = e.GetPosition(this.mousePad);

            float dX = (float)(newPosition.X - oldPosition.X);
            float dY = (float)(newPosition.Y - oldPosition.Y);
            Client.CLIENT.MouseMoved(dX, dY);

            this.oldPosition = newPosition;
        }

        private void mousePad_Tap(object sender, GestureEventArgs e)
        {
            Client.CLIENT.LeftButtonPressed();
            Client.CLIENT.LeftButtonReleased();
        }

        private void rightButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Client.CLIENT.RightButtonPressed();
        }

        private void rightButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Client.CLIENT.RightButtonReleased();
        }

        private void leftButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Client.CLIENT.LeftButtonPressed();

        }

        private void leftButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Client.CLIENT.LeftButtonReleased();
        }

        private void keyboardInputBox_KeyUp(object sender, KeyEventArgs e)
        {
            Client.CLIENT.KeyReleased(e.Key);
        }

        private void keyboardInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            Client.CLIENT.KeyPressed(e.Key);
        }

        private void keyboardInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            String newText = keyboardInputBox.Text;
            if (newText.Length > oldText.Length)
            {
                Client.CLIENT.KeysTyped(newText.Substring(oldText.Length));
                
            }
            oldText = newText;
        }
        /// <summary>
        /// Log text to the lbLog ListBox.
        /// </summary>
        /// <param name="message">The message to write to the lbLog ListBox</param>
        /// <param name="isOutgoing">True if the message is an outgoing (client to server) message; otherwise, False</param>
        /// <remarks>We differentiate between a message from the client and server 
        /// by prepending each line  with ">>" and "<<" respectively.</remarks>
        private void Log(string message, bool isOutgoing)
        {
            if (string.IsNullOrWhiteSpace(message.Trim('\0')))
                return;

            // Always make sure to do this on the UI thread.
            Deployment.Current.Dispatcher.BeginInvoke(
            () =>
            {
                string direction = (isOutgoing) ? ">> " : "<< ";
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                message = timestamp + direction + message;
                lbLog.Items.Add(message);

                // Make sure that the item we added is visible to the user.
                lbLog.ScrollIntoView(message);
            });

        }
    }
}