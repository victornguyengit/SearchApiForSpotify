using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyArtistApi.Models.v1;
using SpotifyArtistApi.Service.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SpotifyArtistApi.Models;
using AutoMapper;

namespace SpotifyArtistApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ArtistController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IArtistService _artistService; 

        public ArtistController(ILogger logger, IMapper mapper, IArtistService artistService)
        {
            _logger = logger;
            _mapper = mapper;
            _artistService = artistService;
        }

        /// <summary>
        /// Action to get an artists name and id 
        /// </summary>
        /// <param name="searchCriteria">The name of the artist</param>
        /// <returns>Returns the artists id and name</returns>
        /// <response code="200">Returned if the artist was found</response>
        /// <response code="204">Returned if no artists were found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult> Artist(string searchCriteria)
        {   
            try
            {
                _logger.LogInformation($"Entering method: Artist with param {searchCriteria}");
                var item = await _artistService.GetArtistByName(searchCriteria);

                var artist = _mapper.Map<ArtistModel>(item);
                
                return Ok(artist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
