using SpotifyArtistApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyArtistApi.Service.v1
{
    public interface IArtistService
    {
        Task<Item> GetArtistByName(string name);
    }
}
