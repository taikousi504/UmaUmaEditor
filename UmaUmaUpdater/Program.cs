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
            string jsonURL = "https://raw.githubusercontent.com/taikousi504/UmaUmaEditor/master/UmaUmaUpdater/UmaMusumeLibrary.json";

            //Config.ini読み込み
            if (File.Exists("Config.ini") == true)
            {
                using (StreamReader sr = new StreamReader("Config.ini"))
                {
                    while (!sr.EndOfStream)
                    {
                        string? line = sr.ReadLine();
                        if (line?.StartsWith("JSON_URL=") == true)
                        {
                            jsonURL = line.Substring(9);
                        }
                    }
                }
            }

            //jsonダウンロード
            try
            {
                Console.WriteLine("UmaMusumeLibrary.jsonを更新します。");
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(jsonURL, "../UmaLibrary/UmaMusumeLibrary.json");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("エラーが発生しました。\n" + e.Message);
                Console.WriteLine("続行するには何かキーを押してください");
                Console.ReadKey();
            }

            if (File.Exists("../UmaLibrary/UmaMusumeLibrary.json") == false)
            {
                Console.WriteLine("エラーが発生しました。\nファイルが存在しません。");
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
                Console.WriteLine("エラーが発生しました。UmaUmaCruiseの実行ファイルが見つかりませんでした。\n" + e.Message);
                Console.WriteLine("続行するには何かキーを押してください");
                Console.ReadKey();
            }


            Console.WriteLine("UmaUmaCruiseを起動しました。");
        }
    }
}