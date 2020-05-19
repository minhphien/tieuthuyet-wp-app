using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Comic.ViewModels;
using System.IO.IsolatedStorage;
using Comic.Model;
using System.IO;
using Comic.ViewModels.AboutUs;
using Comic.ViewModels.Categories;
using Comic.ViewModels.Home;
using Comic.ViewModels.Viewer;
using Comic.ViewModels.Details;
using Comic.ViewModels.Shared;

namespace Comic
{
    public class ComicBootstrapper : PhoneBootstrapper
    {
        private PhoneContainer container;
        const string ROOT_URL = "http://service.comictogo.com/";
        const string ROOT_SETTINGS_FILE = @"comic.config";

        protected override void Configure()
        {
            container = new PhoneContainer();
            if (!Execute.InDesignMode)
                container.RegisterPhoneServices(RootFrame);
            container.PerRequest<HistoryItemViewModel>();
            container.PerRequest<ChapterItemViewModel>();
            container.PerRequest<NovelItemViewModel>();
            container.PerRequest<SearchResultsViewModel>();
            container.PerRequest<GenreItemViewModel>();
            container.PerRequest<AboutUsViewModel>();

            container.Singleton<FormatBarViewModel>();
            container.Singleton<GenreViewModel>();
            container.Singleton<MainViewModel>();
            container.Singleton<MyNovelsViewModel>();
            container.Singleton<ContentViewerViewModel>();
            container.Singleton<IObservableCollection<ChapterItemViewModel>>();
            
            container.Singleton<ChapterListViewModel>();
            container.PerRequest<InfoDetailViewModel>();
            container.Singleton<NovelListViewModel>();
              
            container.Singleton<TopPopularViewModel>();
            container.Singleton<SearchViewModel>();
            
            
            AddCustomConventions();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            base.OnLaunch(sender, e);
            SetConstanstValues();
            LocalDatabase.UserHistoryDb.CreateIfNotExistLocalDatabase();
            var navigationService = IoC.Get<INavigationService>();
            navigationService.UriFor<MainViewModel>().Navigate();
        }

        void SetConstanstValues()
        {
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.ROOT_URL, ROOT_URL);
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_LIST,
                "app/novel/list/");
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_DETAIL,
                "app/novel/detail/");
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_CHAPTERLIST,
                "app/novel_chapter/list/");
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_GENRELIST,
                "app/novel_genres/list/");
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_CHAPTERDETAIL,
                "app/novel_chapter/read/");
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_GENRES,
                "app/novel_genres/read/");
            LocalSettings.SetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_SEARCH,
                "app/novel/search/");
        }
        
        static void AddCustomConventions()
        {
           
        } 
    }
}
