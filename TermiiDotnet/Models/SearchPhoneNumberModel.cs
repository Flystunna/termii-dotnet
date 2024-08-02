using Newtonsoft.Json;

namespace Termii.NET.Models
{
    public class SearchPhoneNumberModel
    {
        [JsonProperty("number")]
        public string? Number { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("dnd_active")]
        public bool DndActive { get; set; }

        [JsonProperty("network")]
        public string? Network { get; set; }

        [JsonProperty("network_code")]
        public string? NetworkCode { get; set; }
    }

}
