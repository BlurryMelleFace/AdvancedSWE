using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string clientId = "65030c7ddddc4cbe822c46c4277fe265";
        string clientSecret = "f8500ebc8b434ec98ebb0f525a02a55c";
        string accessToken = await GetAccessToken(clientId, clientSecret);
        Console.WriteLine("Access Token: " + accessToken);


        string trackId = "6rqhFgbbKwnb9MLmUQDhG6";
        await GetTrack(accessToken, trackId);
    }
    static async Task<string> GetAccessToken(string clientId, string clientSecret)
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
            string accessToken = jsonResponse.access_token;
            return accessToken;
        }
    }
    static async Task GetTrack(string accessToken, string trackId)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"https://api.spotify.com/v1/tracks/{trackId}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Track Details: " + responseString);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
    }

}
