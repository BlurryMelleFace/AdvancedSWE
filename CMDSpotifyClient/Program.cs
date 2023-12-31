﻿using System;
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
using System.ComponentModel.Design.Serialization;

namespace CMDSpotifyClient
{
    class JSONSearchInformation
    {
        public static string JSON { get; set; }

        public static string TrackID { get; set; }
        public static string AlbumID { get; set; }
        public static string ArtistID { get; set; }
        public static void DataSearchTrack()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);
            foreach (var i in deserialized.tracks.items)
            {
                TrackID = i.id;
            }
        }
        public static void DataSearchAlbum()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);
            foreach (var i in deserialized.albums.items)
            {
                AlbumID = i.id;
            }
        }
        public static void DataSearchArtist()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);
            foreach (var i in deserialized.artists.items)
            {
                ArtistID = i.id;
            }
        }
    }
    class JSONPlaylist
    {
        public static string JSON {  get; set; }

        public static string PlaylistID { get; set; }

        public static List<string> plalyistTracksListStringID = new List<string>();
        public static void DataSearchPlaylist()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(JSON);

            foreach (var i in deserialized.playlists.items)
            {
                PlaylistID = i.id;
            }
        }
        public static void DataPlaylist()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetPlaylist.Rootobject>(JSON);
            plalyistTracksListStringID.Clear();
            Console.Clear();
            Console.WriteLine("\n");
            var count = 1;
            foreach (var i in deserialized.tracks.items)
            {
                Console.WriteLine($"Track {count}:---------------------{i.track.name}");
                plalyistTracksListStringID.Add(i.track.id);
                count++;
            }
            Console.WriteLine("\n");
            Console.WriteLine($"Tacks in the Playlist: {deserialized.name}");
        }
    }
    class JSONArtist
    {
        public static string JSON { get; set; }

        public static List<string> artistAlbumListStringID = new List<string>();
        public static List<string> artistSinglesListStringID = new List<string>();
        public static List<string> artistRelatedArtistsListStringID = new List<string>();
        public static List<string> artistGenresListStringID = new List<string>();
        public static void DataArtist()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetArtists.Rootobject>(JSON);
            artistGenresListStringID.Clear();
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
                    artistGenresListStringID.Add(item);
                    count++;
                }
                Console.WriteLine("\n");
            }
        }
        public static void DataArtistSingles()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetArtistAlbums.Rootobject>(JSON);
            artistSinglesListStringID.Clear();
            Console.Clear();
            var count = 1;
            foreach (var i in deserialized.items)
            {
                if (i.total_tracks == 1)
                {
                    Console.WriteLine($"Album {count}:---------------------{i.release_date.Normalize()} | {i.name}");
                    artistSinglesListStringID.Add(i.id);
                    count++;
                }
            }
        }
        public static void DataArtistAlbums()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetArtistAlbums.Rootobject>(JSON);
            artistAlbumListStringID.Clear();
            Console.Clear();
            var count = 1;
            foreach (var i in deserialized.items)
            {
                if (i.total_tracks > 1)
                {
                    Console.WriteLine($"Album {count}:---------------------{i.release_date.Normalize()} | {i.name}");
                    artistAlbumListStringID.Add(i.id);
                    count++;
                }
            }
        }
        public static void DataArtistRelatedArtists()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetArtistRelatedArtists.Rootobject>(JSON);
            artistRelatedArtistsListStringID.Clear();
            Console.Clear();
            Console.WriteLine("Related Artists:");
            Console.WriteLine("\n");
            var count = 1;
            foreach (var i in deserialized.artists)
            {
                    Console.WriteLine($"Track {count}:---------------------{i.name}");
                    artistRelatedArtistsListStringID.Add(i.id);
                    count++;

            }
        }
    }
    class JSONAlbum
    {
        public static string JSON { get; set; }

        public static string AlbumName { get; set; }
        public static string LastArtistName { get; set; }

        public static List<string> trackListString = new List<string>();

        public static List<string> artistIDList = new List<string>();
        public static void DataAlbum()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetAlbum.Rootobject>(JSON);

            trackListString.Clear();
            artistIDList.Clear();

            AlbumName = deserialized.name;
            Console.Clear();
            Console.WriteLine($"Album Name:-----------------{deserialized.name}");
            Console.WriteLine($"Album Release Date:---------{deserialized.release_date}");
            Console.WriteLine($"Album Label:----------------{deserialized.label}");
            Console.WriteLine($"Album Popularity:-----------{deserialized.popularity}");
            var count = 1;
            foreach (var i in deserialized.artists)
            {
                Console.WriteLine($"Album Artist {count}:--------------{i.name}");
                artistIDList.Add(i.id);
                LastArtistName = i.name;
                count++;
            }
            Console.WriteLine("\n");
            count = 1;  
            foreach (var i in deserialized.tracks.items)
            {
                Console.WriteLine($"Track {count}:--------------------{i.name}");
                trackListString.Add(i.id);
                count++;
                

            }
        }

    }
    class JSONTrack
    {
        public static string JSON { get; set; }

        public static List<string> artistSringID = new List<string>();
        public static void DataTrack()
        {
            artistSringID.Clear();
            Console.Clear();
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetTrack.Rootobject>(JSON);
            JSONSearchInformation.TrackID = deserialized.id;
            Console.WriteLine($"Name Of The Track:---------------------{deserialized.name}");
            Console.WriteLine($"\n");
            var count = 1;
            foreach (var i in deserialized.artists)
            {
                Console.WriteLine($"Name Of Artist {count}:----------------------{i.name}");
                count++;
                artistSringID.Add(i.id);
            }
            
            TimeSpan duration = TimeSpan.FromMilliseconds(deserialized.duration_ms);
            string formattedDuration = $"{duration.Minutes:D2}:{duration.Seconds:D2}";
            Console.WriteLine($"Duration:------------------------------{formattedDuration}");
            Console.WriteLine($"Explicit:------------------------------{deserialized._explicit}");
            Console.WriteLine($"Track Popularity:----------------------{deserialized.popularity}");
            Console.WriteLine($"Name Of The Corresponding Album:-------{deserialized.album.name}");
            Console.WriteLine($"Album Release Date:--------------------{deserialized.album.release_date}");
            Console.WriteLine($"Album Total Tracks:--------------------{deserialized.album.total_tracks}");
            Console.WriteLine($"Album Track Position:------------------{deserialized.track_number}");
            }
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
        public static void DataAdditionalTrackInfo()
        {
            var deserialized = JsonConvert.DeserializeObject<JSONResponses.GetAdditionalTrackInfo.Rootobject>(JSON);

            Console.Clear();
            Console.WriteLine($"Track ID:------------------------------{deserialized.id}");
            Console.WriteLine($"Track Key:-----------------------------{deserialized.key}");
            Console.WriteLine($"Track Tempo:---------------------------{deserialized.tempo} BPM");
            Console.WriteLine($"Track Instrumentalness:----------------{deserialized.instrumentalness}");
            Console.WriteLine($"Track Acousticness:--------------------{deserialized.acousticness}");
            Console.WriteLine($"Track Speechiness:---------------------{deserialized.speechiness}");
            Console.WriteLine($"Track Danceability:--------------------{deserialized.danceability}");
            Console.WriteLine($"Track Energy:--------------------------{deserialized.energy}");
            Console.WriteLine($"Track Liveness:------------------------{deserialized.liveness}");
            Console.WriteLine($"Track Loudness:------------------------|{deserialized.loudness} dB");
            Console.WriteLine($"Track Time Signature:------------------{deserialized.time_signature}");
            Console.WriteLine($"Track Valence:-------------------------{deserialized.valence}");
           

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
    class GetInformationFromAPI
    {
        public static async Task SearchTrack(string accessToken, string songName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={songName}&type=track&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONSearchInformation.JSON = responseString;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }
        public static async Task SearchAlbum(string accessToken, string albumName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={albumName}&type=album&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONSearchInformation.JSON = responseString;

                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }
        public static async Task SearchArtist(string accessToken, string artistName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={artistName}&type=artist&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONSearchInformation.JSON = responseString;

                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }
        public static async Task SearchGenrePlaylist(string accessToken, string genreName) 
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/search?q={genreName}&type=playlist&limit=1");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONPlaylist.JSON = responseString;

                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }

        }


        public static async Task GetAdditionalTrackInfo(string accessToken, string trackId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/audio-features/{trackId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONTrack.JSON = responseString;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }

        }
        public static async Task GetTrack(string accessToken, string trackId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/tracks/{trackId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    JSONTrack.JSON = responseString;
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }

        }
        public static async Task GetArtists(string accessToken, string artistID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/artists?ids={artistID}");

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
        public static async Task GetArtistsAlbumsAndSingles(string accessToken, string artistID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/artists/{artistID}/albums?limit=30");

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
        public static async Task GetAlbum(string accessToken, string albumID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/albums/{albumID}");

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
        public static async Task GetArtistRelatedArtists(string accessToken, string artistID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/artists/{artistID}/related-artists");

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
        public static async Task GetPlaylist(string accessToken, string playlistID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"https://api.spotify.com/v1/playlists/{playlistID}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JSONPlaylist.JSON = responseString;
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
        public static async Task MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello and Welcome to Moritz's Spotify Client, What do you want to do?");
            Console.WriteLine("\n");
            Console.WriteLine("| 1 | Search for a Song");
            Console.WriteLine("| 2 | Search for an Album");
            Console.WriteLine("| 3 | Search for an Artist");
            Console.WriteLine("| 4 | Search for a Genre");
            Console.WriteLine("\n");
            Console.WriteLine("| 5 | Quit this application");

            while (true)
            {
                var userInputSelection = Console.ReadLine();

                if (userInputSelection == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Type in a Song you would like to Search for:");
                    await SearchTrackMenu(Console.ReadLine());
                    await MainMenu();
                    break;
                }
                else if (userInputSelection == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Type in an Album you would like to Search for:");
                    await SearchAlbumMenu(Console.ReadLine());
                    await MainMenu();
                    break;
                }
                else if (userInputSelection == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Type in an Artist you would like to search for:");
                    await SearchArtistMenu(Console.ReadLine());
                    await MainMenu();

                    break;
                }
                else if (userInputSelection == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Type in an Genre you would like to search for:");
                    await SearchPlaylist(Console.ReadLine());
                    await MainMenu();

                    break;
                }
                else if (userInputSelection == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3");
                }
            }

        }
            public static async Task SearchTrackMenu(string trackName)
            {
                Console.Clear();
                await GetInformationFromAPI.SearchTrack(SpotifyCredentials.accessToken, trackName);
                JSONSearchInformation.DataSearchTrack();
                await TrackMenu(JSONSearchInformation.TrackID);
            }
            public static async Task SearchAlbumMenu(string albumName)
            {
                Console.Clear();
                await GetInformationFromAPI.SearchAlbum(SpotifyCredentials.accessToken, albumName);
                JSONSearchInformation.DataSearchAlbum();
                await AlbumMenu(JSONSearchInformation.AlbumID);
            }
            public static async Task SearchArtistMenu(string artistName)
            {
                Console.Clear();
                await GetInformationFromAPI.SearchArtist(SpotifyCredentials.accessToken, artistName);
                JSONSearchInformation.DataSearchArtist();
                await ArtistMenu(JSONSearchInformation.ArtistID);
            }
            public static async Task SearchPlaylist(string genreName)
            {
                Console.Clear();
                await GetInformationFromAPI.SearchGenrePlaylist(SpotifyCredentials.accessToken, genreName);
                JSONPlaylist.DataSearchPlaylist();
                await PlaylistMenu(JSONPlaylist.PlaylistID);
            }
               
                public static async Task TrackMenu(string trackID)
                {
                    Console.Clear();

                    await GetInformationFromAPI.GetTrack(SpotifyCredentials.accessToken, trackID);
                    JSONTrack.DataTrack();
                    Console.WriteLine("\n");

                    Console.WriteLine("| 1 | Go Back");
                    Console.WriteLine("| 2 | Playback");
                    Console.WriteLine("| 3 | Search the Artist(s)");
                    Console.WriteLine("| 4 | Search another track");
                    Console.WriteLine("| 5 | Search for the Album");
                    Console.WriteLine("| 6 | Get more Comprehensive Info on the Track");

                    while (true)
                    {
                        string userInput = Console.ReadLine();

                        if (userInput == "1")
                        {
                            break;
                        }
                        else if (userInput == "2")
                        {
                            await GetInformationFromAPI.GetTrack(SpotifyCredentials.accessToken, JSONSearchInformation.TrackID);
                            JSONTrack.DataPlayTrack();
                            await TrackMenu(trackID);
                            break;
                        }
                        else if (userInput == "3")
                        {
                            int index;

                            if (JSONTrack.artistSringID.Count > 1)
                            {
                                do
                                {
                                    Console.WriteLine("Type in the Artist Index");

                                } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONTrack.artistSringID.Count);

                                await ArtistMenu(JSONTrack.artistSringID[index - 1]);
                            }
                            else
                            {
                                await ArtistMenu(JSONTrack.artistSringID[0]);
                            }
                            await TrackMenu(trackID);
                            break;
                        }
                        else if (userInput == "4")
                        {
                            Console.Clear();
                            Console.WriteLine("Type in a new Song you would like to Search for:");
                            var newTrackName = Console.ReadLine();

                            await TrackMenu(trackID);
                            break;
                        }
                        else if (userInput == "5")
                        {
                            await AlbumMenu(JSONSearchInformation.AlbumID);
                            await TrackMenu(trackID);
                            break;
                        }
                        else if (userInput == "6")
                        {
                            await AdditionalTrackInfo(JSONSearchInformation.TrackID);
                            await TrackMenu(trackID);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                        }
                    }
                }
                public static async Task AlbumMenu(string albumID)
                {
                    Console.Clear();

                    await GetInformationFromAPI.GetAlbum(SpotifyCredentials.accessToken, albumID);
                    JSONAlbum.DataAlbum();

                    Console.WriteLine("\n");
                    Console.WriteLine("| 1 | Go Back");
                    Console.WriteLine("| 2 | Visit a Song in the Album");
                    Console.WriteLine("| 3 | Visit the Artist(s)");
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

                            await TrackMenu(JSONAlbum.trackListString[index - 1]);
                            await AlbumMenu(albumID);
                            break;
                        }
                        else if (userInput == "3")
                        {
                            int index2;

                            do
                            {
                                Console.WriteLine("Type in the Artist Index");

                            } while (!int.TryParse(Console.ReadLine(), out index2) || index2 < 1 || index2 > JSONAlbum.artistIDList.Count);

                            await ArtistMenu(JSONAlbum.artistIDList[index2 - 1]);
                            await AlbumMenu(albumID);
                            break;
                            
                        }

                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a Number between 1 and 2");
                        }
                    }
                }
                public static async Task ArtistMenu(string artistID)
                {
                    Console.Clear();

                    await GetInformationFromAPI.GetArtists(SpotifyCredentials.accessToken, artistID);
                    JSONArtist.DataArtist();

                    Console.WriteLine("\n");
                    Console.WriteLine("| 1 | Go Back");
                    Console.WriteLine("| 2 | Search released Albums (no Singles) from the Artist");
                    Console.WriteLine("| 3 | Search released Albums (Singles) from the Artist");
                    Console.WriteLine("| 4 | Search related Artists");
                    Console.WriteLine("| 5 | Search Songs Of Listed Genres");

                    while (true)
                    {
                        string userInput = Console.ReadLine();

                        if (userInput == "1")
                        {
                            break;
                        }
                        if (userInput == "2")
                        {
                            await GetArtistAlbums(artistID);
                            await ArtistMenu(artistID);
                            break;
                        }
                        if (userInput == "3")
                        {
                            await GetArtistSingles(artistID);
                            await ArtistMenu(artistID);
                            break;
                        }
                        if (userInput == "4")
                        {
                            await GetArtistRelatedArtists(artistID);
                            await ArtistMenu(artistID);
                            break;
                        }
                        if (userInput == "5")
                        {
                            int index;
                                
                            do
                            {
                                Console.WriteLine("Type in the Genre Index");

                            } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONArtist.artistGenresListStringID.Count);

                            await SearchPlaylist(JSONArtist.artistGenresListStringID[index-1]);
                            await ArtistMenu(artistID);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input Please enter a number 1.");
                        }
                    }
                }
                public static async Task PlaylistMenu(string playlistID)
                {
                    Console.Clear();

                    await GetInformationFromAPI.GetPlaylist(SpotifyCredentials.accessToken, playlistID);
                    JSONPlaylist.DataPlaylist();

                    Console.WriteLine("\n");
                    Console.WriteLine("| 1 | Go Back");
                    Console.WriteLine("| 2 | Visit a Specific Track");
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
                                Console.WriteLine("Type in the Track Index");

                            } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONPlaylist.plalyistTracksListStringID.Count);

                            await TrackMenu(JSONPlaylist.plalyistTracksListStringID[index - 1]);
                            await PlaylistMenu(playlistID);
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a Number between 1 and 2");
                        }
                    }
                }

                    public static async Task AdditionalTrackInfo(string trackID)
                    {
                        Console.Clear();

                        await GetInformationFromAPI.GetAdditionalTrackInfo(SpotifyCredentials.accessToken, trackID);
                        JSONTrack.DataAdditionalTrackInfo();

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
                                Console.WriteLine("Invalid input. Please enter 1");
                            }
                        }
                    }
                    public static async Task GetArtistAlbums(string artistID)
                    {
                        Console.Clear();

                        await GetInformationFromAPI.GetArtistsAlbumsAndSingles(SpotifyCredentials.accessToken, artistID);
                        JSONArtist.DataArtistAlbums(); 

                        Console.WriteLine("\n");
                        Console.WriteLine("| 1 | Go Back");
                        Console.WriteLine("| 2 | Visit a Specific Album");
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
                                    Console.WriteLine("Type in the Album Index");

                                } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONArtist.artistAlbumListStringID.Count);

                                await AlbumMenu(JSONArtist.artistAlbumListStringID[index - 1]);
                                await GetArtistAlbums(artistID);
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a Number between 1 and 2");
                            }
                        }
                    }
                    public static async Task GetArtistSingles(string artistID)
                    {
                        Console.Clear();

                        await GetInformationFromAPI.GetArtistsAlbumsAndSingles(SpotifyCredentials.accessToken, artistID);
                        JSONArtist.DataArtistSingles();

                        Console.WriteLine("\n");
                        Console.WriteLine("| 1 | Go Back");
                        Console.WriteLine("| 2 | Visit a Specific Album");
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
                                    Console.WriteLine("Type in the Single Index");

                                } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONArtist.artistSinglesListStringID.Count);

                                await AlbumMenu(JSONArtist.artistSinglesListStringID[index - 1]);
                                await GetArtistAlbums(artistID);
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a Number between 1 and 2");
                            }
                        }
                    }
                    public static async Task GetArtistRelatedArtists(string artistID)
                    {
                        Console.Clear();

                        await GetInformationFromAPI.GetArtistRelatedArtists(SpotifyCredentials.accessToken, artistID);
                        JSONArtist.DataArtistRelatedArtists();

                        Console.WriteLine("\n");
                        Console.WriteLine("| 1 | Go Back");
                        Console.WriteLine("| 2 | Visit a Specific Artist");
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
                                    Console.WriteLine("Type in the Single Index");

                                } while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > JSONArtist.artistRelatedArtistsListStringID.Count);

                                await ArtistMenu(JSONArtist.artistRelatedArtistsListStringID[index - 1]);
                                await GetArtistRelatedArtists(artistID);
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a Number between 1 and 2");
                            }
                        }
                    }
    }
    class Program
    {   
        static async Task Main(string[] args)
        {

            await SpotifyCredentials.GetAccessToken();

            await SpotifyClient.MainMenu();

        }
    }
}
//Simple Collapse: Ctrl  M,O
//Collapse To Classes: Ctrl + M,A & Ctrl + M,E
//Decollapse Ctrl: + M,P