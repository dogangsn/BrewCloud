using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Dtos.Settings
{
    public class Navigation
    {
        public Navigation()
        {
            Default = new List<RootNavigationItem>();
        }
        public List<RootNavigationItem> Default { get; set; }  
    }


    public class RootNavigationItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("children")]
        public List<NavigationItem> Children { get; set; }
    }

    public class NavigationItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        public List<NavigationItem> Children { get; set; }
    }


}
