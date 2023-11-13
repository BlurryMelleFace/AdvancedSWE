using System;
using System.Collections.Generic;
using System.Media;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CMDSpotifyClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 
using System.Net;
using NAudio.Wave;
using System.Data;
using System.Runtime.InteropServices;
using System.Globalization;

namespace CMDSpotifyClient
{
    class JSONSearchForItem
    {
        public static string JSON { get; set; }
        public static string TrackID { get; set; }
        public static void ShowData()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);

            foreach (var i in deserialized.tracks.items)
            {
                TrackID = i.id;
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
    class JSONGetTrack
    {
        public static string JSON { get; set; }
        public static void ShowData() 
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetTrack.Rootobject>(JSON);

            string audioUrl = deserialized.preview_url;

            try
            {
                using (var webStream = new MediaFoundationReader(audioUrl))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(webStream);
                    outputDevice.Play();

                    Console.WriteLine("Press any key to stop playback or go back to Track Search");
                    Console.ReadKey();

                    outputDevice.Stop();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

                    JSONGetTrack.JSON = responseString;
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

    class SpotifyClient
    {
        public static async Task Selection()
        {
            Console.Clear();
            Console.WriteLine("Hello and Welchome to the Spotify Client, What do you want to do?");
            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Search for a Song");
            Console.WriteLine("| 2 | View Your Playlist");
            Console.WriteLine("| 3 | Quit this application");

            var userInputSelection = Console.ReadLine();  

            if (userInputSelection == "1")
            {
                await Choise1();
            }
            else if (userInputSelection == "2")
            {
                Console.Clear();

            }
            else if (userInputSelection == "3")
            {
                Console.Clear();


             
            }

        }

        public static async Task Choise1()
        {
            Console.Clear();

            Console.WriteLine("Type in a Song you would like to Search for:");
            string songName = Console.ReadLine();

            await GetInformation.SearchForItem(SpotifyCredentials.accessToken, songName);
            JSONSearchForItem.ShowData();

            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Playback");
            Console.WriteLine("| 2 | Search another track");
            Console.WriteLine("| 3 | Go Back to Selection");
            string userInput = Console.ReadLine();

            if (userInput == "1")
            {
                await GetInformation.GetTrack(SpotifyCredentials.accessToken, JSONSearchForItem.TrackID);
                JSONGetTrack.ShowData();

                await SpotifyClient.Choise1();

            }
            else if (userInput == "2")
            {
                await Choise1();
            }
            else if (userInput == "3")
            {


                await SpotifyClient.Selection();
            }


        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {

            await SpotifyCredentials.GetAccessToken();

            await SpotifyClient.Selection();

        }
    }
}