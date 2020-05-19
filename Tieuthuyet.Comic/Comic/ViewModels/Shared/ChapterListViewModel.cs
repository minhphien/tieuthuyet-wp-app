using Comic.Messages;
using Comic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.ViewModels.Shared
{
    public class ChapterListViewModel : ViewModelBase
    {
        ObservableCollection<ChapterItemViewModel> _chapterList;
        public ObservableCollection<ChapterItemViewModel> chapterList
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

        private List<int> IdChapterList;

        private int _novelId;
        public int novelId
        {
            get { return _novelId; }
            set {
                if (_novelId != value)
                {
                    _novelId = value;
                    LoadData(value);
                }
            }
        }
        
        public ChapterListViewModel()
        {
            OnInitialize();
        }

        protected override void OnInitialize()
        {
            chapterList = new ObservableCollection<ChapterItemViewModel>();
            //if (novelId == 0)
            //{
            //    var novel = LocalSettings
            //        .GetUserHistory(LocalSettings.UserHistoryParams.CURRENT_NOVEL) as NovelModel
            //        ?? null;
            //    if (novel != null) { novelId = novel.ID; }
            //}
        }

        private async void LoadData(int novelId)
        {
            try
            {
                IsLoading = true;
                var chapters = (List<ChapterModel>)await ModelLastestUpdated.GetChapterList(novelId);
                chapterList.Clear();
                foreach (var item in chapters)
                {
                    ChapterItemViewModel chapterItem = new ChapterItemViewModel();
                    chapterItem.Content = item;
                    chapterList.Add(chapterItem);
                }
                UpdateIdChapterList(chapterList);
                IsLoading = false;
                
            }
            catch (Exception e)
            {
                ShowMessage(ComicErrors.CANNOT_LOAD_CHAPTER, e);
            }
        }

        private void UpdateIdChapterList(ObservableCollection<ChapterItemViewModel> value)
        {
            IdChapterList = value.Select(item => item.Content.ID).ToList();
        }

        public int GetNextChapterId(int chapterId)
        {
            var chapterIndex = IdChapterList.IndexOf(chapterId);
            if (chapterIndex >= 0 && chapterIndex < IdChapterList.Count - 1)
            {
                return IdChapterList[chapterIndex + 1];
            }
            return -1;
        }
        
        public int GetPreviousChapterId(int chapterId)
        {
            var chapterIndex = IdChapterList.IndexOf(chapterId);
            if (chapterIndex > 0 && chapterIndex < IdChapterList.Count)
            {
                return IdChapterList[chapterIndex - 1];
            }
            return -1;
        }

        public bool Contains(int chapterId)
        {
            return IdChapterList.Contains(chapterId);
        }
    }
}
