using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Threading.Tasks;
using Microsoft.Azure.Devices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlinkControlUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //Install the Azure Service SDK Nuget; Add this--> Microsoft.Azure.Devices

            //Copy and paste connectionstring from Azure Iot Hub Shared Policy--> Iothubbownwer
        const string connectionString = "HostName=<YOURHUBNAME.azure-devices.net>;SharedAccessKeyName=iothubowner;SharedAccessKey=<YOUR HUB KEY>";
        public MainPage()
        {
            this.InitializeComponent();
            AddDevices();
        }

        void AddDevices()
        {
            //Type your Devices Name instead
            RegistryManager manager = RegistryManager.CreateFromConnectionString(connectionString);
            manager.AddDeviceAsync(new Device("YOUR DEVICE NAME"));
        }

        private async Task TurnLight(bool isOn)
        {
            await SendCloudToDeviceMessageAsync(isOn);
        }

        private static async Task SendCloudToDeviceMessageAsync(bool isOn)
        {
            var serviceClient = ServiceClient.CreateFromConnectionString(connectionString, TransportType.Amqp);
            //var str = "Hello, Cloud, from a UWP app that uses Microsoft.Azure.Devices";
            var message = new Message(System.Text.Encoding.ASCII.GetBytes(isOn ? "on" : "off"));
            await serviceClient.SendAsync("YOUR DEVICE NAME", message);
        }

        private async void ToggleLedBtn_Checked(object sender, RoutedEventArgs e)
        {
            await TurnLight(true);
        }

        private async void ToggleLedBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            await TurnLight(false);
        }
    }
}
