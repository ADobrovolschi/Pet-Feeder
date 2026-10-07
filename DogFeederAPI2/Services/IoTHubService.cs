using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System.Text;

namespace DogFeederAPI2.Services
{
    public class IoTHubService
    {
        private readonly ServiceClient _serviceClient;

        public IoTHubService(string connectionString)
        {
            _serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await _serviceClient.OpenAsync();
                Console.WriteLine("Connection to IoT Hub successful!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }
        public async Task SendTestMessageAsync()
        {
            try
            {
                var message = new Message(Encoding.ASCII.GetBytes("Test Message from Cata"));
                await _serviceClient.SendAsync("PetfeederV1", message);
                Console.WriteLine("Test message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send message: {ex.Message}");
                throw;
            }
        }

        public async Task SendFeedNowCommandAsync()
        {
            var message = new Message(Encoding.ASCII.GetBytes("FEED_NOW"));
            await _serviceClient.SendAsync("PetfeederV1", message);
            Console.WriteLine("Feed now command sent successfully!");
        }

    }
}
