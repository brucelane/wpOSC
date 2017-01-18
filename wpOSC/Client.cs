using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Bespoke.Common.Osc;
using System.Collections.Generic;

namespace wpOSC
{
    public class Client
    {
        public static Client CLIENT;

        private static IPEndPoint Origin = new IPEndPoint(IPAddress.Loopback, 7000);

        public const int DEFAULT_PORT = 7000;
        
        public Client() : this(IPAddress.Broadcast)
        {
        }

        public Client(string serverIP, int port = DEFAULT_PORT) : this(IPAddress.Parse(serverIP), port)
        {
        }

        public Client(IPAddress ipAddress, int port = DEFAULT_PORT)
        {
            endpoint = new IPEndPoint(ipAddress, port);
        }
        public void sendMessage(string message, int i)
        {
            Msg(message, new object[] { i });
        }
        public void LeftButtonPressed() 
        {
            //Msg("/leftbutton", new object[] { 0 });
            Msg("/layer1/clip1/connect", new object[] { 1 });
        }

        public void LeftButtonReleased()
        {
            //Msg("/leftbutton", new object[] { 1 });
            Msg("/layer1/select", new object[] { 1 });

        }

        public void RightButtonPressed()
        {
            Msg("/layer1/clip2/connect", new object[] { 1 });

        }

        public void RightButtonReleased()
        {
            Msg("/layer2/select", new object[] { 0 });

        }

        public void MouseMoved(float x, float y)
        {
            Msg("/mouse", new object[] { 2, x, y });
        }
        public void MousePosition(int x, int y, int z)
        {
            Msg("/mouse/position", new object[] { x, y, z });
        }
        public void ActiveClipPositionX(float x)
        {
            Msg("/activeclip/video/positionx/values", new object[] { x });
        }
        public void ActiveClipPositionY(float y)
        {
            Msg("/activeclip/video/positiony/values", new object[] { y });
        }
        public void AccelerometerX(float x)
        {
            Msg("/composition/video/rotatex/values", new object[] { x });
        }
        public void AccelerometerY(float y)
        {
            Msg("/composition/video/rotatey/values", new object[] { y });
        }
        public void AccelerometerZ(float z)
        {
            Msg("/composition/video/rotatez/values", new object[] { z });
        }
        public void CompassX(float x)
        {
            Msg("/composition/video/rotatex/values", new object[] { x });
        }
        public void CompassY(float y)
        {
            Msg("/composition/video/rotatey/values", new object[] { y });
        }
        public void CompassZ(float z)
        {
            Msg("/composition/video/rotatez/values", new object[] { z });
        }
        public void GyroscopeX(float x)
        {
            Msg("/composition/video/rotatex/values", new object[] { x });
        }
        public void GyroscopeY(float y)
        {
            Msg("/composition/video/rotatey/values", new object[] { y });
        }
        public void GyroscopeZ(float z)
        {
            Msg("/composition/video/rotatez/values", new object[] { z });
        }
        public void Gyroscope(float x, float y, float z)
        {
            Msg("/gyroscope", new object[] { x, y, z });
        }
        public void Slider(int name, int val)
        {
            string cmd = "/layer" + name.ToString() + "/opacityandvolume";
            float v = val / 127.0f;
            Msg(cmd, new object[] { v });
        }
        public void ActiveClipSlider(int name, int val)
        {
            // /activeclip/link1/values (Float 0.0 - 1.0)
            string cmd = "/activeclip/link" + name.ToString() + "/values";
            float v = val / 127.0f;
            Msg(cmd, new object[] { v });
        }
        public void CompositionSlider(int name, int val)
        {
            string cmd = "/composition/link" + name.ToString() + "/values";
            float v = val / 127.0f;
            Msg(cmd, new object[] { v });
        }

        private void Msg(string command, object[] parameters)
        {
            OscMessage msg = new OscMessage(Origin, command);

            foreach (object d in parameters)
            {
                if (d is int)
                {
                    msg.Append((Int32)d);
                }
                else if (d is float)
                {
                    msg.Append((Single)d);
                }
                else if (d is String)
                {
                    msg.Append((String)d);
                }
            }

            msg.Send(endpoint);
        }

        internal void KeyPressed(Key key)
        {
            if (KeyCommandGenerator.CONTROL_KEY_CODES.ContainsKey(key))
            {
                // MessageBox.Show(KeyCommandGenerator.CONTROL_KEY_CODES[key].ToString());
                KeyCommand((int)KeyState.PRESS, KeyCommandGenerator.CONTROL_KEY_CODES[key]);
            }
        }

        private void KeyCommand(int commandCode, int keyCode)
        {
            String keyString = new String(new char[] { (char)keyCode });
            Msg("/keyboard", new object[] { commandCode, keyCode, keyString });
        }

        internal void KeyReleased(Key key)
        {
            if (KeyCommandGenerator.CONTROL_KEY_CODES.ContainsKey(key))
            {
                KeyCommand((int) KeyState.RELEASE, KeyCommandGenerator.CONTROL_KEY_CODES[key]);
            }
        }

        internal void KeysTyped(string keys)
        {
            foreach (char c in keys)
            {
                KeyTyped(c);
            }
        }

        private void KeyTyped(char c)
        {
            IList<KeyCommand> commands = keyCommandGenerator.generateCommands(c);
            foreach (KeyCommand command in commands)
            {
                Msg("/keyboard", new object[] {(int) command.keyState, command.keyCode, command.s});
            }

        }

        public IPEndPoint endpoint { get; private set; }

        private KeyCommandGenerator keyCommandGenerator = new KeyCommandGenerator();
    }
}
