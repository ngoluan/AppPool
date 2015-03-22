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

namespace AppPool
{
    public partial class RemoteViewer : Form
    {
        public string ip;
        public RemoteViewer(string appName, string invitation, string ip)
        {
            InitializeComponent();
            this.Text = appName;
            Connect(invitation, this.rdpViewer, "", "");
            this.ip = ip;
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
            UDPClient con = new UDPClient();
            con.send(ip, "test message");
        }
    }
}
