using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comic.ViewModels;
using Caliburn.Micro;

namespace Comic.ViewModels.Categories
{
    public class GenreViewModel : ViewModelBase
    {
        ObservableCollection<GenreItemViewModel> _genreList;
        public ObservableCollection<GenreItemViewModel> genreList
        {
            get
            {
                return _genreList;
            }
            set
            {
                _genreList = value;
                OnPropertyChanged(() => genreList);
            }
        }
        public GenreViewModel()
        {
            genreList = new ObservableCollection<GenreItemViewModel>();
            LoadData();
        }
        
        protected override void OnActivate()
        {

        }
        protected override void OnInitialize()
        {
            genreList = new ObservableCollection<GenreItemViewModel>();
            LoadData();
        }
        public void RefreshData()
        {
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                List<GenreItemViewModel> list = (List<GenreItemViewModel>)await ModelLastestUpdated.GetGenreList();
                genreList.Clear();
                foreach (var item in list)
                {
                    genreList.Add(item);
                }
            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_GENRE,e);
            }
        }
        
        
    }
}
