using System;
using Caliburn.Micro;
using Comic.Model;
using Comic.ViewModels.Details;

namespace Comic.ViewModels.Shared
{
    
    public class NovelItemViewModel:ViewModelBase
    {
        private NovelModel _novelItem;
        public NovelModel NovelItem
        {
            get
            {
                return _novelItem;
            }
            set
            {
                _novelItem = value;
                OnPropertyChanged(() => NovelItem);
            }
        }

        public NovelItemViewModel()
        {
            navigationService = IoC.Get<INavigationService>();
        }

        public static NovelItemViewModel GetRandomNovel(int id)
        {
            NovelItemViewModel novel = new NovelItemViewModel();

            novel.NovelItem.ID = id;
            novel.NovelItem.Name = "Sample";
            novel.NovelItem.Author = "Sample";
            novel.NovelItem.Genre = "Sample";
            novel.NovelItem.Image = "/Images/Person.jpg";
            return novel;
        }

        public static string GetNameKey(NovelItemViewModel person)
        {
            char key = char.ToLower(person.NovelItem.Name[0]);

            if (key < 'a' || key > 'z')
            {
                key = '#';
            }

            return key.ToString();
        }

        public static int CompareByName(object obj1, object obj2)
        {
            NovelItemViewModel p1 = (NovelItemViewModel)obj1;
            NovelItemViewModel p2 = (NovelItemViewModel)obj2;

            int result = p1.NovelItem.Name.CompareTo(p2.NovelItem.Name);            
            return result;
        }

        public void NovelItemTap(NovelItemViewModel datacontext)
        {
            LocalSettings.SetUserHistory(
                LocalSettings.UserHistoryParams.CURRENT_NOVEL,
                datacontext.NovelItem);
            navigationService.UriFor<InfoDetailViewModel>()
                .WithParam(x=>x.IdNovel,datacontext.NovelItem.ID)
                .WithParam(x=>x.Name,datacontext.NovelItem.Name.ToLowerInvariant())
                .WithParam(x=>x.img,new System.Windows.Media.Imaging.BitmapImage(new Uri(datacontext.NovelItem.Image, UriKind.Absolute)))
                .Navigate();

        }

        public void Dispose()
        {
            
        }
    }
}
