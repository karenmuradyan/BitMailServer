using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace BitServer
{
    public static class BitAPIserver
    {
        public const string I_SECT = "bitmessagesettings";
#if DEBUG
        //private const string I_FILE = @"C:\Users\kgut\Desktop\BM\keys.dat";
        public const string I_FILE = @"D:\BM\keys.dat";
#else
        public const string I_FILE="keys.dat";
#endif
        public static BitAPI BA;

        public static bool init(BitSettings BS)
        {
            BA = (BitAPI)CookComputing.XmlRpc.XmlRpcProxyGen.Create(typeof(BitAPI));
            BA.Url = string.Format("http://{0}:{1}/",BS.IP,BS.Port);
            BA.Headers.Add("Authorization", "Basic " + Bitmessage.B64enc(string.Format("{0}:{1}",BS.UName,BS.UPass)));
            return true;
        }
    }
}
