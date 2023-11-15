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
using System.Threading;

namespace CMDSpotifyClient
{
    class JSONTrackInformation
    {
        public static string JSON { get; set; }
        public static string TrackID { get; set; }
        public static string AlbumID { get; set; }

        public static List<string> artistSring = new List<string>();
        public static string joinedArtists { get; set; }
        public static void DataTrackInformation()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);
            artistSring.Clear();
            foreach (var i in deserialized.tracks.items)
            {
                TrackID = i.id;
                Console.Clear();
                Console.WriteLine($"Name Of The Track:---------------------{i.name}");
                var count = 1;
                foreach (var ii in i.album.artists)
                {
                    Console.WriteLine($"Name Of Artist {count}:----------------------{ii.name}");
                    count++;
                    artistSring.Add(ii.id);
                }
                AlbumID = i.album.id;
                joinedArtists = string.Join(",", artistSring);
                TimeSpan duration = TimeSpan.FromMilliseconds(i.duration_ms);
                string formattedDuration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
                Console.WriteLine($"Duration:------------------------------{formattedDuration}");
                Console.WriteLine($"Explicit:------------------------------{i._explicit}");
                Console.WriteLine($"Track Popularity:----------------------{i.popularity}");
                Console.WriteLine($"Name Of The Corresponding Album:-------{i.album.name}");
                Console.WriteLine($"Album Release Date:--------------------{i.album.release_date}");
                Console.WriteLine($"Album Total Tracks:--------------------{i.album.total_tracks}");
                Console.WriteLine($"Album Track Position:------------------{i.track_number}");

            }
        }
        public static void DataAlbumID()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);
            foreach (var i in deserialized.albums.items)
            {
                AlbumID = i.id;
            }
        }

    }
    class JSONArtist
    {
        public static string JSON { get; set; }
        public static void DataArtist()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetArtists.Rootobject>(JSON);

            Console.Clear();
            foreach (var i in deserialized.artists)
            {
                Console.WriteLine($"Name Of The Artist:--------------------{i.name}");
                Console.WriteLine($"Id Of The Artist:----------------------{i.id}");
                Console.WriteLine($"Number of Followers:-------------------{i.followers.total}");
                Console.WriteLine($"Popularity of the Artist:--------------{i.popularity}");

                var count = 1;
                foreach (var item in i.genres)
                {
                    Console.WriteLine($"Genre {count}:---------------------{item}");
                    count++;
                }
                Console.WriteLine("\n");
            }
        }

    }
    class JSONAlbum
    {
        public static string JSON { get; set; }
        public static List<string> trackListString = new List<string>();
        public static string AlbumName { get; set; }
        public static string Artist { get; set; }
        public static void DataAlbum()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetAlbum.Rootobject>(JSON);
            trackListString.Clear();    
            AlbumName = deserialized.name;
            Console.Clear();
            Console.WriteLine($"Album Name:-----------------{deserialized.name}");
            Console.WriteLine($"Album Release Date:---------{deserialized.release_date}");
            Console.WriteLine($"Album Label:----------------{deserialized.label}");
            Console.WriteLine($"Album Popularity:-----------{deserialized.popularity}");
            Console.WriteLine("\n");
            var count = 1;
            foreach (var i in deserialized.artists)
            {
                Artist = i.name;
            }
            foreach (var i in deserialized.tracks.items)
            {
                Console.WriteLine($"Track {count}:--------------------{i.name}");
                trackListString.Add(i.name);
                count++;
                

            }
        }

    }
    class JSONPlayTrack
    {
        public static string JSON { get; set; }
        public static void DataPlayTrack()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetTrack.Rootobject>(JSON);

            string audioUrl = deserialized.preview_url;
            try
            {
                if (audioUrl == null)
                {

                    Console.WriteLine("Audio URL is null.");
                    Console.ReadLine();
                }

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
        public static async Task TrackInformation(string accessToken, string songName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={songName}&type=track&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONTrackInformation.JSON = responseString;

                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }
        public static async Task AlbumID(string accessToken, string albumName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={albumName}&type=album&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONTrackInformation.JSON = responseString;

                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }
        public static async Task PlayTrack(string accessToken, string trackId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/tracks/{trackId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONPlayTrack.JSON = responseString;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }

        }
        public static async Task GetArtists(string accessToken, string artistString)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/artists?ids={artistString}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JSONArtist.JSON = responseString;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }

        }
        public static async Task GetAlbum(string accessToken, string albumString)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/albums/{albumString}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JSONAlbum.JSON = responseString;
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
            Console.WriteLine("Hello and Welcome to Moritz's Spotify Client, What do you want to do?");
            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Search for a Song");
            Console.WriteLine("| 2 | Search for an Album");
            Console.WriteLine("| 3 | Quit this application");

            while (true)
            {
                var userInputSelection = Console.ReadLine();

                if (userInputSelection == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Type in a Song you would like to Search for:");
                    string songName = Console.ReadLine();
                    await SearchTrack(songName);
                    await Selection();
                    break;
                }
                else if (userInputSelection == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Type in an Album you would like to Search for:");
                    string albumName = Console.ReadLine();
                    await SearchAlbumViaID(albumName);
                    await Selection();
                    break;
                }
                else if (userInputSelection == "3")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3");
                }
            }

        }
        public static async Task SearchTrack(string songName)
        {
            Console.Clear();
            await GetInformation.TrackInformation(SpotifyCredentials.accessToken, songName);
            JSONTrackInformation.DataTrackInformation();

            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Go Back");
            Console.WriteLine("| 2 | Playback");
            Console.WriteLine("| 3 | Search the Artist");
            Console.WriteLine("| 4 | Search another track");
            Console.WriteLine("| 5 | Search for the Album");


            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    break;
                }
                else if (userInput == "2")
                {
                    await GetInformation.PlayTrack(SpotifyCredentials.accessToken, JSONTrackInformation.TrackID);
                    JSONPlayTrack.DataPlayTrack();
                    await SearchTrack(songName);
                    break;
                }
                else if (userInput == "3")
                {
                    await SearchArtist(JSONTrackInformation.joinedArtists);
                    await SearchTrack(songName);
                    break;
                }
                else if (userInput == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Type in a new Song you would like to Search for:");
                    var newSongName = Console.ReadLine();

                    await SearchTrack(newSongName);
                    break;
                }
                else if (userInput == "5")
                {
                    await SearchAlbum(JSONTrackInformation.AlbumID);
                    await SearchTrack(songName);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                }
            }
        }
        public static async Task SearchArtist(string artistString)
        {
            Console.Clear();

            await GetInformation.GetArtists(SpotifyCredentials.accessToken, artistString);
            JSONArtist.DataArtist();

            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Go Back");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input Please enter a number 1.");
                }
            }
        }
        public static async Task SearchAlbum(string albumName)
        {
            Console.Clear();

            await GetInformation.GetAlbum(SpotifyCredentials.accessToken, albumName);
            JSONAlbum.DataAlbum();

            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Go Back");
            Console.WriteLine("| 2 | Visit a Song in the Album");
            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    break;
                }

                else if (userInput == "2")
                {
                    int index;

                    do
                    {
                        Console.WriteLine("Type in the Song Index");

                    } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONAlbum.trackListString.Count);

                    var indexSongName = JSONAlbum.trackListString[index - 1];
                    var songName = $"{JSONAlbum.Artist} {JSONAlbum.AlbumName} {indexSongName}";

                    await SearchTrack(songName);
                    await SearchAlbum(albumName);
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please enter a Number between 1 and 2");
                }
            }
        }
        public static async Task SearchAlbumViaID(string albumName)
        {
            Console.Clear();
            await GetInformation.AlbumID(SpotifyCredentials.accessToken, albumName);
            JSONTrackInformation.DataAlbumID();
            var albumString = JSONTrackInformation.AlbumID;
            await SearchAlbum(albumString);
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