using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Comic.Resources;
using Comic.ViewModels.Shared;
using Comic.Model;

namespace Comic.ViewModels.Home
{
    public class SearchResultsViewModel: ViewModelBase
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
        
        string _title;
        public string title
        {
            get { return _title; }
            set {
                var rm = new LocalizedStrings();
                _title = AppResources.SearchResultLabel + " \"" + value + "\"";
                OnPropertyChanged(() => title); }
        }

        string _keyword;
        public string keyword
        {
            get { return _keyword; }
            set { _keyword = value; RefreshData(); OnPropertyChanged(() => keyword); }
        }
        ImageSource _img;
        public ImageSource img
        {
            get { return _img; }
            set { _img = value; OnPropertyChanged(() => img); }
        }
        #endregion

        #region Methods
        public SearchResultsViewModel()
        {
            
            
        }
        protected override void OnActivate()
        {
            IsLoading = true;
            IsEmpty = false;
            novelNovelList = new ObservableCollection<NovelItemViewModel>();
            title = keyword;
            RefreshData();
        }
        private async void RefreshData()
        {
            try
            {
                List<NovelModel> list = (List<NovelModel>)await ModelLastestUpdated.GetSearchNovelList(keyword, 0, 50);
                novelNovelList.Clear();
                foreach (var item in list)
                {
                    NovelItemViewModel novelViewModel = new NovelItemViewModel();
                    novelViewModel.NovelItem = item;
                    novelNovelList.Add(novelViewModel);
                }
                if (list.Count == 0) IsEmpty = true;
                IsLoading = false;
            }
            catch(Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_NOVEL,e);
            }
        }
        #endregion
    }
}
