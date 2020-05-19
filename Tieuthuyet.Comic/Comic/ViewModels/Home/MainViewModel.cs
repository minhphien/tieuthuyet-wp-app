using System.Windows.Input;
using Caliburn.Micro;
using Comic.Commands;
using Comic.Messages;
using Comic.Model;
using Comic.ViewModels.Categories;
using Comic.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Phone.Tasks;

namespace Comic.ViewModels.Home
{
    public class MainViewModel : ViewModelBase, IHandle<SearchMessage>
    {
        #region Properties

        private ObservableCollection<NovelItemViewModel> _novelByName;
        public ObservableCollection<NovelItemViewModel> NovelByName
        {
            get { return _novelByName; }
            set { _novelByName = value; }
        }

        private GenreViewModel _genres;
        public GenreViewModel GenreList
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged(() => GenreList); }
        }

        private MyNovelsViewModel _myNovels;
        public MyNovelsViewModel MyNovel
        {
            get { return _myNovels; }
            set { _myNovels = value; OnPropertyChanged(() => MyNovel); }
        }

        private Shared.SearchViewModel _SearchContainer;
        public Shared.SearchViewModel SearchContainer
        {
            get { return _SearchContainer; }
            set { _SearchContainer = value; OnPropertyChanged(() => SearchContainer); }
        }

        

        #endregion

        #region Commands
        public ICommand RatingCommand { get; set; }
        public ICommand AboutUsCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CategoryCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ClearListCommand { get; set; }
        
        public void RateApp()
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
        private void NavigateToAboutUs()
        {
            navigationService.UriFor<AboutUs.AboutUsViewModel>().Navigate();
        }

        public void ExecuteSearch()
        {
            IsSearching = !IsSearching;
            SearchContainer = IsSearching ? IoC.Get<SearchViewModel>() : null;
        }

        public void GoToSearchResult()
        {
            navigationService.UriFor<SearchResultsViewModel>()
                .WithParam(x=>x.keyword,SearchContainer.SearchKeyword)
                .Navigate();
        }

        #endregion

        public MainViewModel()
        {
            eventAggregator.Subscribe(this);
            RatingCommand = new ActionCommand(RateApp);
            AboutUsCommand = new ActionCommand(NavigateToAboutUs);
            SearchCommand = new ActionCommand(ExecuteSearch);
            CategoryCommand = new ActionCommand(navigationService.UriFor<TopPopularViewModel>().Navigate);
            RefreshCommand = new ActionCommand(RefreshData);
        }
        

        #region Methods

        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            IsSearching = false;
            NovelByName = new ObservableCollection<NovelItemViewModel>();
            GenreList = IoC.Get<GenreViewModel>();
            MyNovel = IoC.Get<MyNovelsViewModel>();
            LoadDataFromService();
        }

        protected override void OnActivate()
        {
            MyNovel.LoadFromDb();
        }

        public void RefreshData()
        {
            LoadDataFromService();
            MyNovel.LoadFromDb();
            GenreList = IoC.Get<GenreViewModel>();
            GenreList.RefreshData();
        }

        public async void LoadDataFromService()
        {
            try
            {
                int off = 0, limit = 10;
                bool flag = true;
                NovelByName.Clear();
                IsLoading = true;
                do
                {
                    
                    var novellist = (List<NovelModel>)await 
                        ModelLastestUpdated.GetLastUpdatedList(ListTypes.new_update, off, limit);
                    if (novellist.Count < limit) flag = false;
                    if (off > 10) flag = false;
                    foreach (NovelModel item in novellist)
                    {
                        NovelItemViewModel novelViewModel = new NovelItemViewModel();
                        novelViewModel.NovelItem = item;
                        NovelByName.Add(novelViewModel);
                    }
                    
                    off += limit;
                    limit = 30;
                } while (flag);
                IsLoading = false;
            }
            catch(Exception e)
            {
                ShowMessage(ViewModelBase.ComicErrors.CANNOT_LOAD_NOVEL,e);
            }
        }
        
        #endregion

        #region Event Handler
        

        public void Categories_Click(object sender, EventArgs e)
        {
            navigationService.UriFor<GenreViewModel>().Navigate();
        }

        public void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (IsSearching)
            {
                IsSearching = false;
                SearchContainer = null;
                e.Cancel = true;
            }
        }

        public void SearchContainer_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsSearching)
            {
                IsSearching = false;
                SearchContainer = null;
            }
        }
        
        #endregion

        public void Handle(SearchMessage message)
        {
            navigationService.UriFor<SearchResultsViewModel>().WithParam(x=>x.keyword,message.Keyword).Navigate();
        }

    }
}
