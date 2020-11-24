using System;
using System.Runtime.Serialization;

namespace SpotifyArtistApi.Domain
{
    [DataContract]  
    public class Item
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
