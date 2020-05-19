using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.ViewModels.Shared
{
    public class SearchViewModel : ViewModelBase
    {
        public string SearchKeyword { get; set; }
        public SearchViewModel()
        {
            SearchKeyword = Resources.AppResources.SearchLabel;
        }
    }
}
