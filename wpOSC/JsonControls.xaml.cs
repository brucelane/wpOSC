using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;

namespace wpOSC
{
    public sealed partial class JsonControls : UserControl
    {
        public delegate void OnAction(string cmd, object value);
        public event OnAction OnActionHandler=null ;
        private Dictionary<UIElement, string> _dicActions = new Dictionary<UIElement, string>();


        public JsonControls()
        {
            InitializeComponent();            
        }

        public bool Parse(string sJson)
        {
            _dicActions.Clear();
            LayoutRoot.Children.Clear();

            return ParseArray(LayoutRoot, sJson);
        }

        private bool ParseArray(Panel root, string sJson)
        {
            JToken data;
            try {
                data = JToken.Parse(sJson);
            }
            catch {
                return false; 
            }

            //orientation  = h , v
            string dir = data["dir"].ToObject<string>();

            var stack = new StackPanel();
            stack.Orientation = dir.Equals("h",StringComparison.OrdinalIgnoreCase) ?  Orientation.Horizontal : Orientation.Vertical;
            root.Children.Add(stack);

            foreach (var item in data["items"].ToList())
            {
                if (item["dir"] !=null)
                {
                    //sub list
                    ParseArray(stack, item.ToString());
                }
                else
                {
                    try
                    {
                        string label = item["label"].ToObject<string>();
                        string type = item["type"].ToObject<string>();
                        string action = item["action"].ToObject<string>();

                        //int width = item["width"].ToObject<int>(); ;
                        //int height = item["height"].ToObject<int>(); ;

                        switch (type)
                        {
                            case "button":
                                {
                                    var bt = new Button();
                                    bt.Click += bt_Click;
                                    bt.Content = label;
                                    bt.Width = 160;
                                    bt.Height = 100;
                                    _dicActions.Add(bt, action);
                                    stack.Children.Add(bt);
                                    break;
                                }

                            case "slider":
                                {
                                    var sl = new Slider();
                                    sl.ManipulationCompleted += sl_ManipulationCompleted;
                                    _dicActions.Add(sl, action);
                                    stack.Children.Add(sl);
                                    break;
                                }

                        }
                    }
                    catch { }
                }
                  
            }

            return true;
        }

        #region events

        private void bt_Click(Object sender, RoutedEventArgs e)
        {
            if (_dicActions.ContainsKey(sender as UIElement) && OnActionHandler!=null)
            {
                OnActionHandler(_dicActions[sender as UIElement],null);
            }
        }


        private void sl_ManipulationCompleted(Object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (_dicActions.ContainsKey(sender as UIElement) && OnActionHandler != null)
            {
                OnActionHandler(_dicActions[sender as UIElement], (sender as Slider).Value );
            }
        }

        #endregion
    }
}
