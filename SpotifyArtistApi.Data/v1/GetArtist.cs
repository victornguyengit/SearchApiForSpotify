using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpotifyArtistApi.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyArtistApi.Data.v1
{
    public class GetArtist : IGetArtist
    {
        public GetArtist()
        {
        }

        private static async Task<AccessToken> GetToken()
        {
            string clientId = Secrets.Configuration["Authorization:SpotifyClientId"];
            string clientSecret = Secrets.Configuration["Authorization:SpotifySecretKey"];
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            using (var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }

        private string BuildSpotifySearchUrl(string name)
        {
            return $"https://api.spotify.com/v1/search" +
                   $"?q=%22{name}%22" +
                   $"&type=artist";
        }

        public async Task<SpotifyModel> ReturnArtistByName(string name)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();
                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token.access_token);

                var url = new Uri(BuildSpotifySearchUrl(name));

                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                return JsonConvert.DeserializeObject<SpotifyModel>(json);
            }
        }
    }
}
