using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDSpotifyClient
{
    public class JSONResponses
    {
        public class SearchForItem
        {
            public class Rootobject
            {
                public Tracks tracks { get; set; }
                public Artists artists { get; set; }
                public Albums albums { get; set; }
                public Playlists playlists { get; set; }
                public Shows shows { get; set; }
                public Episodes episodes { get; set; }
                public Audiobooks audiobooks { get; set; }
            }

            public class Tracks
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item[] items { get; set; }
            }

            public class Item
            {
                public Album album { get; set; }
                public Artist1[] artists { get; set; }
                public string[] available_markets { get; set; }
                public int disc_number { get; set; }
                public int duration_ms { get; set; }
                public bool _explicit { get; set; }
                public External_Ids external_ids { get; set; }
                public External_Urls2 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public bool is_playable { get; set; }
                public Linked_From linked_from { get; set; }
                public Restrictions1 restrictions { get; set; }
                public string name { get; set; }
                public int popularity { get; set; }
                public string preview_url { get; set; }
                public int track_number { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public bool is_local { get; set; }
            }

            public class Album
            {
                public string album_type { get; set; }
                public int total_tracks { get; set; }
                public string[] available_markets { get; set; }
                public External_Urls external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image[] images { get; set; }
                public string name { get; set; }
                public string release_date { get; set; }
                public string release_date_precision { get; set; }
                public Restrictions restrictions { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public Artist[] artists { get; set; }
            }

            public class External_Urls
            {
                public string spotify { get; set; }
            }

            public class Restrictions
            {
                public string reason { get; set; }
            }

            public class Image
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Artist
            {
                public External_Urls1 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls1
            {
                public string spotify { get; set; }
            }

            public class External_Ids
            {
                public string isrc { get; set; }
                public string ean { get; set; }
                public string upc { get; set; }
            }

            public class External_Urls2
            {
                public string spotify { get; set; }
            }

            public class Linked_From
            {
            }

            public class Restrictions1
            {
                public string reason { get; set; }
            }

            public class Artist1
            {
                public External_Urls3 external_urls { get; set; }
                public Followers followers { get; set; }
                public string[] genres { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image1[] images { get; set; }
                public string name { get; set; }
                public int popularity { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls3
            {
                public string spotify { get; set; }
            }

            public class Followers
            {
                public string href { get; set; }
                public int total { get; set; }
            }

            public class Image1
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Artists
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item1[] items { get; set; }
            }

            public class Item1
            {
                public External_Urls4 external_urls { get; set; }
                public Followers1 followers { get; set; }
                public string[] genres { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image2[] images { get; set; }
                public string name { get; set; }
                public int popularity { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls4
            {
                public string spotify { get; set; }
            }

            public class Followers1
            {
                public string href { get; set; }
                public int total { get; set; }
            }

            public class Image2
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Albums
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item2[] items { get; set; }
            }

            public class Item2
            {
                public string album_type { get; set; }
                public int total_tracks { get; set; }
                public string[] available_markets { get; set; }
                public External_Urls5 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image3[] images { get; set; }
                public string name { get; set; }
                public string release_date { get; set; }
                public string release_date_precision { get; set; }
                public Restrictions2 restrictions { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public Artist2[] artists { get; set; }
            }

            public class External_Urls5
            {
                public string spotify { get; set; }
            }

            public class Restrictions2
            {
                public string reason { get; set; }
            }

            public class Image3
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Artist2
            {
                public External_Urls6 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls6
            {
                public string spotify { get; set; }
            }

            public class Playlists
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item3[] items { get; set; }
            }

            public class Item3
            {
                public bool collaborative { get; set; }
                public string description { get; set; }
                public External_Urls7 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image4[] images { get; set; }
                public string name { get; set; }
                public Owner owner { get; set; }
                public bool _public { get; set; }
                public string snapshot_id { get; set; }
                public Tracks1 tracks { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls7
            {
                public string spotify { get; set; }
            }

            public class Owner
            {
                public External_Urls8 external_urls { get; set; }
                public Followers2 followers { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public string display_name { get; set; }
            }

            public class External_Urls8
            {
                public string spotify { get; set; }
            }

            public class Followers2
            {
                public string href { get; set; }
                public int total { get; set; }
            }

            public class Tracks1
            {
                public string href { get; set; }
                public int total { get; set; }
            }

            public class Image4
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Shows
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item4[] items { get; set; }
            }

            public class Item4
            {
                public string[] available_markets { get; set; }
                public Copyright[] copyrights { get; set; }
                public string description { get; set; }
                public string html_description { get; set; }
                public bool _explicit { get; set; }
                public External_Urls9 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image5[] images { get; set; }
                public bool is_externally_hosted { get; set; }
                public string[] languages { get; set; }
                public string media_type { get; set; }
                public string name { get; set; }
                public string publisher { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public int total_episodes { get; set; }
            }

            public class External_Urls9
            {
                public string spotify { get; set; }
            }

            public class Copyright
            {
                public string text { get; set; }
                public string type { get; set; }
            }

            public class Image5
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Episodes
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item5[] items { get; set; }
            }

            public class Item5
            {
                public string audio_preview_url { get; set; }
                public string description { get; set; }
                public string html_description { get; set; }
                public int duration_ms { get; set; }
                public bool _explicit { get; set; }
                public External_Urls10 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image6[] images { get; set; }
                public bool is_externally_hosted { get; set; }
                public bool is_playable { get; set; }
                public string language { get; set; }
                public string[] languages { get; set; }
                public string name { get; set; }
                public string release_date { get; set; }
                public string release_date_precision { get; set; }
                public Resume_Point resume_point { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public Restrictions3 restrictions { get; set; }
            }

            public class External_Urls10
            {
                public string spotify { get; set; }
            }

            public class Resume_Point
            {
                public bool fully_played { get; set; }
                public int resume_position_ms { get; set; }
            }

            public class Restrictions3
            {
                public string reason { get; set; }
            }

            public class Image6
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Audiobooks
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item6[] items { get; set; }
            }

            public class Item6
            {
                public Author[] authors { get; set; }
                public string[] available_markets { get; set; }
                public Copyright1[] copyrights { get; set; }
                public string description { get; set; }
                public string html_description { get; set; }
                public string edition { get; set; }
                public bool _explicit { get; set; }
                public External_Urls11 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image7[] images { get; set; }
                public string[] languages { get; set; }
                public string media_type { get; set; }
                public string name { get; set; }
                public Narrator[] narrators { get; set; }
                public string publisher { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public int total_chapters { get; set; }
            }

            public class External_Urls11
            {
                public string spotify { get; set; }
            }

            public class Author
            {
                public string name { get; set; }
            }

            public class Copyright1
            {
                public string text { get; set; }
                public string type { get; set; }
            }

            public class Image7
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Narrator
            {
                public string name { get; set; }
            }

        }

        public class GetTrack
        {
            public class Rootobject
            {
                public Album album { get; set; }
                public Artist1[] artists { get; set; }
                public string[] available_markets { get; set; }
                public int disc_number { get; set; }
                public int duration_ms { get; set; }
                public bool _explicit { get; set; }
                public External_Ids external_ids { get; set; }
                public External_Urls2 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public bool is_playable { get; set; }
                public Linked_From linked_from { get; set; }
                public Restrictions1 restrictions { get; set; }
                public string name { get; set; }
                public int popularity { get; set; }
                public string preview_url { get; set; }
                public int track_number { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public bool is_local { get; set; }
            }

            public class Album
            {
                public string album_type { get; set; }
                public int total_tracks { get; set; }
                public string[] available_markets { get; set; }
                public External_Urls external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image[] images { get; set; }
                public string name { get; set; }
                public string release_date { get; set; }
                public string release_date_precision { get; set; }
                public Restrictions restrictions { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public Artist[] artists { get; set; }
            }

            public class External_Urls
            {
                public string spotify { get; set; }
            }

            public class Restrictions
            {
                public string reason { get; set; }
            }

            public class Image
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Artist
            {
                public External_Urls1 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls1
            {
                public string spotify { get; set; }
            }

            public class External_Ids
            {
                public string isrc { get; set; }
                public string ean { get; set; }
                public string upc { get; set; }
            }

            public class External_Urls2
            {
                public string spotify { get; set; }
            }

            public class Linked_From
            {
            }

            public class Restrictions1
            {
                public string reason { get; set; }
            }

            public class Artist1
            {
                public External_Urls3 external_urls { get; set; }
                public Followers followers { get; set; }
                public string[] genres { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image1[] images { get; set; }
                public string name { get; set; }
                public int popularity { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls3
            {
                public string spotify { get; set; }
            }

            public class Followers
            {
                public string href { get; set; }
                public int total { get; set; }
            }

            public class Image1
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

        }

        public class GetArtists
        {

            public class Rootobject
            {
                public Artist[] artists { get; set; }
            }

            public class Artist
            {
                public External_Urls external_urls { get; set; }
                public Followers followers { get; set; }
                public string[] genres { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image[] images { get; set; }
                public string name { get; set; }
                public int popularity { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls
            {
                public string spotify { get; set; }
            }

            public class Followers
            {
                public string href { get; set; }
                public int total { get; set; }
            }

            public class Image
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

        }

        public class GetAlbum
        {

            public class Rootobject
            {
                public string album_type { get; set; }
                public int total_tracks { get; set; }
                public string[] available_markets { get; set; }
                public External_Urls external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public Image[] images { get; set; }
                public string name { get; set; }
                public string release_date { get; set; }
                public string release_date_precision { get; set; }
                public Restrictions restrictions { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public Artist1[] artists { get; set; }
                public Tracks tracks { get; set; }
                public Copyright[] copyrights { get; set; }
                public External_Ids external_ids { get; set; }
                public string[] genres { get; set; }
                public string label { get; set; }
                public int popularity { get; set; }
            }

            public class External_Urls
            {
                public string spotify { get; set; }
            }

            public class Restrictions
            {
                public string reason { get; set; }
            }

            public class Tracks
            {
                public string href { get; set; }
                public int limit { get; set; }
                public string next { get; set; }
                public int offset { get; set; }
                public string previous { get; set; }
                public int total { get; set; }
                public Item[] items { get; set; }
            }

            public class Item
            {
                public Artist[] artists { get; set; }
                public string[] available_markets { get; set; }
                public int disc_number { get; set; }
                public int duration_ms { get; set; }
                public bool _explicit { get; set; }
                public External_Urls1 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public bool is_playable { get; set; }
                public Linked_From linked_from { get; set; }
                public Restrictions1 restrictions { get; set; }
                public string name { get; set; }
                public string preview_url { get; set; }
                public int track_number { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public bool is_local { get; set; }
            }

            public class External_Urls1
            {
                public string spotify { get; set; }
            }

            public class Linked_From
            {
                public External_Urls2 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls2
            {
                public string spotify { get; set; }
            }

            public class Restrictions1
            {
                public string reason { get; set; }
            }

            public class Artist
            {
                public External_Urls3 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls3
            {
                public string spotify { get; set; }
            }

            public class External_Ids
            {
                public string isrc { get; set; }
                public string ean { get; set; }
                public string upc { get; set; }
            }

            public class Image
            {
                public string url { get; set; }
                public int height { get; set; }
                public int width { get; set; }
            }

            public class Artist1
            {
                public External_Urls4 external_urls { get; set; }
                public string href { get; set; }
                public string id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
            }

            public class External_Urls4
            {
                public string spotify { get; set; }
            }

            public class Copyright
            {
                public string text { get; set; }
                public string type { get; set; }
            }

        }

        public class GetAdditionalTrackInfo
        {

            public class Rootobject
            {
                public float acousticness { get; set; }
                public string analysis_url { get; set; }
                public float danceability { get; set; }
                public int duration_ms { get; set; }
                public float energy { get; set; }
                public string id { get; set; }
                public float instrumentalness { get; set; }
                public int key { get; set; }
                public float liveness { get; set; }
                public float loudness { get; set; }
                public int mode { get; set; }
                public float speechiness { get; set; }
                public float tempo { get; set; }
                public int time_signature { get; set; }
                public string track_href { get; set; }
                public string type { get; set; }
                public string uri { get; set; }
                public float valence { get; set; }
            }

        }
    }
}