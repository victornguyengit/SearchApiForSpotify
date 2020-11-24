using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyArtistApi.Domain
{
    public class Artist
    {
        public IEnumerable<Item> Items { get; set; }
    }
}
