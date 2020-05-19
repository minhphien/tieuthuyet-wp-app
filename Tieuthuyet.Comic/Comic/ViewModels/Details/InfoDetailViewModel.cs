using Caliburn.Micro;
using Comic.Messages;
using Comic.Model;
using Comic.ViewModels.Shared;
using Comic.ViewModels.Viewer;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Comic.ViewModels.Details
{
    public class InfoDetailViewModel : ViewModelBase, IHandle<ChapterItemTapMessage>
    {
        #region Properties
        NovelItemViewModel _novelInfo;
        public NovelItemViewModel novelInfo
        {
            get
            {
                return _novelInfo;
            }
            set
            {
                if (_novelInfo != value)
                {
                    _novelInfo = value;
                    OnPropertyChanged(() => novelInfo);
                    //if (value != null&&(_novelInfo==null || _novelInfo.NovelItem.ID !=value.NovelItem.ID))
                    //{
                        
                    //}
                    
                }
            }
        }

        private int _idNovel = -1;
        public int IdNovel
        {
            get { return _idNovel; }
            set
            {
                if (_idNovel != value)
                {
                    _idNovel = value;
                    chapterList.novelId = value;
                    LoadData(value);
                }
            }
        }
        
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(() => Name);
            }
        }
        ImageSource _img;
        public ImageSource img
        {
            get { return _img; }
            set { _img = value; OnPropertyChanged(() => img); }
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

        #endregion

        #region Methods

        public InfoDetailViewModel(NovelItemViewModel novelInfo, ChapterListViewModel chapterList)
        {
            this.novelInfo = novelInfo;
            this.chapterList = chapterList;
            eventAggregator.Subscribe(this);
        }
        
        protected override void OnInitialize()
        {
            
        }

        protected override void OnActivate()
        {
            if (IdNovel == -1)
            {
                var currentNovel = LocalSettings.
                    GetUserHistory(LocalSettings.UserHistoryParams.CURRENT_NOVEL) as NovelModel
                    ?? null;
                IdNovel = currentNovel.ID;
            }
            
            if(novelInfo.NovelItem!=null)
            {
                
            }
        }
        
        private async void LoadData(int novelId)
        {
            try
            {
                IsLoading = true;
                var noveldetail = (NovelModel)await ModelLastestUpdated.GetInfoDetails(novelId);
                Name = noveldetail.Name.ToLowerInvariant();
                img = new BitmapImage(new Uri(noveldetail.Image, UriKind.Absolute));
                novelInfo.NovelItem = noveldetail;
                LocalSettings.SetUserHistory(
                  LocalSettings.UserHistoryParams.CURRENT_NOVEL,
                  novelInfo.NovelItem);
                IsLoading = false;
            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_CHAPTER, e);
            }
        }
        
        public async void LoadDataById(int novelId,int chapterId)
        {
            try
            {
                IsLoading = true;
                var noveldetail = await ModelLastestUpdated.GetInfoDetails(novelId);
                novelInfo = IoC.Get<NovelItemViewModel>();
                novelInfo.NovelItem = noveldetail;
                LocalSettings.SetUserHistory(
                  LocalSettings.UserHistoryParams.CURRENT_NOVEL,
                  novelInfo.NovelItem);
                IsLoading = false;
                eventAggregator.Publish(new FinishServerCallMessage(novelId, chapterId));
            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_CHAPTER, e);
            }
        }

        #endregion

        public void Handle(ChapterItemTapMessage message)
        {
            //Viet ham mo contenviewer len o day 
            if (this.IsActive)
            {
                navigationService.UriFor<ContentViewerViewModel>()
                    .WithParam(x => x.chapterId, message.ChapterId)
                    .WithParam(x=>x.chapterList.novelId,IdNovel)
                    .Navigate();
            }
        }
    }
}
