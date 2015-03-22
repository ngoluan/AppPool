using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace AppPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Utilities utilities = new Utilities();
            utilities.OpenApp("GIMP");
        }

        private void Server_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF15D82F"));
            UDPListener udpListener = new UDPListener();
            RemoteServer server = new RemoteServer();
            server.startSession();
        }

        private void launchApp_Click(object sender, RoutedEventArgs e)
        {
            string appName = "";
            Button button = (Button)sender;
            if (button.Name == "arcgisBtn")
            {
                appName = "ArcGIS";
            }
            else if (button.Name == "gimpBtn")
            {
                appName = "GIMP";
            }
            else if (button.Name == "stataBtn")
            {
                appName = "Stata";
            }
            getInvitation(appName);

        }
        private async void getInvitation(string appName)
        {
            using (var client = new HttpClient())
            {


                var response = await client.PostAsync("http://apppool.local-motion.ca/getSession.php", null);

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Server Response: " + responseString);

                dynamic session = JsonConvert.DeserializeObject(responseString);

                string invitation = session.invitation;
                string ip = session.ip;
                RemoteViewer viewer = new RemoteViewer(appName, invitation, ip);
                viewer.ShowDialog();
                this.Close();
            }
        }
    }
}
