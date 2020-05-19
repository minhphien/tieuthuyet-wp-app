using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Comic.Model
{
    [DataContract]
    public class NovelModel
    {
        
        [DataMember]
        [JsonProperty("id")]
        public int ID { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("author")]
        public string Author { get; set; }

        [DataMember]
        [JsonProperty("genres")]
        public string Genre { get; set; }

        [DataMember]
        [JsonProperty("image")]
        public string Image { get; set; }

        [DataMember]
        [JsonProperty("view")]
        public int Viewss { get; set; }

        [DataMember]
        [JsonProperty("hot")]
        public byte Hot { get; set; }

        [DataMember]
        [JsonProperty("update_date")]
        public string LastUpdate { get; set; }

        [DataMember]
        [JsonProperty("create_date")]
        public string Create { get; set; }

        [DataMember]
        [JsonProperty("description")]
        public string Descriptions { get; set; }
    }
}
