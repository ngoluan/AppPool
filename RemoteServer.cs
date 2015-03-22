using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using RDPCOMAPILib;

namespace AppPool
{
    class RemoteServer
    {
        public static RDPSession currentSession = null;
        public void startSession()
        {
            currentSession = new RDPSession();
            Connect(currentSession);
            logSession(getConnectionString(currentSession,
                "Test", "Group", "", 5));
        }
        private async void logSession(string invitation)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "invitation", invitation}
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("http://apppool.local-motion.ca/createSession.php", content);

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Server Response: " + responseString);
            }
        }
        public static void Connect(RDPSession session)
        {
            session.OnAttendeeConnected += Incoming;
            session.Open();
        }

        public static void Disconnect(RDPSession session)
        {
            session.Close();
        }
        public static string getConnectionString(RDPSession session, String authString,
            string group, string password, int clientLimit)
        {
            IRDPSRAPIInvitation invitation =
                session.Invitations.CreateInvitation
                (authString, group, password, clientLimit);
            return invitation.ConnectionString;
        }

        private static void Incoming(object Guest)
        {
            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;
        }
    }
    
}
