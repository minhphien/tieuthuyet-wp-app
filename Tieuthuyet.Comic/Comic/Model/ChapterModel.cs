using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Comic.Model
{
    [DataContract]
    public class ChapterModel
    {
        [DataMember]
        [JsonProperty("id")]
        public int ID { get;  set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get;  set; }

        [DataMember]
        [JsonProperty("update_date")]
        public string LastUpdate { get;  set; }

        [DataMember]
        [JsonProperty("create_date")]
        public string Create { get;  set; }

        [DataMember]
        [JsonProperty("content")]
        public string Content { get;  set; }
    }
}
