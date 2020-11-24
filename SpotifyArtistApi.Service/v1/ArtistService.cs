using SpotifyArtistApi.Domain;
using SpotifyArtistApi.Data.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace SpotifyArtistApi.Service.v1
{
    public class ArtistService : IArtistService
    {
        private readonly ILogger _logger;
        private readonly IGetArtist _getArtist;

        public ArtistService(ILogger<ArtistService> logger, IGetArtist getArtist)
        {
            _logger = logger;
            _getArtist = getArtist;
        }

        public string FormatInput(string input) 
        {
            return Regex.Replace(input.ToLower(), "[ .]+", "");
        }

        public async Task<Item> GetArtistByName(string searchCriteria)
        {
            try
            {
                _logger.LogInformation($"Entering method: GetArtistByName with param {searchCriteria}");
                var result = await _getArtist.ReturnArtistByName(FormatInput(searchCriteria));

                return result.Artists.Items.FirstOrDefault(x => FormatInput(x.Name) == FormatInput(searchCriteria));
            }
            catch (Exception ex)
            {   
                _logger.LogError($"Something went wrong: {ex}");
                throw;
            }
        }
    }
}
