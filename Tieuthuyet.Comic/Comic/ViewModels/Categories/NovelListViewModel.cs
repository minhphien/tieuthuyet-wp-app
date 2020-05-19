using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Comic.Model;
using Comic.ViewModels.Shared;

namespace Comic.ViewModels.Categories

{
    public class NovelListViewModel : ViewModelBase
    {
        #region Properties
        ObservableCollection<NovelItemViewModel> _novelNovelList;
        public ObservableCollection<NovelItemViewModel> novelNovelList
        {
            get
            {
                return _novelNovelList;
            }
            set
            {
                _novelNovelList = value;
                OnPropertyChanged(() => novelNovelList);
            }
        }

        private GenreItemViewModel _currentGenre;

        public GenreItemViewModel currentGenre
        {
            get { return _currentGenre; }
            set { _currentGenre = value; }
        }

        
        string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(() => title); }
        }
        ImageSource _img;
        public ImageSource img
        {
            get { return _img; }
            set { _img = value; OnPropertyChanged(() => img); }
        }
        #endregion

        #region Methods
        public NovelListViewModel()
        {
            
            
        }
        protected override void OnInitialize()
        {
            
        }
        protected override void OnActivate()
        {
            IsLoading = true;
            
            currentGenre = LocalSettings.
                GetUserHistory(LocalSettings.UserHistoryParams.CURRENT_GENRE) as GenreItemViewModel
                ?? null;

            if (currentGenre!=null)
            {
                novelNovelList = new ObservableCollection<NovelItemViewModel>();
                title = currentGenre.Name.ToUpperInvariant();
                img = new BitmapImage(new Uri(currentGenre.Image, UriKind.Absolute));
                LoadData();

            }
        }
        private async void LoadData()
        {
            try
            {
                List<NovelModel> list = (List<NovelModel>)
                    await ModelLastestUpdated.GetNovelListByGenre(currentGenre.Alias, 0, 50);
                novelNovelList.Clear();
                foreach (var item in list)
                {
                    NovelItemViewModel novelViewModel = new NovelItemViewModel();
                    novelViewModel.NovelItem = item;
                    novelNovelList.Add(novelViewModel);
                }
                IsLoading = false;
            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_NOVEL,e);
            }
        }
        
        
        #endregion
    }
}
