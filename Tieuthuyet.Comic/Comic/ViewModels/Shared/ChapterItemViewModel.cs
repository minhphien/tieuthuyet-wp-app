using Newtonsoft.Json;
using Caliburn.Micro;
using Comic.ViewModels.Viewer;
using Comic.Model;
using Comic.Messages;

namespace Comic.ViewModels.Shared
{
    public class ChapterItemViewModel : ViewModelBase
    {
        private ChapterModel _content;
        public ChapterModel Content
        {
            get { return _content; }
            set { 
                _content = value;
                OnPropertyChanged(() => Content);
            }
        }

        public static ChapterItemViewModel GetRandomChapter(int id)
        {

            ChapterItemViewModel chapter = new ChapterItemViewModel();
            chapter.Content.ID = id;
            chapter.Content.Name = "Chapter Name";
            chapter.Content.LastUpdate = "Last Updated Date";
            chapter.Content.Create = "Created Date";
            return chapter;
        }

        public void ChapterItemTap(ChapterItemViewModel datacontext)
        {
            eventAggregator.Publish(new ChapterItemTapMessage(datacontext.Content.ID));
        }

    }
}
