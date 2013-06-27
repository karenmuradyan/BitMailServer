using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace BitServer
{
    public delegate void POP3commandHandler(POP3connection POP3, string command,string[] args,string raw);
    public delegate void POP3droppedHandler();

    public class POP3connection
    {
        public event POP3commandHandler POP3command;
        public event POP3droppedHandler POP3dropped;

        private TcpClient c;
        private Thread t;
        private byte[] buffer;
        private byte[] lastLine;
        private int linePos = 0;

        public bool IsConnected
        {
            get
            {
                try
                {
                    return !(c.Client.Poll(1, SelectMode.SelectRead) && c.Client.Available == 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        public POP3connection(TcpClient cli)
        {
            c = cli;
            lastLine = new byte[10240];
            buffer = new byte[10240];
            c.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(dataRec), null);
            t = new Thread(new ThreadStart(check));
            t.IsBackground = true;
            t.Start();
        }

        ~POP3connection()
        {
            err("Connection Disposed gayfag!", true);
        }

        public void Dispose()
        {
            err("Connection Disposed gayfag!", true);
        }

        private void check()
        {
            while (IsConnected)
            {
                Thread.Sleep(100);
            }
            if (POP3dropped != null)
            {
                POP3dropped();
            }
        }

        private void dataRec(IAsyncResult ar)
        {
            int i = 0;
            try
            {
                i = c.Client.EndReceive(ar);
            }
            catch
            {
                return;
            }
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if (buffer[j] == 10)
                    {
                        if (POP3command != null)
                        {
                            string line = Encoding.UTF8.GetString(lastLine, 0, linePos - 1).Trim();
                            linePos = 0;
                            if (line.Contains(" "))
                            {
                                POP3command(this,
                                    line.ToUpper().Substring(0,line.IndexOf(' ')),
                                    line.Substring(line.IndexOf(' ')+1).Split(' '),
                                    line);
                            }
                            else
                            {
                                POP3command(this,line.ToUpper(), new string[]{}, line);
                            }
                        }
                    }
                    else
                    {
                        lastLine[linePos++] = buffer[j];
                        if (linePos >= lastLine.Length)
                        {
                            err("Make Lines shorter you stupid!", true);
                            linePos = 0;
                            if (POP3dropped != null)
                            {
                                POP3dropped();
                            }
                            return;
                        }
                    }
                }
                if (IsConnected)
                {
                    c.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(dataRec), null);
                }
            }
        }

        public void ok(string Message)
        {
            send("+OK " + Message);
        }

        public void err(string Message, bool close)
        {
            try
            {
                send("-ERR " + Message);
            }
            catch
            {
                close = true;
            }
            if (close)
            {
                if (c!=null && IsConnected)
                {
                    c.Client.Disconnect(false);
                }
                c = null;
            }
        }

        public void sendRaw(string content)
        {
            c.Client.Send(Encoding.ASCII.GetBytes(content));
        }

        private void send(string Message)
        {
            if (c != null)
            {
                try
                {
                    c.Client.Send(Encoding.ASCII.GetBytes(Message + "\r\n"));
                }
                catch
                {
                    c.Close();
                    if (POP3dropped != null)
                    {
                        POP3dropped();
                    }
                }
            }
        }

        internal void close()
        {
            c.Client.Disconnect(false);
            c = null;
        }
    }
}
