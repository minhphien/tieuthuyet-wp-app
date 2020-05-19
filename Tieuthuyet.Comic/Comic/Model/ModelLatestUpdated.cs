// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Comic.Resources;
using Comic.Model;
using Comic.ViewModels.Shared;
using Comic.ViewModels.Categories;

namespace Comic.ViewModels
{
    public enum ListTypes{
        new_update,hot,top_rating,full
    }
    
    public class ModelLastestUpdated : ModelBase
    {
       // private Novel _novel;
        private static RestClient _client= new RestClient(
            LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.ROOT_URL) as string);

        public ModelLastestUpdated()
        {
           
        }
        
        public static async Task<IEnumerable<NovelModel>> GetLastUpdatedList(
            ListTypes listType,int off,int limit)
        {
            string url=string
                .Format(LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_LIST) 
                        as string);

            var tcs = new TaskCompletionSource<IEnumerable<NovelModel>>();
            
            RestRequest request = new RestRequest(url,Method.GET);
            request.AddParameter("type",listType.ToString());
            request.AddParameter("off", off);
            request.AddParameter("limit", limit);
            request.AddParameter("format","json");
            
            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetException(new Exception("LastestConnectionError"));
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<IEnumerable<NovelModel>>(result.Content);
                    tcs.SetResult(value);
                }
            });
            
            return await tcs.Task;
        }

        public static async Task<IEnumerable<NovelModel>> GetNovelListByGenre(string id,int off,int limit)
        {
            string url =string
                .Format(LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_GENRES) 
                    as string);

            var tcs = new TaskCompletionSource<IEnumerable<NovelModel>>();

            RestRequest request = new RestRequest(url, Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("off", off);
            request.AddParameter("limit", limit);
            request.AddParameter("format", "json");

            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetException(new Exception("GenreConnectionError"));
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<IEnumerable<NovelModel>>(result.Content);
                    tcs.SetResult(value);
                }
            });

            return await tcs.Task;
        }

        public static async Task<IEnumerable<NovelModel>> GetSearchNovelList(string keyword, int off, int limit)
        {
            string url =
                string.Format(LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_SEARCH) 
                    as string);

            var tcs = new TaskCompletionSource<IEnumerable<NovelModel>>();

            RestRequest request = new RestRequest(url, Method.GET);
            request.AddParameter("keyword", Uri.EscapeDataString(keyword));
            request.AddParameter("off", off);
            request.AddParameter("limit", limit);
            request.AddParameter("format", "json");

            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<IEnumerable<NovelModel>>(result.Content);
                    tcs.SetResult(value);
                }
            });

            return await tcs.Task;
        }

        public static async Task<NovelModel> GetInfoDetails(int ID)
        {
            string url =
                string.Format(LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_DETAIL) 
                    as string);

            var tcs = new TaskCompletionSource<NovelModel>();

            RestRequest request = new RestRequest(url, Method.GET);
            request.AddParameter("id", ID);
            request.AddParameter("format", "json");

            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<NovelModel>(result.Content);
                    tcs.SetResult(value);
                }
            });

            return await tcs.Task;
        }
        
        public static async Task<IEnumerable<ChapterModel>> GetChapterList(int nId)
        {
            string url =
                string.Format(LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_CHAPTERLIST) 
                as string);

            var tcs = new TaskCompletionSource<IEnumerable<ChapterModel>>();

            RestRequest request = new RestRequest(url, Method.GET);
            request.AddParameter("nid", nId);
            request.AddParameter("format", "json");

            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<IEnumerable<ChapterModel>>(result.Content);
                    tcs.SetResult(value);
                }
            });
            try
            {
                return await tcs.Task;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<ChapterModel> GetChapterContent(int cId)
        {
            string url =
                string.Format(
                    LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_CHAPTERDETAIL) 
                    as string
                );

            var tcs = new TaskCompletionSource<ChapterModel>();

            RestRequest request = new RestRequest(url, Method.GET);
            request.AddParameter("cid", cId);
            request.AddParameter("format", "json");

            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<ChapterModel>(result.Content);
                    tcs.SetResult(value);
                }
            });

            return await tcs.Task;
        }

        public static async Task<IEnumerable<GenreItemViewModel>> GetGenreList()
        {
            string url =
                string.Format(LocalSettings.GetNetworkConfig(LocalSettings.NetworkConfigParams.URL_NOVEL_GENRELIST) 
                as string);
            var tcs = new TaskCompletionSource<IEnumerable<GenreItemViewModel>>();

            RestRequest request = new RestRequest(url, Method.GET);
            request.AddParameter("lang", AppResources.lang);
            request.AddParameter("format", "json");

            _client.ExecuteAsync(request, result =>
            {
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = JsonConvert.DeserializeObject<Genres>(result.Content);
                    tcs.SetResult(value.Genre);
                }
            });

            return await tcs.Task;
        }
        
        class Genres
        {
            [JsonProperty("genres")]
            public List<GenreItemViewModel> Genre { get; private set; }
        }
    }
}