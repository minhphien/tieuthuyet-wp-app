using System;
using Caliburn.Micro;
using Comic.Model;
using Comic.ViewModels.Details;
using Comic.ViewModels.Viewer;
using Comic.Messages;

namespace Comic.ViewModels.Shared
{
    
    public class HistoryItemViewModel:ViewModelBase, IHandle<FinishServerCallMessage>
    {
        private UserHistories _content;
        public UserHistories Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged(() => Content);
            }
        }

        public HistoryItemViewModel()
        {
            navigationService = IoC.Get<INavigationService>();
            eventAggregator.Subscribe(this);
        }

        public void ItemTap(HistoryItemViewModel datacontext)
        {
            NovelModel currentNovel = new NovelModel();
            currentNovel.Author = datacontext.Content.Author;
            currentNovel.Image = datacontext.Content.Image;
            currentNovel.ID = datacontext.Content.NovelId;
            currentNovel.Name = datacontext.Content.NovelName;
            currentNovel.Genre = datacontext.Content.Genre;
            LocalSettings.SetUserHistory(
                LocalSettings.UserHistoryParams.CURRENT_NOVEL,
                currentNovel);
            navigationService.UriFor<ContentViewerViewModel>()
                .WithParam(x => x.chapterId, datacontext.Content.ChapterId)
                .WithParam(x => x.NovelId,datacontext.Content.NovelId)
                .Navigate();
        }

        public void Handle(FinishServerCallMessage message)
        {
            
            
        }
    }
}
