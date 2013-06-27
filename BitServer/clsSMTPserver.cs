using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BitServer
{
    public delegate void SMTPincommingHandler(TcpClient c);

    public class SMTPserver
    {
        public event SMTPincommingHandler SMTPincomming;

        private TcpListener server;
        private IAsyncResult IA;

        public SMTPserver(int port)
        {
            server = new TcpListener(IPAddress.Loopback, port);
            server.Start();
            IA = server.BeginAcceptTcpClient(new AsyncCallback(conIn), null);
        }

        ~SMTPserver()
        {
            server.EndAcceptTcpClient(IA);
            server.Stop();
        }

        public void Dispose()
        {
            server.EndAcceptTcpClient(IA);
            server.Stop();
        }

        private void conIn(IAsyncResult ar)
        {
            TcpClient c = server.EndAcceptTcpClient(ar);
            if (c != null)
            {
                if (SMTPincomming != null)
                {
                    SMTPincomming(c);
                }
                else
                {
                    try
                    {
                        c.Client.Send(Encoding.ASCII.GetBytes("421 i'm busy. GTFO!"));
                    }
                    catch
                    {
                        //NOOP
                    }
                    c.Client.Disconnect(false);
                    c.Close();
                }
                IA = server.BeginAcceptTcpClient(new AsyncCallback(conIn), null);
            }
        }
    }
}
