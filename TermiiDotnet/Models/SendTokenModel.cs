using Newtonsoft.Json;

namespace Termii.NET.Models
{
    public class SendTokenModel
    {
        [JsonProperty("pinId")]
        public string? PinId { get; set; }

        [JsonProperty("to")]
        public string? To { get; set; }

        [JsonProperty("smsStatus")]
        public string? SmsStatus { get; set; }
    }

}
