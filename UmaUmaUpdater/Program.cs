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
            //jsonダウンロード
            try
            {
                Console.WriteLine("UmaMusumeLibrary.jsonを更新します。");
                WebClient wc = new WebClient();
                wc.DownloadFile("https://raw.githubusercontent.com/taikousi504/UmaUmaEditor/master/UmaUmaUpdater/UmaMusumeLibrary.json", "../UmaLibrary/UmaMusumeLibrary.json");
                wc.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("エラーが発生しました。UmaUmaCruiseの実行ファイルがあるディレクトリにUmaUmaUpdaterフォルダを配置し、本アプリを起動させてください。");
                Console.WriteLine("続行するには何かキーを押してください");
                Console.ReadKey();
            }

            Console.WriteLine("UmaMusumeLibrary.jsonを更新しました。");

            //UmaUmaCruise起動
            try
            {
                Process.Start("../UmaUmaCruise.exe");
            }
            catch (Exception e)
            {
                Console.WriteLine("エラーが発生しました。UmaUmaCruiseの実行ファイルが見つかりませんでした。");
                Console.WriteLine("続行するには何かキーを押してください");
                Console.ReadKey();
            }


            Console.WriteLine("UmaUmaCruiseを起動しました。");
        }
    }
}