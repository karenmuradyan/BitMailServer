using System;
using System.Collections.Generic;
using System.Text;
using CookComputing.XmlRpc;

namespace BitServer
{
    public static class UnixTime
    {
        private static DateTime UNIX = new DateTime(1970, 1, 1);
        private static DateTime OVERFLOW = new DateTime(2038, 1, 19, 3, 14, 18);

        public static DateTime ConvertFrom(int unix)
        {
            if (unix > 0)
            {
                return UNIX.AddSeconds(unix);
            }
            //works until 7 Feb 2106
            return OVERFLOW.AddSeconds(unix + int.MaxValue);
        }

        public static int ConvertTo(DateTime DT)
        {
            return (int)(DT.Subtract(UNIX).TotalSeconds);
        }
    }

    public interface BitAPI : IXmlRpcProxy
    {
        [XmlRpcMethod]
        string helloWorld(string firstWord, string secondWord);
        [XmlRpcMethod]
        int add(int a, int b);
        [XmlRpcMethod]
        void statusBar(string message);
        [XmlRpcMethod]
        string sendBroadcast(string Addr, string Base64subject, string Base64message);
        [XmlRpcMethod]
        string sendMessage(string ToAddr,string FromAddr, string Base64subject, string Base64message);
        [XmlRpcMethod]
        string createRandomAddress(string base64label, bool shortAddr);
        [XmlRpcMethod]
        string getAllInboxMessages();
        [XmlRpcMethod]
        void trashMessage(string msgid);
        [XmlRpcMethod]
        string createDeterministicAddresses(string base64Password, int numberAddr, int addrVersion, int stream, bool shortAddr);
        [XmlRpcMethod]
        string listAddresses();
        [XmlRpcMethod]
        void addSubscription(string addr, string base64Label);
        [XmlRpcMethod]
        void deleteSubscription(string addr);
        [XmlRpcMethod]
        string getStatus(string ackdata);
    }
}
