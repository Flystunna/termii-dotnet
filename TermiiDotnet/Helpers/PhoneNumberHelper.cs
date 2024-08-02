using System;
using System.Collections.Generic;

namespace Termii.NET.Helpers
{
    internal static class PhoneNumberHelper
    {
        internal static List<string> CleanPhoneNumbers(List<string> phoneNumbers)
        {
            List<string> to = new List<string>();
            foreach (var phoneNumber in phoneNumbers)
            {
                var cleanedPhone = CleanPhoneNumber(phoneNumber);
                to.Add(cleanedPhone);
            }
            return to;
        }
        internal static string CleanPhoneNumber(string phoneNumber)
        {
            var cleanedPhone = string.Empty;
            var p = phoneNumber[0].ToString();
            if (string.Equals(p, "0")) cleanedPhone = "234" + phoneNumber.Substring(1, (phoneNumber.Length - 1));
            else cleanedPhone = phoneNumber;
            return cleanedPhone;
        }
    }
}
