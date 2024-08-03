# Termii.NET

This utility library is designed to make integrating with [Termii](https://developers.termii.com/) easier using C#

## Usage

To download from [Nuget.org](https://www.nuget.org/packages/Termii.NET/)

```C#
dotnet add package Termii.NET 
```

The library has one classes `TermiiService`.
TermiiService exposes APIs directly integrated to TERMII's API.

Firstly, you have to initialize TermiiService:

```C#
var messaging = new TermiiService("{YOUR_API_KEY}", "{YOUR_SENDER_ID}");
```

### GetBalance

[Balance API](https://developers.termii.com/balance)

This method returns your total balance and balance information from your wallet, such as currency.

Here is how to use this method:

```C#
var balance = await messaging.GetBalance();
Console.WriteLine(JsonConvert.SerializeObject(balance));
```

```JSON
{
    "user": "Tayo Joel",
    "balance": 0,
    "currency": "NGN"
}
```

### SearchPhoneNumber

[Search API](https://developers.termii.com/search)

This method allows businesses verify phone numbers and automatically detect their status as well as current network. It also tells if the number has activated the do-not-disturb settings

Here is how to use this method:

```C#
var searchNumber = await messaging.SearchPhoneNumber("07089509657");
Console.WriteLine(JsonConvert.SerializeObject(searchNumber));
```

```JSON
{
      "number": "2347089509657",
      "status": "DND blacklisted",
      "network": "Airtel Nigeria",
      "network_code": "62120"
} 
```

### GetHistory

[History API](https://developers.termii.com/history)

This method returns reports for messages sent across the sms, voice & whatsapp channels. Reports can either display all messages on termii or a single message.

Here is how to use this method:

```C#
var history = await messaging.GetHistory();
Console.WriteLine(JsonConvert.SerializeObject(history));
```

```JSON
    {
        "data": [
            {
                "sender": "SecureM",
                "receiver": "23499990111",
                "country_code": "234",
                "message": "This is your sign in code 189165",
                "amount": 1,
                "reroute": 0,
                "status": "Delivered",
                "sms_type": "plain",
                "send_by": "api",
                "media_url": null,
                "message_id": "10202408333113000220182700000094918",
                "notify_url": "https://google.com",
                "notify_id": null,
                "created_at": "2024-08-02T19:18:27Z",
                "sent_at": "2024-08-02T19:18:27Z"
            }    
        ],
        "links": {
            "first": "http://api.ng.termii.com/api/sms/inbox?page=1",
            "last": "http://api.ng.termii.com/api/sms/inbox?page=9",
            "prev": null,
            "next": "http://api.ng.termii.com/api/sms/inbox?page=2"
        },
        "meta": {
            "current_page": 1,
            "from": 1,
            "last_page": 9,
            "path": "http://api.ng.termii.com/api/sms/inbox",
            "per_page": 15,
            "to": 15,
            "total": 125
        }
    }
```

You can also use this method by parsing in a specific messageId. This will return details for the messageId alone.

```C#
var history = await messaging.GetHistory("10202408333113000220182700000094918");
Console.WriteLine(JsonConvert.SerializeObject(history));
```

```JSON
{
    "data": [
        {
            "sender": "SecureM",
            "receiver": "23499990111",
            "country_code": "234",
            "message": "This is your sign in code 189165",
            "amount": 1,
            "reroute": 0,
            "status": "Delivered",
            "sms_type": "plain",
            "send_by": "api",
            "media_url": null,
            "message_id": "10202408333113000220182700000094918",
            "notify_url": "https://dashboard.getconvoy.io/ingest/6FMau5w7seS3Swjs",
            "notify_id": null,
            "created_at": "2024-08-02T19:18:27Z",
            "sent_at": "2024-08-02T19:18:27Z"
        }
    ],
    "links": {
        "first": "http://api.ng.termii.com/api/sms/inbox?page=1",
        "last": "http://api.ng.termii.com/api/sms/inbox?page=1",
        "prev": null,
        "next": null
    },
    "meta": {
        "current_page": 1,
        "from": 1,
        "last_page": 1,
        "path": "http://api.ng.termii.com/api/sms/inbox",
        "per_page": 15,
        "to": 1,
        "total": 1
    }
}
```

### SendSms

[Messaging API](https://developers.termii.com/messaging-api)

This method allows businesses send text messages to their customers across different messaging channels. 
It takes the text and an array of phone numbers (max of 100 phone numbers).

Messaging Channels/Routes

generic: This channel is used to send promotional messages and messages to phone number not on dnd
dnd: On this channel all your messages deliver whether there is dnd restriction or not on the phone number
whatsapp: This channel sends messages via WhatsApp

```C#
var sendSms = await messaging.SendSms("This is to notify you", ["08136644812"]);
Console.WriteLine(JsonConvert.SerializeObject(sendSms));
```

```JSON
{
    "message_id": "9122821270554876574",
    "message": "Successfully Sent",
    "balance": 9,
    "user": "Peter Mcleish"
}
```


### SendBulkSms

[Messaging API](https://developers.termii.com/messaging-api)

This method allows businesses send text messages to their customers across different messaging channels. 
It takes the text and an array of phone numbers (max of 10,000 phone numbers).

Messaging Channels/Routes

generic: This channel is used to send promotional messages and messages to phone number not on dnd
dnd: On this channel all your messages deliver whether there is dnd restriction or not on the phone number
whatsapp: This channel sends messages via WhatsApp

```C#
var sendSms = await messaging.SendSms("This is to bulk notify you", ["08136644812"]);
Console.WriteLine(JsonConvert.SerializeObject(sendSms));
```

```JSON
{
    "code": "ok",
    "message_id": "9122821270554876574",
    "message": "Successfully Sent",
    "balance": 9,
    "user": "Peter Mcleish"
}
```


### SendToken

[Send Token API](https://developers.termii.com/send-token)

The send token method allows businesses trigger one-time-passwords (OTP) across any available messaging channel on Termii. 
One-time-passwords created are generated randomly and there's an option to set an expiry time.
Parameters expected are message_text, phoneNumber, pin_placeholder.
Optional parameters include channel, message_type, pin_attempts, pin_time_to_live, pin_length.

```C#
var sendToken = await messaging.SendToken("This is your sign in code <code>", "08136644812", "<code>");
Console.WriteLine(JsonConvert.SerializeObject(sendToken));
```

```JSON
 {
    "pinId": "29ae67c2-c8e1-4165-8a51-8d3d7c298081",
    "to": "2348136644812",
    "smsStatus": "Message Sent"
  }
```


### VerifyToken

[Verify Token API](https://developers.termii.com/verify-token)

The verify token method checks tokens sent to customers and returns a response confirming the status of the token. 
A token can either be confirmed as verified or expired based on the timer set for the token.
Parameters expected are pin_id and pin.

```C#
var verifyToken = await messaging.VerifyToken("1a116587-2e19-4858-80df-f6638427ddeb", "189165");
Console.WriteLine(JsonConvert.SerializeObject(verifyToken));
```

```JSON
   {
     "pinId": "c8dcd048-5e7f-4347-8c89-4470c3af0b",
     "verified": "True",
     "msisdn": "2348109077743"
   }
```