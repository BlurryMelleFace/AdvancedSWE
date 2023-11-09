using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMDSpotifyClient
{
    class JSONSearchForItem
    {
        public static string JSON { get; set; }
        public static void ShowData()
        {
            var SONGS = JsonConvert.DeserializeObject<JSONResponses.Rootobject>(JSON);

            foreach (var i in SONGS.tracks.items)
            {
                Console.Clear();
                Console.WriteLine($"Name Of The Track: {i.name}");
                TimeSpan duration = TimeSpan.FromMilliseconds(i.duration_ms);
                string formattedDuration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
                Console.WriteLine($"Duration: {formattedDuration}");
                Console.WriteLine($"Explicit: {i._explicit}");
                Console.WriteLine($"Name of The Corresponding Album: {i.album.name}");
                Console.WriteLine($"Album Release Date: {i.album.release_date}");

                foreach (var ii in i.album.artists)
                {
                    Console.WriteLine($"Name Of Artist: {ii.name}");
                }
            }
        }
        
    }

    class SpotifyCredentials
    {
        private static string clientId { get; } = "65030c7ddddc4cbe822c46c4277fe265";
        private static string clientSecret { get; } = "f8500ebc8b434ec98ebb0f525a02a55c";
        public static string accessToken { get; set; }
        public static async Task GetAccessToken()
        {
            using (HttpClient client = new HttpClient())
            {
                string base64Auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to get the access token
                // Note: You should use a JSON parsing library for production code
                // For simplicity, this example assumes the response is in valid JSON format
                // and directly extracts the access token as a string.
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                accessToken = jsonResponse.access_token;
            }
        }
    }
    class GetInformation
    {
        public static async Task GetTrack(string accessToken, string trackId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/tracks/{trackId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }

        }
        public static async Task SearchForItem(string accessToken, string songName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={songName}&type=track&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONSearchForItem.JSON = responseString;

                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }

    }

    class Program
    {
        static async Task Main(string[] args)
        {

            await SpotifyCredentials.GetAccessToken();

            Console.WriteLine("Type in a Song you would like to Search for:");
            string songName = Console.ReadLine();
            
            await GetInformation.SearchForItem(SpotifyCredentials.accessToken, songName);
            JSONSearchForItem.ShowData();

            Console.WriteLine("\n");
            Console.WriteLine("| 1 | You want to get more Info of the Track");
            Console.WriteLine("| 2 | You want to search another track ");
            Console.WriteLine("| 3 | You Want to quit this application");
            string userInput = Console.ReadLine();

            if (userInput == "1")
            {

            }
            else if (userInput == "2")
            {
                Console.Clear();
                Console.WriteLine("Type in a Song you would like to Search for:");
                songName = Console.ReadLine();
                await GetInformation.SearchForItem(SpotifyCredentials.accessToken, songName);
                JSONSearchForItem.ShowData();
        }
            else if (userInput == "3")
            {
                System.Environment.Exit(0);
            }

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();


        }
    }

}