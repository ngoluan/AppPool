﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace AppPool
{
    class UDPClient
    {
        public void send(string ip, string msg)
        {
            Boolean exception_thrown = false;
            #region comments
            // Create a socket object. This is the fundamental device used to network
            // communications. When creating this object we specify:
            // Internetwork: We use the internet communications protocol
            // Dgram: We use datagrams or broadcast to everyone rather than send to
            // a specific listener
            // UDP: the messages are to be formated as user datagram protocal.
            // The last two seem to be a bit redundant.
            #endregion
            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            ProtocolType.Udp);
            #region comments
            // create an address object and populate it with the IP address that we will use
            // in sending at data to. This particular address ends in 255 meaning we will send
            // to all devices whose address begins with 192.168.2.
            // However, objects of class IPAddress have other properties. In particular, the
            // property AddressFamily. Does this constructor examine the IP address being
            // passed in, determine that this is IPv4 and set the field. If so, the notes
            // in the help file should say so.
            #endregion
            IPAddress send_to_address = IPAddress.Parse(ip);
            #region comments
            // IPEndPoint appears (to me) to be a class defining the first or final data
            // object in the process of sending or receiving a communications packet. It
            // holds the address to send to or receive from and the port to be used. We create
            // this one using the address just built in the previous line, and adding in the
            // port number. As this will be a broadcase message, I don't know what role the
            // port number plays in this.
            #endregion
            IPEndPoint sending_end_point = new IPEndPoint(send_to_address, 11000);
            #region comments
            // The below three lines of code will not work. They appear to load
            // the variable broadcast_string witha broadcast address. But that
            // address causes an exception when performing the send.
            //
            //string broadcast_string = IPAddress.Broadcast.ToString();
            //Console.WriteLine("broadcast_string contains {0}", broadcast_string);
            //send_to_address = IPAddress.Parse(broadcast_string);
            #endregion

           
                
                    // the socket object must have an array of bytes to send.
                    // this loads the string entered by the user into an array of bytes.
                    byte[] send_buffer = Encoding.ASCII.GetBytes(msg);

                    // Remind the user of where this is going.
                    MessageBox.Show("sending to address: "+sending_end_point.Address.ToString()+" "+ sending_end_point.Port.ToString());
                    try
                    {
                        sending_socket.SendTo(send_buffer, sending_end_point);
                    }
                    catch (Exception send_exception)
                    {
                        MessageBox.Show(send_exception.ToString());
                    }


        } // end of main()
    }

}
