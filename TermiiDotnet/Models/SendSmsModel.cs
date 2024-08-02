using Newtonsoft.Json;

namespace Termii.NET.Models
{
    public class SendSmsModel
    {
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("message_id")]
        public string? MessageId { get; set; }

        [JsonProperty("message_id_str")]
        public string? MessageIdStr { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }
    }
}
