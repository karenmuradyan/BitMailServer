using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BitServer
{
    public delegate void POP3incommingHandler(TcpClient c);
    public class POP3server
    {
        public event POP3incommingHandler POP3incomming;

        private TcpListener server;
        private IAsyncResult IA;

        public POP3server(int port)
        {
            server = new TcpListener(IPAddress.Loopback, port);
            server.Start();
            IA=server.BeginAcceptTcpClient(new AsyncCallback(conIn), null);
        }

        ~POP3server()
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
                if (POP3incomming != null)
                {
                    POP3incomming(c);
                }
                else
                {
                    try
                    {
                        c.Client.Send(Encoding.ASCII.GetBytes("+ERR <nelson>HAHA</nelson>"));
                    }
                    catch
                    {
                        //NOOP
                    }
                    c.Client.Disconnect(false);
                    c.Close();
                }
                IA=server.BeginAcceptTcpClient(new AsyncCallback(conIn), null);
            }
        }
    }
}
