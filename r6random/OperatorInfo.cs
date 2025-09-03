using System.Collections.Generic;
using Newtonsoft.Json;

namespace r6random
{

    public class OperatorInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("weapons")]
        public List<Weapon> Weapons { get; set; }
        [JsonProperty("gadgets")]
        public List<Gadget> Gadgets { get; set; }

        public bool Enabled { get; set; } = true;
        public string ImagePath => $@"Res\{Role}s\{Name}.png";
        public string IconPath => $@"Res\{Role}s\{Name}_ICON.png";
    }
}
