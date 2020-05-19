using Newtonsoft.Json;
using System;
using Caliburn.Micro;
using System.Runtime.Serialization;
using Comic.Model;

namespace Comic.ViewModels.Categories
{
    [DataContract]
    public class GenreItemViewModel 
    {
        protected INavigationService navigationService;
        [DataMember]
        [JsonProperty("id")]
        public int ID { get;  set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get;  set; }
        
        [DataMember]
        [JsonProperty("description")]
        public string Description { get;  set; }

        [DataMember]
        [JsonProperty("alias")]
        public string Alias { get;  set; }

        [DataMember]
        [JsonProperty("image")]
        public string Image { get;  set; }

        public GenreItemViewModel()
        {
            navigationService = IoC.Get<INavigationService>();
        }

        public void Genre_Tap(GenreItemViewModel datacontext)
        {

            LocalSettings.SetUserHistory(
              LocalSettings.UserHistoryParams.CURRENT_GENRE,
              datacontext);

            navigationService.UriFor<NovelListViewModel>().Navigate();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
