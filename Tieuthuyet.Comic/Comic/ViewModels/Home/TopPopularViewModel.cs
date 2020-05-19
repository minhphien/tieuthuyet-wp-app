using Comic.Model;
using Comic.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Comic.ViewModels.Home
{
    public class TopPopularViewModel : ViewModelBase
    {
        ObservableCollection<NovelItemViewModel> _novelSortTop;
        public ObservableCollection<NovelItemViewModel> novelSortTop
        {
            get
            {
                return _novelSortTop;
            }
            set
            {
                _novelSortTop = value;
                OnPropertyChanged(() => novelSortTop);
            }
        }
        
        public TopPopularViewModel()
        {
            IsLoading = true;
            novelSortTop = new ObservableCollection<NovelItemViewModel>();
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                List<NovelModel> list = (List<NovelModel>)await ModelLastestUpdated.GetLastUpdatedList(ListTypes.new_update, 0, 100);
                novelSortTop.Clear();
                foreach (var item in list)
                {
                    NovelItemViewModel novelViewModel = new NovelItemViewModel();
                    novelViewModel.NovelItem = item;
                    novelSortTop.Add(novelViewModel);
                }
                IsLoading = false;
            }
            catch(Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_NOVEL,e);
            }
        }
        
    }
}
