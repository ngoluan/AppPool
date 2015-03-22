using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AppPool
{
    class UDPListener
    {
        private const int listenPort = 11000;
        public static bool messageReceived = false;
        public UDPListener()
        {
            Thread oThread = new Thread(ReceiveMessages);
            oThread.Start();
        }


        public static void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("Received: {0}", receiveString);

            dynamic json = JsonConvert.DeserializeObject(receiveString);

            if (json.launchApp != null)
            {
                string launchApp = json.launchApp;
                Console.WriteLine(launchApp);
                Utilities utilities = new Utilities();
                utilities.OpenApp(launchApp);
            }
            
        }

        public static void ReceiveMessages()
        {
            //// Receive a message and write it to the console.
            IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
            UdpClient u = new UdpClient(e);

            UdpState s = new UdpState();
            s.e = e;
            s.u = u;
            Console.WriteLine("listening for messages");
            u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

            // Do some work while we wait for a message. For this example, 
            // we'll just sleep 
            while (!messageReceived)
            {
                Thread.Sleep(100);
            }
        }

    }
    public class UdpState
    {
        public IPEndPoint e = null;
        public UdpClient u = null;
    }  
}
