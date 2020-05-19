using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Caliburn.Micro;

namespace Comic.Views.Shared
{
    public partial class FormatBarView : UserControl,IHandle<Comic.Messages.OpenFormatBarMessage>
    {
        protected readonly IEventAggregator eventAggregator;

        private bool IsAppear { get; set; }

        public FormatBarView()
        {
            InitializeComponent();
            IsAppear = false;
            eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Subscribe(this);
        }

        public void AppearPanel()
        {
            Appear.Begin();
        }

        public void DisappearPanel()
        {
            Disappear.Begin();
        }

        public void ToggleAppear()
        {
            if (!IsAppear) AppearPanel();
            else DisappearPanel();
        }

        public void Handle(Messages.OpenFormatBarMessage message)
        {
            ToggleAppear();
        }

        private void Appear_Completed(object sender, EventArgs e)
        {
            IsAppear = true;
        }

        private void Disappear_Completed(object sender, EventArgs e)
        {
            IsAppear = false;
        }
    }
}
