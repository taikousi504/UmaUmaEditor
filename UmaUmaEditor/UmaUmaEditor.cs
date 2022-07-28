using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace UmaUmaEditor
{
    public partial class UmaUmaEditor : Form
    {
        UmaUmaData data;

        public UmaUmaEditor()
        {
            InitializeComponent();
            Initialize();
        }

        void Initialize()
        {
            // JSONオプション設定
            var options = new JsonSerializerOptions
            {
                // 日本語を変換するためのエンコード設定
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),

                // インデントを付ける
                WriteIndented = true
            };

            //UmaLibrary/UmaMusumeLibrary.jsonを読み込む
            string str;
            using (StreamReader sr = new StreamReader("UmaLibrary/UmaMusumeLibrary.json"))
            {
                str = sr.ReadToEnd();
            }

            //デシリアライズ
            data = JsonSerializer.Deserialize<UmaUmaData>(str, options);

            //表示用にアイテム追加
            cb1.Items.Add("Charactor");
            cb1.Items.Add("Support");

            //最初の要素を選択状態に
            cb1.SelectedIndex = 0;

            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
        }

        void UpdateCB2()
        {
            //まずすべてクリア
            cb2.Items.Clear();

            //ランク種類
            //Charactor
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                foreach (var v in data.charas)
                {
                    cb2.Items.Add(v.Key);
                }
            }
            //Support
            else if (cb1.SelectedIndex == (int)Kinds.KIND_SUPPORT)
            {
                foreach (var v in data.supports)
                {
                    cb2.Items.Add(v.Key);
                }
            }

            //最初の要素を選択状態に
            cb2.SelectedIndex = 0;
        }

        void UpdateCB3()
        {
            //まずすべてクリア
            cb3.Items.Clear();

            //カード
            //Charactor
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                foreach (var v in data.charas[cb2.Text])
                {
                    cb3.Items.Add(v.Key);
                }
            }
            //Support
            else if (cb1.SelectedIndex == (int)Kinds.KIND_SUPPORT)
            {
                foreach (var v in data.supports[cb2.Text])
                {
                    cb3.Items.Add(v.Key);
                }
            }

            //最初の要素を選択状態に
            cb3.SelectedIndex = 0;
        }

        void UpdateCB4()
        {
            //まずすべてクリア
            cb4.Items.Clear();

            //イベントリスト
            //Charactor
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                foreach (var v1 in data.charas[cb2.Text][cb3.Text].events)
                {
                    foreach (var v2 in v1.Keys)
                    {
                        cb4.Items.Add(v2);
                    }
                }
            }
            //Support
            else if (cb1.SelectedIndex == (int)Kinds.KIND_SUPPORT)
            {
                foreach (var v1 in data.supports[cb2.Text][cb3.Text].events)
                {
                    foreach (var v2 in v1.Keys)
                    {
                        cb4.Items.Add(v2);
                    }
                }
            }

            //最初の要素を選択状態に
            cb4.SelectedIndex = 0;
        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
        }

        private void cb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB3();
            UpdateCB4();
        }

        private void cb3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB4();
        }
    }
}