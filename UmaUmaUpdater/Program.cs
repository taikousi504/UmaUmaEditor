using System;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace UmaUmaUpdater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile("https://raw.githubusercontent.com/taikousi504/UmaUmaEditor/master/UmaUmaUpdater/UmaMusumeLibrary.json", "../UmaLibrary/UmaMusumeLibrary.json");
            wc.Dispose();

            Console.WriteLine("UmaMusumeLibrary.jsonを更新しました。");

            //UmaUmaCruise起動
            Process.Start("../UmaUmaCruise.exe");
        }
    }
}