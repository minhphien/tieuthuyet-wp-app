using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Comic.Views.Details
{
    public partial class InfoDetailView : PhoneApplicationPage
    {
        public InfoDetailView()
        {
            InitializeComponent();
            //AdUnit.ErrorOccurred += AdUnit_ErrorOccurred;
            
        }



        private void AdView_FailedToReceiveAd(object sender, GoogleAds.AdErrorEventArgs e)
        {

        }
    }
}