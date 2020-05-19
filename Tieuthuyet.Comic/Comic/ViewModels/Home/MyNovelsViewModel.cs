using Caliburn.Micro;
using Comic.LocalDatabase;
using Comic.ViewModels.Shared;
using System.Collections.ObjectModel;

namespace Comic.ViewModels.Home
{
    public class MyNovelsViewModel : ViewModelBase
    {
        private ObservableCollection<HistoryItemViewModel> _historyNovel;
        public ObservableCollection<HistoryItemViewModel> HistoryNovel
        {
            get { return _historyNovel; }
            set
            {
                _historyNovel = value;
                OnPropertyChanged(() => HistoryNovel);
            }
        }
        
        #region Methods

        public MyNovelsViewModel()
        {
            navigationService = IoC.Get<INavigationService>();
        }
        
        public void DeleteAll()
        {
            if (HistoryNovel != null)
            {
                (new UserHistoryDb()).DeleteAllItems();
                LoadFromDb();
            }
        }
        
        public void LoadFromDb()
        {
            var historyNovelList = (new UserHistoryDb()).GetAllHistoryNovels();
            HistoryNovel = new ObservableCollection<HistoryItemViewModel>();
            foreach (var historyItem in historyNovelList)
            {
                HistoryItemViewModel HistoryItem = new HistoryItemViewModel();
                HistoryItem.Content = historyItem;
                HistoryNovel.Add(HistoryItem);
            }
        }
        
        #endregion
    }
}
