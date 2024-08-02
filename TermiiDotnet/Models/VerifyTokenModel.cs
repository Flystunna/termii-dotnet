using Newtonsoft.Json;

namespace Termii.NET.Models
{
    public class VerifyTokenModel
    {
        [JsonProperty("pinId")]
        public string? PinId { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("msisdn")]
        public string? Msisdn { get; set; }
    }
}
