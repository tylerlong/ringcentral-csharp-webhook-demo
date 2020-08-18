using System;
using System.Threading.Tasks;
using RingCentral;

namespace setup_webhook
{
    class Program
    {
        private static string RINGCENTRAL_CLIENT_ID = Environment.GetEnvironmentVariable("RINGCENTRAL_CLIENT_ID");
        private static string RINGCENTRAL_CLIENT_SECRET = Environment.GetEnvironmentVariable("RINGCENTRAL_CLIENT_SECRET");
        private static string RINGCENTRAL_SERVER_URL = Environment.GetEnvironmentVariable("RINGCENTRAL_SERVER_URL");
        private static string RINGCENTRAL_USERNAME = Environment.GetEnvironmentVariable("RINGCENTRAL_USERNAME");
        private static string RINGCENTRAL_EXTENSION = Environment.GetEnvironmentVariable("RINGCENTRAL_EXTENSION");
        private static string RINGCENTRAL_PASSWORD = Environment.GetEnvironmentVariable("RINGCENTRAL_PASSWORD");

        private const string RINGCENTRAL_WEBHOOK_ADDRESS = "https://4db0953a1d01.ngrok.io/webhook";

        static async Task Main(string[] args)
        {
            var rc = new RestClient(RINGCENTRAL_CLIENT_ID, RINGCENTRAL_CLIENT_SECRET, RINGCENTRAL_SERVER_URL);
            await rc.Authorize(RINGCENTRAL_USERNAME, RINGCENTRAL_EXTENSION, RINGCENTRAL_PASSWORD);
            await rc.Restapi().Subscription().Post(new CreateSubscriptionRequest
            {
                eventFilters = new[] {"/restapi/v1.0/account/~/extension/~/message-store/instant?type=SMS"},
                deliveryMode = new NotificationDeliveryModeRequest
                {
                    transportType = "WebHook",
                    address = RINGCENTRAL_WEBHOOK_ADDRESS
                }
            });
            Console.WriteLine("WebHook ready!");
        }
    }
}