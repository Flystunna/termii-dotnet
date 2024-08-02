using Newtonsoft.Json;

namespace Termii.NET.Models
{
    public class GetBalanceModel
    {
        [JsonProperty("user")]
        public string? User { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }
    }
}
