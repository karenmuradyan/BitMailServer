using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace BitServer
{
    public delegate void SMTPcommandHandler(SMTPconnection SMTP, string command, string[] args, string raw);
    public delegate void SMTPdroppedHandler();

    public class SMTPconnection
    {
        public event SMTPcommandHandler SMTPcommand;
        public event SMTPdroppedHandler SMTPdropped;

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

        public bool dataMode
        { get; set; }

        public SMTPconnection(TcpClient cli)
        {
            dataMode = false;
            c = cli;
            lastLine = new byte[10240];
            buffer = new byte[10240];
            c.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(dataRec), null);
            t = new Thread(new ThreadStart(check));
            t.IsBackground = true;
            t.Start();
        }

        ~SMTPconnection()
        {
            msg(451, "Connection Disposed gayfag!");
            exit();
        }

        public void Dispose()
        {
            msg(451,"Connection Disposed gayfag!");
            exit();
        }

        private void check()
        {
            while (IsConnected)
            {
                Thread.Sleep(100);
            }
            if (SMTPdropped != null)
            {
                SMTPdropped();
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
                        if (SMTPcommand != null)
                        {
                            string line = Encoding.UTF8.GetString(lastLine, 0, linePos - 1);
                            linePos = 0;
                            if (line.Contains(" ") && !dataMode)
                            {
                                SMTPcommand(this,
                                    line.ToUpper().Substring(0, line.IndexOf(' ')),
                                    line.Substring(line.IndexOf(' ') + 1).Split(' '),
                                    line);
                            }
                            else
                            {
                                SMTPcommand(this, line.ToUpper(), new string[]{}, line);
                            }
                        }
                    }
                    else
                    {
                        lastLine[linePos++] = buffer[j];
                        if (linePos >= lastLine.Length)
                        {
                            msg(552,"Make Lines shorter you stupid!");
                            exit();
                            linePos = 0;
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

        private void exit()
        {
            try
            {
                c.Client.Disconnect(false);
                c.Close();
            }
            catch
            {
                //NOOP
            }
            if (SMTPdropped != null)
            {
                SMTPdropped();
            }
        }

        public void msg(int number,string Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                msg(number, new string[] { Message });
            }
        }

        public void msg(int number, string[] Message)
        {
            if (Message != null && Message.Length > 0)
            {
                for (int i = 0; i < Message.Length - 1; i++)
                {
                    send(string.Format("{0}-{1}",number.ToString("000"),Message[i]));
                }
                send(string.Format("{0} {1}", number.ToString("000"), Message[Message.Length-1]));
            }
        }

        public void msg(byte[][] raw)
        {
            foreach (byte[] b in raw)
            {
                sendraw(b);
            }
        }

        private void sendraw(byte[] line)
        {
            c.Client.Send(line);
        }

        private void send(string Message)
        {
            if (c != null)
            {
                try
                {
                    sendraw(Encoding.ASCII.GetBytes(Message + "\r\n"));
                }
                catch
                {
                    c.Close();
                    if (SMTPdropped != null)
                    {
                        SMTPdropped();
                    }
                }
            }
        }

        public void close()
        {
            exit();
            c = null;
        }
    }
}
