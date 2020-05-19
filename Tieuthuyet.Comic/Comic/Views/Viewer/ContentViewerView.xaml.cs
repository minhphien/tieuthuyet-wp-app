using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Comic.Resources;
using Comic.ViewModels.Viewer;
using Caliburn.Micro;
using Comic.Messages;
using System.Windows.Controls;


namespace Comic.Views.Viewer
{
    public partial class ContentViewerView : PhoneApplicationPage, 
        IHandle<ChapterItemTapMessage>,IHandle<ChapterContentLoadedMessage>,
        IHandle<ChangeContentFormatMessage>
    {
        protected IEventAggregator eventAggregator;
        public ContentViewerView()
        {
            InitializeComponent();
            InitializeAppBar();
            eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Subscribe(this);
        }

        void InitializeAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_Previous, 
                new Uri("/Assets/ApplicationBar.Previous.png", UriKind.Relative), Previous_Click));
            ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_Menu, 
                new Uri("/Assets/ApplicationBar.ChapterList.png", UriKind.Relative), ViewChapter_Click));
            //ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_TextEdit,
            //    new Uri("/Assets/ApplicationBar.TextEdit.png", UriKind.Relative), TextEdit_Click));
            ApplicationBar.Buttons.Add(GetAppBarButton(AppResources.AppBar_Next, 
                new Uri("/Assets/ApplicationBar.Next.png", UriKind.Relative), Next_Click));
        }

        ApplicationBarIconButton GetAppBarButton(string text, Uri iconUri, EventHandler eventHander)
        {
            var AppBtn = new ApplicationBarIconButton();
            AppBtn.Text = text;
            AppBtn.IconUri = iconUri;
            AppBtn.Click += eventHander;
            return AppBtn;
        }

        /* Handle ApplicationBar Events*/

        private void Next_Click(object sender, EventArgs e)
        {
            (DataContext as ContentViewerViewModel).GoNext();
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            (DataContext as ContentViewerViewModel).GoPrevious();
        }

        private void ViewChapter_Click(object sender, EventArgs e)
        {
            (DataContext as ContentViewerViewModel).ViewChapterList();
            ApplicationBar.IsVisible = false;
        }

        private void TextEdit_Click(object sender, EventArgs e)
        {
            eventAggregator.Publish(new OpenFormatBarMessage());
            
        }

        private void PhoneApplicationPage_BackKeyPress
            (object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((DataContext as ContentViewerViewModel).chapterListVisibility == true)
            {
                (DataContext as ContentViewerViewModel).HideChapterList();
                ApplicationBar.IsVisible = true;
                e.Cancel = true;
            }
        }

        public void Handle(ChapterItemTapMessage message)
        {
            if (!ApplicationBar.IsVisible)
            {
                ApplicationBar.IsVisible = true;
            }
        }

        private void HtmlViewer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplicationBar.IsVisible = !ApplicationBar.IsVisible;
            var t = HtmlViewer;
        }

        public void Handle(ChapterContentLoadedMessage message)
        {
            ContentContainer.ScrollToVerticalOffset(0);
        }

        public void Handle(ChangeContentFormatMessage message)
        {
            if (message.type == ChangeContentFormatMessage.MessageTypes.Size)
            {
                int newSizeValue= (int)message.Size;
                
                if (HtmlViewer.Html != null) {
                    string currentHtml = HtmlViewer.Html;
                    HtmlViewer.Html = "";
                    HtmlViewer.DefaultFontSize = (double)newSizeValue;
                    HtmlViewer.Html = currentHtml;
                }
                else
                {
                    HtmlViewer.DefaultFontSize = (double)newSizeValue;
                }
                
            }
        }
    }
}