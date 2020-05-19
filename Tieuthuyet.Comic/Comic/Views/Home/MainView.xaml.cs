using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Comic.Resources;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Comic.ViewModels.Home;
using BindableApplicationBar;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Comic.Views.Home
{
    public partial class MainView : PhoneApplicationPage
    {
        // Constructor
        public MainView()
        {
            InitializeComponent();
            //(ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Text = AppResources.AppBar_Rate;
            //(ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).Text = AppResources.AppBar_AboutUs;
           // ShowNormalAppBar();
            
        }

        private void ShowNormalAppBar()
        {
            ApplicationBar.Buttons.Clear();
            //ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_Search, new Uri("/Assets/ApplicationBar.Search.png", UriKind.Relative), Search_Click));
            //ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_Top, new Uri("/Assets/ApplicationBar.Top.png", UriKind.Relative), Top_Click));
            //ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_Refresh, new Uri("/Assets/ApplicationBar.Refresh.png", UriKind.Relative), Refresh_Click));
        }

        private void ShowMyPivotAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_DeleteAll, new Uri("/Assets/ApplicationBar.Delete.png", UriKind.Relative), DeleteHistory_Click));
        }

        ApplicationBarIconButton GetAppBarButton(string text, Uri iconUri, EventHandler eventHander)
        {
            var AppBtn = new ApplicationBarIconButton();
            AppBtn.Text = text;
            AppBtn.IconUri = iconUri;
            //AppBtn.MouseLeftButtonDown += eventHander;
            return AppBtn;
        }

        private void DeleteHistory_Click(object sender, EventArgs e)
        {
            (DataContext as MainViewModel).MyNovel.DeleteAll();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var temp = this.DataContext as MainViewModel;
            temp.OnBackKeyPress(e);
        }

        private void SearchContainer_LostFocus(object sender, RoutedEventArgs e)
        {
            var temp = this.DataContext as MainViewModel;
            temp.SearchContainer_LostFocus(sender, e);
        }

        private void Pivot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.RemovedItems[0] == MyNovelsPivot)
            {
                ShowNormalAppBar();
            }
            else if (e.AddedItems[0] == MyNovelsPivot)
            {
                ShowMyPivotAppBar();
            }
        }

    }
}