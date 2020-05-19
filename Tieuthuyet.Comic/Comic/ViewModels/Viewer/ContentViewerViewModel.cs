using System;
using System.Text;
using System.IO.IsolatedStorage;
using Caliburn.Micro;
using System.IO;
using Comic.ViewModels.Shared;
using Comic.Model;
using Comic.Messages;
namespace Comic.ViewModels.Viewer
{
    public class ContentViewerViewModel : ViewModelBase, IHandle<ChapterItemTapMessage>
    {
        #region Properties

        ChapterItemViewModel _chapterContent;
        public ChapterItemViewModel chapterContent
        {
            get
            {
                return _chapterContent;
            }
            set
            {
                _chapterContent = value;
                OnPropertyChanged(() => chapterContent);
            }
        }

        ChapterListViewModel _chapterList;
        public ChapterListViewModel chapterList
        {
            get
            {
                return _chapterList;
            }
            set
            {
                _chapterList = value;
                OnPropertyChanged(() => chapterList);
            }
        }

        FormatBarViewModel _formatBar;
        public FormatBarViewModel FormatBar
        {
            get { return _formatBar; }
            set {
                if (_formatBar != value)
                {
                    _formatBar = value;
                    OnPropertyChanged(() => FormatBar);
                }
                
            }
        }

        private int _chapterId;
        public int chapterId
        {
            get { return _chapterId; }
            set
            {
                
                if (_chapterId != value)
                {
                    LoadHtmlData(value);
                    _chapterId = value;
                }
            }
        }

        private int _novelId;
        public int NovelId
        {
            get { return _novelId; }
            set {
                if (value != _novelId)
                {
                    _novelId = value;
                    chapterList.novelId = NovelId;
                }
            }
        }

        string _content;
        public string content
        {
            get { return _content; }
            set { 
                _content = value;
                OnPropertyChanged(() => content);
                eventAggregator.Publish(new ChapterContentLoadedMessage());
            }
        }

        private bool _chapterListVisibility;
        public bool chapterListVisibility
        {
            get { return _chapterListVisibility; }
            set { 
                _chapterListVisibility = value;
                OnPropertyChanged(() => chapterListVisibility);
            }
        }
        
        const string FILE_PATH = @"chapter.html";

        #endregion

        #region Methods

        public ContentViewerViewModel(ChapterItemViewModel chapterContent, ChapterListViewModel chapterList, FormatBarViewModel FormatBar)
        {
            this.chapterContent = chapterContent;
            this.chapterList = chapterList;
            this.FormatBar = FormatBar;
            eventAggregator.Subscribe(this);
        }
        
        protected override void OnActivate()
        {
            chapterListVisibility = false;
        }

        private async void LoadData(int ID)
        {
            try
            {
                IsLoading = true;
                var chapterDetail = (ChapterModel)await ModelLastestUpdated.GetChapterContent(ID);
                chapterContent = IoC.Get<ChapterItemViewModel>();
                chapterContent.Content = chapterDetail;
                UpdateToHistory();
                LocalSettings.SetUserHistory(
                    LocalSettings.UserHistoryParams.CURRENT_CHAPTERID,
                    ID);

                var htmlContent = AddHeader() + chapterDetail.Content + AddFooter();
                IsolatedStorageFile newfile = IsolatedStorageFile.GetUserStoreForApplication();
                if (newfile.FileExists(FILE_PATH))
                {
                    newfile.DeleteFile(FILE_PATH);
                }
                IsolatedStorageFileStream chaptercontent = newfile.CreateFile(FILE_PATH);

                StreamWriter writer = new StreamWriter(chaptercontent, Encoding.Unicode);
                writer.WriteLine(htmlContent);
                writer.Close();
                content = FILE_PATH;
                OnPropertyChanged(() => content);
                IsLoading = false;

            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_CHAPTERCONTENT, e);
            }
        }

        private async void LoadHtmlData(int ID)
        {
            try
            {
                IsLoading = true;
                content = "";
                var chapterDetail = await ModelLastestUpdated.GetChapterContent(ID);
                chapterContent.Content = chapterDetail;
                UpdateToHistory();
                content =  ReformatHtml(chapterDetail.Content);
                IsLoading = false;
            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_CHAPTERCONTENT, e);
            }
        }

        string ReformatHtml(string html){
            var result = html;
            result = result.Replace("\n", "");
            result = result.Replace("  ", " ");
            result = result.Replace(" />", "/>");
            result = result.Replace("/> <", "/><");
            result = result.Replace("<br/>", "<br>");
            result = result.Replace("<br><br>", "<br>");
            return result;
        }
        
        private void UpdateToHistory()
        {
            NovelModel currentNovel = LocalSettings.GetUserHistory(LocalSettings.UserHistoryParams.CURRENT_NOVEL) as NovelModel ?? null;
            if (currentNovel != null)
            {
                UserHistories currentHistory = new UserHistories();
                currentHistory.Author = currentNovel.Author;
                currentHistory.Image = currentNovel.Image;
                currentHistory.NovelId = currentNovel.ID;
                currentHistory.NovelName = currentNovel.Name;
                currentHistory.Genre = currentNovel.Genre;

                currentHistory.ChapterName = chapterContent.Content.Name;
                currentHistory.ChapterId = chapterId;

                currentHistory.TimeCreated = DateTime.Now;
                (new LocalDatabase.UserHistoryDb()).Insert(currentHistory);
            }
            
        }
        
        private string AddHeader()
        {
            return Resources.AppResources.HtmlHeader;
        }
        
        private string AddFooter()
        {
            return Resources.AppResources.HtmlFooter;
        }

        public void GoNext()
        {
            var NextChapterId = chapterList.GetNextChapterId(chapterId);
            if (NextChapterId != -1)
            {
                chapterId = NextChapterId;
            }
            
        }

        public void GoPrevious()
        {
            var PreviousChapterId = chapterList.GetPreviousChapterId(chapterId);
            
            if (PreviousChapterId != -1)
            {
                chapterId = PreviousChapterId;
            }
        }
        
        public void ViewChapterList()
        {
            chapterListVisibility = true;
        }

        public void HideChapterList()
        {
            chapterListVisibility = false;
        }

        public void Handle(ChapterItemTapMessage message)
        {
            if (chapterList.Contains(message.ChapterId))
            {
                HideChapterList();
                chapterId = message.ChapterId;
            }
        }
        
        #endregion

    }
}
