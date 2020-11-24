using SpotifyArtistApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyArtistApi.Data.v1
{
    public interface IGetArtist
    {
        Task<SpotifyModel> ReturnArtistByName(string name);
    }
}
