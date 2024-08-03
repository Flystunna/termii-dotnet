using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Termii.NET.Helpers;
using Termii.NET.Models;

namespace Termii.NET.Messaging
{
    public class TermiiService
    {
        private string ApiKey { get; set; }
        private string SenderId { get; set; }

        private readonly string BaseUrl = "https://api.ng.termii.com/api/";

        private Utilities _utilities;
        public TermiiService(string apiKey, string senderId)
        {
            var httpClientFactory = new SimpleHttpClientFactory();
            _utilities = new Utilities(httpClientFactory);

            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            SenderId = senderId ?? throw new ArgumentNullException(nameof(senderId));
        }

        /// <summary>
        /// The Balance API returns your total balance and balance information from your wallet, such as currency.
        /// </summary>
        /// <returns></returns>
        public async Task<GetBalanceModel?> GetBalance()
        {
            var response = await _utilities.MakeHttpRequest(new StringContent(string.Empty, Encoding.UTF8, "application/json"), BaseUrl, $"get-balance?api_key={ApiKey}", HttpMethod.Get);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<GetBalanceModel>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// The search API allows businesses verify phone numbers and automatically detect their status as well as current network. It also tells if the number has activated the do-not-disturb settings.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<SearchPhoneNumberModel?> SearchPhoneNumber(string phoneNumber)
        {
            var response = await _utilities.MakeHttpRequest(new StringContent(string.Empty, Encoding.UTF8, "application/json"), BaseUrl, $"check/dnd?api_key={ApiKey}&phone_number={PhoneNumberHelper.CleanPhoneNumber(phoneNumber)}", HttpMethod.Get);
            return JsonConvert.DeserializeObject<SearchPhoneNumberModel>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// This Inbox API returns reports for messages sent across the sms, voice &amp; whatsapp channels. <br/>
        /// Reports can either display all messages on termii or a single message. <br/>
        /// You can query a single message by adding message_id as query param and passing the message_id as value. <br/>
        /// Find the Message ID in the JSON response of Send Message Endpoint All reports on your account will be returned as response if the message_id is not present in the request. <br/>
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<GetHistoryModel?> GetHistory(string? messageId = null)
        {
            HttpResponseMessage? response;
            if(!string.IsNullOrEmpty(messageId))
                response = await _utilities.MakeHttpRequest(new StringContent(string.Empty, Encoding.UTF8, "application/json"), BaseUrl, $"sms/inbox?api_key={ApiKey}&message_id={messageId}", HttpMethod.Get);
            else 
                response = await _utilities.MakeHttpRequest(new StringContent(string.Empty, Encoding.UTF8, "application/json"), BaseUrl, $"sms/inbox?api_key={ApiKey}", HttpMethod.Get);

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<GetHistoryModel>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// This API allows businesses send text messages to their customers across different messaging channels. The API accepts JSON request payload and returns JSON encoded responses, and uses standard HTTP response codes. <br/>
        /// generic: This channel is used to send promotional messages and messages to phone number not on dnd <br/>
        /// dnd: On this channel all your messages deliver whether there is dnd restriction or not on the phone number <br/>
        /// whatsapp: This channel sends messages via WhatsApp
        /// </summary>
        /// <param name="sms"></param>
        /// <param name="phoneNumbers"></param>
        /// <param name="channel"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public async Task<SendSmsModel?> SendSms(string sms, List<string> phoneNumbers, string channel = "generic", string type = "plain")
        {
            if (phoneNumbers.Count > 100) throw new Exception("A maximum of 100 PhoneNumbers is allowed");
            List<string> to = PhoneNumberHelper.CleanPhoneNumbers(phoneNumbers);

            var payload = new
            {
                to,
                sms,
                channel,
                type,
                from = SenderId,
                api_key = ApiKey
            };

            var response = await _utilities.MakeHttpRequest(payload, BaseUrl, "sms/send", HttpMethod.Post);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<SendSmsModel>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// This API allows businesses send bulk text messages to their customers across different messaging channels. The API accepts JSON request payload and returns JSON encoded responses, and uses standard HTTP response codes. <br/>
        /// generic: This channel is used to send promotional messages and messages to phone number not on dnd <br/>
        /// dnd: On this channel all your messages deliver whether there is dnd restriction or not on the phone number <br/>
        /// whatsapp: This channel sends messages via WhatsApp
        /// </summary>
        /// <param name="sms"></param>
        /// <param name="phoneNumbers"></param>
        /// <param name="channel"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SendSmsModel?> SendBulkSms(string sms, List<string> phoneNumbers, string channel = "generic", string type = "plain")
        {
            if (phoneNumbers.Count > 10000) throw new Exception("A maximum of 10,000 PhoneNumbers is allowed");
            List<string> to = PhoneNumberHelper.CleanPhoneNumbers(phoneNumbers);

            var payload = new
            {
                to,
                sms,
                channel,
                type,
                from = SenderId,
                api_key = ApiKey
            };
            var response = await _utilities.MakeHttpRequest(payload, BaseUrl, "sms/send/bulk", HttpMethod.Post);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<SendSmsModel>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// The send token API allows businesses trigger one-time-passwords (OTP) across any available messaging channel on Termii. One-time-passwords created are generated randomly and there's an option to set an expiry time.
        /// </summary>
        /// <param name="message_text"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="pin_placeholder"></param>
        /// <param name="message_type"></param>
        /// <param name="channel"></param>
        /// <param name="pin_attempts"></param>
        /// <param name="pin_time_to_live"></param>
        /// <param name="pin_length"></param>
        /// <returns></returns>
        public async Task<SendTokenModel?> SendToken(string message_text, string phoneNumber, string pin_placeholder, string channel = "generic", string message_type = "NUMERIC", int pin_attempts = 10, int pin_time_to_live = 5, int pin_length = 6)
        {
            string to = PhoneNumberHelper.CleanPhoneNumber(phoneNumber);

            var payload = new
            {
                message_text,
                to,
                pin_placeholder,
                message_type,
                pin_attempts,
                pin_time_to_live,
                pin_length,
                channel,
                from = SenderId,
                api_key = ApiKey
            };

            var response = await _utilities.MakeHttpRequest(payload, BaseUrl, "sms/otp/send", HttpMethod.Post);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<SendTokenModel>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Verify token API, checks tokens sent to customers and returns a response confirming the status of the token. A token can either be confirmed as verified or expired based on the timer set for the token.
        /// </summary>
        /// <param name="pin_id"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public async Task<VerifyTokenModel?> VerifyToken(string pin_id, string pin)
        {
            var payload = new { pin_id, pin, api_key = ApiKey };

            var response = await _utilities.MakeHttpRequest(payload, BaseUrl, "sms/otp/verify", HttpMethod.Post);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<VerifyTokenModel>(await response.Content.ReadAsStringAsync());
        }

    }
}
