using Newtonsoft.Json;
using System.Collections.Generic;

namespace r6random
{
    public class Gadget
    {
        [JsonProperty("gadget_name")]
        public string GadgetName { get; set; }
    }
}