using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace ChildCare
{
    public class TwilioSMS
    {

        public void SendSMS(string messageText, string phoneNumber)
        {
            //TEST CREDENTIALS
            //string accountSid = "AC2bbe1b78d57d4a73899b6f0a63d758cf"; // Your Account SID from www.twilio.com/console
            //string authToken = "dc9bf3662853dc5f43895d40f4cd477f";  // Your Auth Token from www.twilio.com/console

            //PRODUCTION CREDENTIALS
            string accountSid = "AC60d3e2ed31e25f0f12396fdf40247dfa"; // Your Account SID from www.twilio.com/console
            string authToken = "03008d4e6bad20d2cc4c488922d220c6";  // Your Auth Token from www.twilio.com/console


            TwilioRestClient twilio = new TwilioRestClient(accountSid, authToken);
            var message = twilio.SendMessage(
                "+14144556685", // From (Replace with your Twilio number)
                "+1" + phoneNumber,
                //"+14147585380", // To (Replace with your phone number)
                messageText
                );

            if (message.RestException != null)
            {
                var error = message.RestException.Message;
                Console.WriteLine(error);
            }
        }

}
}