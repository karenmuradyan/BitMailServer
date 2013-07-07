using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BitServer
{
    public struct BitAddrs
    {
        public BitAddr[] addresses;
    }

    public struct BitAddr
    {
        public string label;
        public string address;
        public int stream;
        public bool enabled;
    }

    public struct BitMsgs
    {
        public BitMsg[] inboxMessages;
    }

    public struct BitMsg
    {
        public int encodingType;
        public string toAddress;
        public string msgid;
        public int receivedTime;
        public string message;
        public string fromAddress;
        public string subject;
    }

    public static class Bitmessage
    {
        public const string BROADCAST = "BROADCAST";
        public const string ADDRESS = "ADDRESS";
        public const string DOMAIN = "bitmessage.ch";

        /// <summary>
        /// Sends a bitmessage
        /// </summary>
        /// <param name="API">Bitmessage API</param>
        /// <param name="From">Sender</param>
        /// <param name="To">Receiver (broadcast allowed)</param>
        /// <param name="Subject">Message Subject</param>
        /// <param name="Message">Message Body</param>
        public static void Send(BitAPI API, string From,string To,string Subject,string Message)
        {
            if (To == BROADCAST)
            {
                API.sendBroadcast(From, B64enc(Subject), B64enc(Message));
            }
            else
            {
                API.sendMessage(To, From, B64enc(Subject), B64enc(Message));
            }
        }

        /// <summary>
        /// Generates a BitMessage Address
        /// </summary>
        /// <param name="BA">API</param>
        /// <param name="label">Address label</param>
        /// <returns>Bitmessage Address</returns>
        public static string generateAddress(BitAPI BA,string label)
        {
            if(string.IsNullOrEmpty(label))
            {
                var dt = DateTime.Now;
                label = "GENADDR-" + dt.ToShortDateString()+";"+dt.ToShortTimeString();
            }
            return BA.createRandomAddress(B64enc(label), false);
        }


        /// <summary>
        /// Generates a deterministic BitMessage Address
        /// </summary>
        /// <param name="BA">API</param>
        /// <param name="passphrase">Passphrase</param>
        /// <param name="label">Address label</param>
        /// <returns>Bitmessage Address</returns>
        public static string generateAddress(BitAPI BA,string passphrase, string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                var dt = DateTime.Now;
                label = "GENADDR-" + dt.ToShortDateString() + ";" + dt.ToShortTimeString();
            }
            return BA.createDeterministicAddresses(B64enc(passphrase), 1, 3, 1, false);
        }


        /// <summary>
        /// Returns the bitmessage address, BROADCAST or null if invalid address.
        /// </summary>
        /// <param name="p">E-Mail Address to check</param>
        /// <returns>bitmessage address</returns>
        /// <param name="allowBroadcast">true, if broadcast address is valid</param>
        public static string ParseAddress(string p,bool allowBroadcast)
        {
            p = p.Replace("<", "").Replace(">", "").Trim();
            if (!p.ToLower().Contains("@" + DOMAIN))
            {
                return null;
            }
            if(p.Contains(" "))
            {
                p = p.Substring(p.LastIndexOf(' ') + 1);
            }
            if (p.ToUpper().StartsWith(BROADCAST + "@"))
            {
                return allowBroadcast ? BROADCAST : null;
            }
            if (p.ToUpper().StartsWith(ADDRESS + "@"))
            {
                return allowBroadcast ? ADDRESS : null;
            }
            if (p.StartsWith("BM-"))
            {
                return p.Split('@')[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts from/to Base64
        /// </summary>
        /// <param name="s">Autodetected Input string</param>
        /// <returns>Base64 Representation</returns>
        public static string B64enc(string s)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s), Base64FormattingOptions.None);
        }

        /// <summary>
        /// Converts from/to Base64
        /// </summary>
        /// <param name="s">Autodetected Input string</param>
        /// <returns>Base64 Representation</returns>
        public static string B64dec(string s)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }

        /// <summary>
        /// Returns a list of all Addresses
        /// </summary>
        /// <param name="BA">Bitmessage API</param>
        /// <returns>Array of addresses</returns>
        public static BitAddr[] getAddresses(BitAPI BA)
        {
            var ADDR = JsonConvert.DeserializeObject<BitAddrs>(BA.listAddresses());
            if (ADDR.addresses == null)
            {
                ADDR.addresses = new BitAddr[] { };
            }
            return ADDR.addresses;
        }

        /// <summary>
        /// List of all Bitmessage Messages in Inbox
        /// </summary>
        /// <param name="BA">BitAPI object</param>
        /// <returns>Array of Bitmessage Messages</returns>
        public static BitMsg[] getMessages(BitAPI BA)
        {

            BitMsgs MSG;
            try
            {
                MSG = JsonConvert.DeserializeObject<BitMsgs>(BA.getAllInboxMessages());
            }
            catch
            {
                return null;
            }

            if (MSG.inboxMessages != null)
            {
                for (int i = 0; i < MSG.inboxMessages.Length; i++)
                {
                    //Decode Base64
                    MSG.inboxMessages[i].message = B64dec(MSG.inboxMessages[i].message.Trim()).Trim();
                    MSG.inboxMessages[i].subject = B64dec(MSG.inboxMessages[i].subject.Trim()).Trim();
                    //Convert line endings
                    MSG.inboxMessages[i].message = MSG.inboxMessages[i].message
                        .Replace("\r\n", "\n")
                        .Replace("\r", "")
                        .Replace("\n", "\r\n");
                }
            }
            else
            {
                //Empty array
                MSG.inboxMessages = new BitMsg[0];
            }
            return MSG.inboxMessages;
        }
    }
}
