using System.Collections.ObjectModel;
using Comic.ViewModels.Shared;

namespace Comic.ViewModels
{
    public class NovelInGroup : ObservableCollection<NovelItemViewModel>
    {
        public NovelInGroup(string category)
        {
            Key = category;
        }

        public string Key { get; set; }

        public bool HasItems { get { return Count > 0; } }

    }
}
