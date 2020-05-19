using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.Model
{
    public class LocalSettings
    {
        #region Properties
        public enum NetworkConfigParams
        {
            ROOT_URL,
            URL_NOVEL_LIST,
            URL_NOVEL_DETAIL,
            URL_NOVEL_CHAPTERLIST,
            URL_NOVEL_GENRELIST,
            URL_NOVEL_CHAPTERDETAIL,
            URL_NOVEL_GENRES,
            URL_NOVEL_SEARCH
        }

        public enum UserHistoryParams
        {
            CURRENT_CHAPTERLIST,
            CURRENT_CHAPTERID,
            CURRENT_GENRE,
            CURRENT_KEYWORD,
            CURRENT_NOVEL
        }
        #endregion

        #region methods

        public static void SetNetworkConfig(NetworkConfigParams key, object value)
        {
            SetNetworkConfig(key.ToString(), value);
        }
        public static void SetNetworkConfig(string key, object value)
        {
            SetValue(key, value);
        }
        public static object GetNetworkConfig(NetworkConfigParams key)
        {
            return GetValue(key.ToString());
        }
        
        public static void SetUserHistory(UserHistoryParams key, object value)
        {
            SetUserHistory(key.ToString(), value);
        }
        public static void SetUserHistory(string key, object value)
        {
            SetValue(key, value);
        }
        public static object GetUserHistory(UserHistoryParams key)
        {
            return GetValue(key.ToString());
        }

        public static object GetValue(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                return IsolatedStorageSettings.ApplicationSettings[key];
            else return null;
        }

        private static void SetValue(string key, object value)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(key, value);
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings[key]= value;
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
        

        #endregion
    }
}
