using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Caliburn.Micro;
using Comic.Messages;

namespace Comic.Views.Shared
{
    public class EventSearchAgr : EventArgs
    {
        public string KeyWord { get; set; }
        public EventSearchAgr(string KeyWord)
        {
            this.KeyWord = KeyWord;
        }

    }
    
    public partial class SearchView : UserControl
    {
        //public EventHandler<EventSearchAgr> Search_Tapped;
        
        public SearchView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Appear.Begin();
            SearchBox.Focus();
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var temp = sender as TextBox;
                if (temp.Text.Length > 0) InvokeSearchTapEvent(temp.Text);
            }
        }
        
        private void InvokeSearchTapEvent(string keyword)
        {
            //if (Search_Tapped != null)
            //{
            //    Search_Tapped(this, new EventSearchAgr(keyword));
                IoC.Get<IEventAggregator>().Publish(new SearchMessage(keyword));
            //}
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (SearchBox.Text.Length > 0) InvokeSearchTapEvent(SearchBox.Text);
        }
        

    }
}
