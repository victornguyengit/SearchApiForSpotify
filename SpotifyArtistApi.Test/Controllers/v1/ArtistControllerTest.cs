using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyArtistApi.Controllers.v1;
using SpotifyArtistApi.Domain;
using SpotifyArtistApi.Models.v1;
using SpotifyArtistApi.Service.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace SpotifyArtistApi.Test.Controllers.v1
{
    public class ArtistControllerTest
    {
        private readonly ArtistController _testee;
        private readonly ArtistModel _artistModel;
        private readonly ArtistService _artistService;
        private readonly string _searchCriteria = "Katy Perry";

        public ArtistControllerTest()
        {
            var logger = A.Fake<ILogger>();
            var mapper = A.Fake<IMapper>();

            _testee = new ArtistController(logger, mapper, _artistService);

            _artistModel = new ArtistModel
            {
                Artist_id = "6jJ0s89eD6GaHleKKya26X",
                Artist_name = "Katy Perry"
            };

            var item = new Item
            {
                Id = "6jJ0s89eD6GaHleKKya26X",
                Name = "Katy Perry"
            };

            A.CallTo(() => _artistService.GetArtistByName(_searchCriteria)).Returns(item);
        }

 
    }
}
