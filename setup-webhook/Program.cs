using System;
using System.Threading.Tasks;
using RingCentral;

namespace setup_webhook
{
    class Program
    {
        private const string RINGCENTRAL_CLIENT_ID = "<RINGCENTRAL_CLIENT_ID>";
        private const string RINGCENTRAL_CLIENT_SECRET = "<RINGCENTRAL_CLIENT_SECRET>";
        private const string RINGCENTRAL_SERVER_URL = "https://platform.devtest.ringcentral.com";
        private const string RINGCENTRAL_USERNAME = "";
        private const string RINGCENTRAL_EXTENSION = "";
        private const string RINGCENTRAL_PASSWORD = "";
        private const string RINGCENTRAL_WEBHOOK_ADDRESS = "";
        
        static async Task Main(string[] args)
        {
            var rc = new RestClient(RINGCENTRAL_CLIENT_ID, RINGCENTRAL_CLIENT_SECRET, RINGCENTRAL_SERVER_URL);
            await rc.Authorize(RINGCENTRAL_USERNAME, RINGCENTRAL_EXTENSION, RINGCENTRAL_PASSWORD);
            await rc.Restapi().Subscription().Post(new CreateSubscriptionRequest
            {
                eventFilters = new []{ "/restapi/v1.0/account/~/extension/~/message-store/instant?type=SMS" },
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
