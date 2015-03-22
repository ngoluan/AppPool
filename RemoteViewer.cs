using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxRDPCOMAPILib;
using Newtonsoft.Json;

namespace AppPool
{
    public partial class RemoteViewer : Form
    {
        public string ip;
        public string appName;
        UDPClient con;
        public RemoteViewer(string appName, string invitation, string ip)
        {
            InitializeComponent();

            this.appName = appName;
            this.Text = appName;
            this.ip = ip;

            Connect(invitation, this.rdpViewer, "", "");
            
            con = new UDPClient();

            sendAppOpenRequest(this.appName);
        }
        public void sendAppOpenRequest(string appName)
        {
            IDictionary<string, string> message = new Dictionary<string, string>();
            message["launchApp"] = appName;

            string json = JsonConvert.SerializeObject(message);
            con.send(ip, json);
        }

        public static void Connect(string invitation, AxRDPViewer display, string userName, string password)
        {
            display.Connect(invitation, userName, password);
        }

        public static void disconnect(AxRDPViewer display)
        {
            display.Disconnect();
        }

        private void viewer_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            con.send(ip, "test message");
        }
    }
}
