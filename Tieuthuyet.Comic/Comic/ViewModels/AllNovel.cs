using System.Collections.Generic;
using Comic.ViewModels.Shared;


namespace Comic.ViewModels
{
    public class AllNovel : IEnumerable<NovelItemViewModel>
    {
        private static Dictionary<int, NovelItemViewModel> _novelLookup;
        private static AllNovel _instance;

        public static AllNovel Current
        {
            get
            {
                return _instance ?? (_instance = new AllNovel());
            }
        }

        public NovelItemViewModel this[int index]
        {
            get
            {
                NovelItemViewModel novel;
                _novelLookup.TryGetValue(index, out novel);
                return novel;
            }
        }

        #region IEnumerable<Novel> Members

        public IEnumerator<NovelItemViewModel> GetEnumerator()
        {
            EnsureData();
            //LoadDataFromService();
            return _novelLookup.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            EnsureData();
            return _novelLookup.Values.GetEnumerator();
            
        }

        #endregion

        private void EnsureData()
        {
            if (_novelLookup == null)
            {
                _novelLookup = new Dictionary<int, NovelItemViewModel>();
            }   
                for (int n = 0; n < 5; ++n)
                {
                    NovelItemViewModel person = NovelItemViewModel.GetRandomNovel(n);
                    _novelLookup[n] = person;
                }
        }

    }
}
