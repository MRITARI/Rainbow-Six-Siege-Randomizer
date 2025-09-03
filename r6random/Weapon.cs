using Newtonsoft.Json;
using System.Collections.Generic;

namespace r6random
{
    public class Weapon
    {
        [JsonProperty("weapon_name")]
        public string WeaponName { get; set; }
        [JsonProperty("weapon_type")]
        public string WeaponType { get; set; }
        [JsonProperty("grip")]
        public List<string> Grips { get; set; }
        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }
        [JsonProperty("attachments")]
        public List<string> Attachments { get; set; }
    }
}