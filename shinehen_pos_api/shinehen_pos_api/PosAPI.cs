using System;
using System.Runtime.InteropServices;

namespace shinehen_pos_api
{
    public class PosAPI
    {
        [DllImport("PosAPI.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string put(String message);
        [DllImport("PosAPI.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string returnBill(String message);
        [DllImport("PosAPI.dll")]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string sendData();
        [DllImport("PosAPI.dll")]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string checkApi();
        [DllImport("PosAPI.dll")]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string getInformation();
        [DllImport("PosAPI.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string callFunction(string funcName, string param);
        public const string NUMBER_FORMAT = "0.00";
    }
}
