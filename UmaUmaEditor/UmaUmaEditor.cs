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
        int nowActiveTextBoxIndex = -1;

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

            //更新
            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
            UpdateAddParam();
        }

        void UpdateCB2()
        {
            //まずすべてクリア
            cb2.Items.Clear();

            //ランク種類
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas : data.supports)
            {
                cb2.Items.Add(v.Key);
            }

            //最初の要素を選択状態に
            cb2.SelectedIndex = 0;
        }

        void UpdateCB3()
        {
            //まずすべてクリア
            cb3.Items.Clear();

            //カード
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text] : data.supports[cb2.Text])
            {
                cb3.Items.Add(v.Key);
            }

            //最初の要素を選択状態に
            cb3.SelectedIndex = 0;
        }

        void UpdateCB4()
        {
            //まずすべてクリア
            cb4.Items.Clear();

            //イベントリスト
            foreach (var v1 in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text][cb3.Text].events : data.supports[cb2.Text][cb3.Text].events)
            {
                foreach (var v2 in v1.Keys)
                {
                    cb4.Items.Add(v2);
                }
            }

            //最初の要素を選択状態に
            cb4.SelectedIndex = 0;
        }

        void UpdateOptionEffect()
        {
            //まずすべてクリア
            tbOption1.Clear();
            tbOption2.Clear();
            tbOption3.Clear();
            tbOption4.Clear();
            tbOption5.Clear();

            tbEffect1.Clear();
            tbEffect2.Clear();
            tbEffect3.Clear();
            tbEffect4.Clear();
            tbEffect5.Clear();

            //OptionとEffect
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text][cb3.Text].events[cb4.SelectedIndex] : data.supports[cb2.Text][cb3.Text].events[cb4.SelectedIndex])
            {
                for (int i = 0; i < v.Value.Count; i++)
                {
                    if (i == 0)
                    {
                        tbOption1.Text = v.Value[i].option;
                        tbEffect1.Text = v.Value[i].effect;
                    }
                    else if (i == 1)
                    {
                        tbOption2.Text = v.Value[i].option;
                        tbEffect2.Text = v.Value[i].effect;
                    }
                    else if (i == 2)
                    {
                        tbOption3.Text = v.Value[i].option;
                        tbEffect3.Text = v.Value[i].effect;
                    }
                    else if (i == 3)
                    {
                        tbOption4.Text = v.Value[i].option;
                        tbEffect4.Text = v.Value[i].effect;
                    }
                    else if (i == 4)
                    {
                        tbOption5.Text = v.Value[i].option;
                        tbEffect5.Text = v.Value[i].effect;
                    }
                    else
                    {
                        MessageBox.Show("5つ以上の選択肢が含まれています。\n編集できるのは5つの選択肢までです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }
            }
        }

        void UpdateAddParam()
        {
            btnSpeed.Text = "スピード" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnStamina.Text = "スタミナ" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnPower.Text = "パワー" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnGuts.Text = "根性" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnWise.Text = "賢さ" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnAllParam.Text = "全ステ" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();

            btnMotivation.Text = "やる気" + (numUDMotivation.Value >= 0 ? "+" : "") + numUDMotivation.Value.ToString();
            btnHP.Text = "体力" + (numUDHP.Value >= 0 ? "+" : "") + numUDHP.Value.ToString();
            btnMaxHP.Text = "体力最大値" + (numUDMaxHp.Value >= 0 ? "+" : "") + numUDMaxHp.Value.ToString();
            btnSkillPt.Text = "スキルPt" + (numUDSkillPt.Value >= 0 ? "+" : "") + numUDSkillPt.Value.ToString();
            btnBond.Text = "絆ゲージ" + (numUDBond.Value >= 0 ? "+" : "") + numUDBond.Value.ToString();

            btnHintLv.Text = cbHintLv.SelectedIndex != cbHintLv.Items.Count - 1 ? "ヒントLv+" + (cbHintLv.SelectedIndex + 1).ToString() : "になる";
        }

        private void UpdateAddParam(object sender, EventArgs e)
        {
            UpdateAddParam();
        }

        void AddTextToTextBox(string text)
        {
            if (nowActiveTextBoxIndex == tbOption1.TabIndex)
            {
                tbOption1.Text = tbOption1.Text.Insert(tbOption1.SelectionStart, text);
                tbOption1.SelectionStart = tbOption1.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect1.TabIndex)
            {
                tbEffect1.Text = tbEffect1.Text.Insert(tbEffect1.SelectionStart, text);
                tbEffect1.SelectionStart = tbEffect1.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption2.TabIndex)
            {
                tbOption2.Text = tbOption2.Text.Insert(tbOption2.SelectionStart, text);
                tbOption2.SelectionStart = tbOption2.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect2.TabIndex)
            {
                tbEffect2.Text = tbEffect2.Text.Insert(tbEffect2.SelectionStart, text);
                tbEffect2.SelectionStart = tbEffect2.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption3.TabIndex)
            {
                tbOption3.Text = tbOption3.Text.Insert(tbOption3.SelectionStart, text);
                tbOption3.SelectionStart = tbOption3.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect3.TabIndex)
            {
                tbEffect3.Text = tbEffect3.Text.Insert(tbEffect3.SelectionStart, text);
                tbEffect3.SelectionStart = tbEffect3.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption4.TabIndex)
            {
                tbOption4.Text = tbOption4.Text.Insert(tbOption4.SelectionStart, text);
                tbOption4.SelectionStart = tbOption4.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect4.TabIndex)
            {
                tbEffect4.Text = tbEffect4.Text.Insert(tbEffect4.SelectionStart, text);
                tbEffect4.SelectionStart = tbEffect4.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption5.TabIndex)
            {
                tbOption5.Text = tbOption5.Text.Insert(tbOption5.SelectionStart, text);
                tbOption5.SelectionStart = tbOption5.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect5.TabIndex)
            {
                tbEffect5.Text = tbEffect5.Text.Insert(tbEffect5.SelectionStart, text);
                tbEffect5.SelectionStart = tbEffect5.Text.Length;
            }
        }

        private void AddTextToTextBox(object sender, EventArgs e)
        {
            AddTextToTextBox(((Button)sender).Text);
        }

        private void AddConditionTextToTextBox(object sender, EventArgs e)
        {
            AddTextToTextBox((cbIsRandom.Checked ? "ランダムで" : "") + "「" + ((Button)sender).Text + "」");
        }

        private void DeleteText(object sender, EventArgs e)
        {
            if (nowActiveTextBoxIndex == tbOption1.TabIndex)
            {
                tbOption1.Text = string.Empty;
                tbOption1.SelectionStart = tbOption1.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect1.TabIndex)
            {
                tbEffect1.Text = string.Empty;
                tbEffect1.SelectionStart = tbEffect1.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption2.TabIndex)
            {
                tbOption2.Text = string.Empty;
                tbOption2.SelectionStart = tbOption2.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect2.TabIndex)
            {
                tbEffect2.Text = string.Empty;
                tbEffect2.SelectionStart = tbEffect2.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption3.TabIndex)
            {
                tbOption3.Text = string.Empty;
                tbOption3.SelectionStart = tbOption3.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect3.TabIndex)
            {
                tbEffect3.Text = string.Empty;
                tbEffect3.SelectionStart = tbEffect3.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption4.TabIndex)
            {
                tbOption4.Text = string.Empty;
                tbOption4.SelectionStart = tbOption4.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect4.TabIndex)
            {
                tbEffect4.Text = string.Empty;
                tbEffect4.SelectionStart = tbEffect4.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbOption5.TabIndex)
            {
                tbOption5.Text = string.Empty;
                tbOption5.SelectionStart = tbOption5.Text.Length;
            }
            else if (nowActiveTextBoxIndex == tbEffect5.TabIndex)
            {
                tbEffect5.Text = string.Empty;
                tbEffect5.SelectionStart = tbEffect5.Text.Length;
            }
        }

        private void UmaUmaEditor_KeyDown(object sender, KeyEventArgs e)
        {
            //アンドゥ
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                if (nowActiveTextBoxIndex == tbOption1.TabIndex)
                {
                    tbOption1.Undo();
                }
                else if (nowActiveTextBoxIndex == tbEffect1.TabIndex)
                {
                    tbEffect1.Undo();
                }
                else if (nowActiveTextBoxIndex == tbOption2.TabIndex)
                {
                    tbOption2.Undo();
                }
                else if (nowActiveTextBoxIndex == tbEffect2.TabIndex)
                {
                    tbEffect2.Undo();
                }
                else if (nowActiveTextBoxIndex == tbOption3.TabIndex)
                {
                    tbOption3.Undo();
                }
                else if (nowActiveTextBoxIndex == tbEffect3.TabIndex)
                {
                    tbEffect3.Undo();
                }
                else if (nowActiveTextBoxIndex == tbOption4.TabIndex)
                {
                    tbOption4.Undo();
                }
                else if (nowActiveTextBoxIndex == tbEffect4.TabIndex)
                {
                    tbEffect4.Undo();
                }
                else if (nowActiveTextBoxIndex == tbOption5.TabIndex)
                {
                    tbOption5.Undo();
                }
                else if (nowActiveTextBoxIndex == tbEffect5.TabIndex)
                {
                    tbEffect5.Undo();
                }
            }

            //リドゥ
            if (e.KeyData == (Keys.Control | Keys.Y))
            {
                if (nowActiveTextBoxIndex == tbOption1.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbEffect1.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbOption2.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbEffect2.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbOption3.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbEffect3.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbOption4.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbEffect4.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbOption5.TabIndex)
                {
                }
                else if (nowActiveTextBoxIndex == tbEffect5.TabIndex)
                {
                }
            }
        }


        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
            UpdateOptionEffect();
        }

        private void cb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB3();
            UpdateCB4();
            UpdateOptionEffect();
        }

        private void cb3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCB4();
            UpdateOptionEffect();
        }

        private void cb4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOptionEffect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // JSONオプション設定
            var options = new JsonSerializerOptions
            {
                // 日本語を変換するためのエンコード設定
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

                // インデントを付ける
                WriteIndented = true
            };


            //シリアライズ
            var str = JsonSerializer.Serialize(data, options);

            using (StreamWriter sw = new StreamWriter("UmaLibrary/UmaMusumeLibrary_Test.json"))
            {
                sw.Write(str);
            }
        }

        private void tbOption1_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbOption1.TabIndex;
        }

        private void tbEffect1_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbEffect1.TabIndex;
        }

        private void tbOption2_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbOption2.TabIndex;
        }

        private void tbEffect2_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbEffect2.TabIndex;
        }

        private void tbOption3_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbOption3.TabIndex;
        }

        private void tbEffect3_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbEffect3.TabIndex;
        }

        private void tbOption4_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbOption4.TabIndex;
        }

        private void tbEffect4_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbEffect4.TabIndex;
        }

        private void tbOption5_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbOption5.TabIndex;
        }

        private void tbEffect5_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = tbEffect5.TabIndex;
        }

        private void btnHintLv_Click(object sender, EventArgs e)
        {
            string text = "「" + cbSkillName.Text + "」" + btnHintLv.Text;
            AddTextToTextBox(text);
        }

        private void btnNewLine_Click(object sender, EventArgs e)
        {
            AddTextToTextBox(Environment.NewLine);
        }
    }
}