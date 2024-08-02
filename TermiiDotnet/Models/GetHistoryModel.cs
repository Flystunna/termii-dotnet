using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Termii.NET.Models
{
    public class GetHistoryDataModel
    {
        [JsonProperty("sender")]
        public string? Sender { get; set; }

        [JsonProperty("receiver")]
        public string? Receiver { get; set; }

        [JsonProperty("country_code")]
        public string? CountryCode { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("reroute")]
        public int Reroute { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("sms_type")]
        public string? SmsType { get; set; }

        [JsonProperty("send_by")]
        public string? SendBy { get; set; }

        [JsonProperty("media_url")]
        public object? MediaUrl { get; set; }

        [JsonProperty("message_id")]
        public string? MessageId { get; set; }

        [JsonProperty("notify_url")]
        public string? NotifyUrl { get; set; }

        [JsonProperty("notify_id")]
        public object? NotifyId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("sent_at")]
        public DateTime SentAt { get; set; }
    }

    public class GetHistoryLinkModel
    {
        [JsonProperty("first")]
        public string? First { get; set; }

        [JsonProperty("last")]
        public string? Last { get; set; }

        [JsonProperty("prev")]
        public object? Prev { get; set; }

        [JsonProperty("next")]
        public object? Next { get; set; }
    }

    public class GetHistoryMetaModel
    {
        [JsonProperty("current_page")]
        public int? CurrentPage { get; set; }

        [JsonProperty("from")]
        public int? From { get; set; }

        [JsonProperty("last_page")]
        public int? LastPage { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class GetHistoryModel
    {
        [JsonProperty("data")]
        public List<GetHistoryDataModel>? Data { get; set; }

        [JsonProperty("links")]
        public GetHistoryLinkModel? Links { get; set; }

        [JsonProperty("meta")]
        public GetHistoryMetaModel? Meta { get; set; }
    }

}
