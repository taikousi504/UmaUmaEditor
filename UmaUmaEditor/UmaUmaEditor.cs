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

        int cbEdit3OldSelectedIndex = -1;
        int cbEdit3OldSelectionStart = 0;
        int cbEdit4OldSelectedIndex = -1;
        int cbEdit4OldSelectionStart = 0;

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

        void UpdateCB2(int selectedIndex = 0)
        {
            //まずすべてクリア
            cb2.Items.Clear();

            //ランク種類
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas : data.supports)
            {
                cb2.Items.Add(v.Key);
            }

            //最初の要素を選択状態に
            cb2.SelectedIndex = selectedIndex;
        }

        void UpdateCB3(int selectedIndex = 0)
        {
            //まずすべてクリア
            cbEdit3.Items.Clear();

            //カード
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text] : data.supports[cb2.Text])
            {
                cbEdit3.Items.Add(v.Key);
            }

            //最初の要素を選択状態に
            cbEdit3.SelectedIndex = selectedIndex;
        }

        void UpdateCB4(int selectedIndex = 0)
        {
            //まずすべてクリア
            cbEdit4.Items.Clear();

            //イベントリスト
            foreach (var v1 in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text][cbEdit3.Text].events : data.supports[cb2.Text][cbEdit3.Text].events)
            {
                foreach (var v2 in v1.Keys)
                {
                    cbEdit4.Items.Add(v2);
                }
            }

            //最初の要素を選択状態に
            cbEdit4.SelectedIndex = selectedIndex;
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
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex] : data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex])
            {
                for (int i = 0; i < v.Value.Count; i++)
                {
                    if (i == 0)
                    {
                        tbOption1.Text = v.Value[i].option;
                        tbEffect1.Text = v.Value[i].effect;
                        tbEffect1.Text = tbEffect1.Text.ReplaceLineEndings();
                    }
                    else if (i == 1)
                    {
                        tbOption2.Text = v.Value[i].option;
                        tbEffect2.Text = v.Value[i].effect;
                        tbEffect2.Text = tbEffect2.Text.ReplaceLineEndings();
                    }
                    else if (i == 2)
                    {
                        tbOption3.Text = v.Value[i].option;
                        tbEffect3.Text = v.Value[i].effect;
                        tbEffect3.Text = tbEffect3.Text.ReplaceLineEndings();
                    }
                    else if (i == 3)
                    {
                        tbOption4.Text = v.Value[i].option;
                        tbEffect4.Text = v.Value[i].effect;
                        tbEffect4.Text = tbEffect4.Text.ReplaceLineEndings();
                    }
                    else if (i == 4)
                    {
                        tbOption5.Text = v.Value[i].option;
                        tbEffect5.Text = v.Value[i].effect;
                        tbEffect5.Text = tbEffect5.Text.ReplaceLineEndings();
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

        void AddTextToTextBox(string text, int selectionStartSub = 0)
        {
            if (nowActiveTextBoxIndex == tbOption1.TabIndex)
            {
                int temp = tbOption1.SelectionStart;
                tbOption1.Text = tbOption1.Text.Insert(tbOption1.SelectionStart, text);
                tbOption1.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbOption1;
            }
            else if (nowActiveTextBoxIndex == tbEffect1.TabIndex)
            {
                int temp = tbEffect1.SelectionStart;
                tbEffect1.Text = tbEffect1.Text.Insert(tbEffect1.SelectionStart, text);
                tbEffect1.Text = tbEffect1.Text.ReplaceLineEndings();
                tbEffect1.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbEffect1;
            }
            else if (nowActiveTextBoxIndex == tbOption2.TabIndex)
            {
                int temp = tbOption2.SelectionStart;
                tbOption2.Text = tbOption2.Text.Insert(tbOption2.SelectionStart, text);
                tbOption2.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbOption2;
            }
            else if (nowActiveTextBoxIndex == tbEffect2.TabIndex)
            {
                int temp = tbEffect2.SelectionStart;
                tbEffect2.Text = tbEffect2.Text.Insert(tbEffect2.SelectionStart, text);
                tbEffect2.Text = tbEffect2.Text.ReplaceLineEndings();
                tbEffect2.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbEffect2;
            }
            else if (nowActiveTextBoxIndex == tbOption3.TabIndex)
            {
                int temp = tbOption3.SelectionStart;
                tbOption3.Text = tbOption3.Text.Insert(tbOption3.SelectionStart, text);
                tbOption3.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbOption3;
            }
            else if (nowActiveTextBoxIndex == tbEffect3.TabIndex)
            {
                int temp = tbEffect3.SelectionStart;
                tbEffect3.Text = tbEffect3.Text.Insert(tbEffect3.SelectionStart, text);
                tbEffect3.Text = tbEffect3.Text.ReplaceLineEndings();
                tbEffect3.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbEffect3;
            }
            else if (nowActiveTextBoxIndex == tbOption4.TabIndex)
            {
                int temp = tbOption4.SelectionStart;
                tbOption4.Text = tbOption4.Text.Insert(tbOption4.SelectionStart, text);
                tbOption4.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbOption4;
            }
            else if (nowActiveTextBoxIndex == tbEffect4.TabIndex)
            {
                int temp = tbEffect4.SelectionStart;
                tbEffect4.Text = tbEffect4.Text.Insert(tbEffect4.SelectionStart, text);
                tbEffect4.Text = tbEffect4.Text.ReplaceLineEndings();
                tbEffect4.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbEffect4;
            }
            else if (nowActiveTextBoxIndex == tbOption5.TabIndex)
            {
                int temp = tbOption5.SelectionStart;
                tbOption5.Text = tbOption5.Text.Insert(tbOption5.SelectionStart, text);
                tbOption5.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbOption5;
            }
            else if (nowActiveTextBoxIndex == tbEffect5.TabIndex)
            {
                int temp = tbEffect5.SelectionStart;
                tbEffect5.Text = tbEffect5.Text.Insert(tbEffect5.SelectionStart, text);
                tbEffect5.Text = tbEffect5.Text.ReplaceLineEndings();
                tbEffect5.SelectionStart = temp + text.Length - selectionStartSub;
                ActiveControl = tbEffect5;
            }
            else if (nowActiveTextBoxIndex == cbEdit3.TabIndex)
            {
                cbEdit3.Text = cbEdit3.Text.Insert(cbEdit3OldSelectionStart, text);
                cbEdit3.SelectionStart = cbEdit3OldSelectionStart + text.Length - selectionStartSub;
                cbEdit3OldSelectionStart = cbEdit3.SelectionStart;
                ActiveControl = cbEdit3;
            }
            else if (nowActiveTextBoxIndex == cbEdit4.TabIndex)
            {
                cbEdit4.Text = cbEdit4.Text.Insert(cbEdit4OldSelectionStart, text);
                cbEdit4.SelectionStart = cbEdit4OldSelectionStart + text.Length - selectionStartSub;
                cbEdit4OldSelectionStart = cbEdit4.SelectionStart;
                ActiveControl = cbEdit4;
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

        void AddCard()
        {
            if (string.IsNullOrEmpty(cbEdit3.Text))
            {
                cbEdit3.SelectedIndex = cbEdit3OldSelectedIndex;
            }

            else {
                if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
                {
                    if (data.charas[cb2.Text].ContainsKey(cbEdit3.Text) == false)
                    {
                        DialogResult result = MessageBox.Show("指定したキャラクターは存在しません。新規作成しますか？", "Not Found Charactor.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.charas[cb2.Text].Add(cbEdit3.Text, new CardEvent { events = new List<Dictionary<string, List<OptionEffect>>>() });

                            for (int i = 0; i < 11; i++)
                            {
                                data.charas[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            }

                            #region 汎用イベント追加
                            data.charas[cb2.Text][cbEdit3.Text].events[0].Add("ダンスレッスン", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[0]["ダンスレッスン"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "効果1"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[0]["ダンスレッスン"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "効果2"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[1].Add("追加の自主トレ", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[1]["追加の自主トレ"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "体力-5\n直前のトレーニング種別+5\n秋川理事長の絆ゲージ+5"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[1]["追加の自主トレ"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "体力+5"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[2].Add("新年の抱負", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[2]["新年の抱負"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "効果1"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[2]["新年の抱負"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "体力+20"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[2]["新年の抱負"].Add(new OptionEffect
                            {
                                option = "選択肢3",
                                effect = "スキルPt+20"
                            });


                            data.charas[cb2.Text][cbEdit3.Text].events[3].Add("初詣", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[3]["初詣"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "体力+30"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[3]["初詣"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "5種すべて+5"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[3]["初詣"].Add(new OptionEffect
                            {
                                option = "選択肢3",
                                effect = "スキルPt+35"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[4].Add("夏合宿（2年目）にて", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[4]["夏合宿（2年目）にて"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "パワー+10"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[4]["夏合宿（2年目）にて"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "根性+10"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[5].Add("レース入賞", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[5]["レース入賞"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "体力-20\nランダム1種+2～+8\nスキルPt+20～+45\nランダムで出走したレース場やバ場状態などに関するスキルヒント"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[5]["レース入賞"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "体力-10/-30\nランダム1種+2～+8\nスキルPt+20～+45\nランダムで出走したレース場やバ場状態などに関するスキルヒント"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[6].Add("レース勝利", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[6]["レース勝利"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "体力-15\nランダム1種+5～+10\nスキルPt+35/+45\nランダムで出走したレース場やバ場状態などに関するスキルヒント"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[6]["レース勝利"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "体力-5/-20/-30\nランダム1種+5～+10\nスキルPt+35/+45\nランダムで出走したレース場やバ場状態などに関するスキルヒント"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[7].Add("レース敗北", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[7]["レース敗北"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "体力-25\nランダム1種+0～+4\nスキルPt+10～+25\nランダムで出走したレース場やバ場状態などに関するスキルヒント"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[7]["レース敗北"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "体力-15/-25/-35\nランダム1種+0～+4\nスキルPt+10～+25\nランダムで出走したレース場やバ場状態などに関するスキルヒント"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[8].Add("飯イベ(仮)", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[8]["飯イベ(仮)"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "体力+10、スキルPt+5"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[8]["飯イベ(仮)"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "体力+30\nスキルPt+10\nスピード-5\nパワー+5\n太り気味になる"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[9].Add("お大事に！", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[9]["お大事に！"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "やる気-1\n直前のトレーニング種別-5\nランダムで「練習ベタ」"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[9]["お大事に！"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "失敗時\n└やる気-1\n└直前のトレーニング種別-10\n└ランダムで「練習ベタ」\n成功時\n└「練習上手〇」"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[10].Add("無茶は厳禁！", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[10]["無茶は厳禁！"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "やる気-3\n直前のトレーニング種別-10\n5種ステータスからランダムに2種-10\nランダムで「練習ベタ」"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[10]["無茶は厳禁！"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "失敗時\n└やる気-3\n└直前のトレーニング種別-10\n└5種ステータスからランダムに2種-10\n└「練習ベタ」\n成功時\n└体力+10\n└「練習上手〇」"
                            });

                            #endregion

                            UpdateCB3(data.charas[cb2.Text].Count - 1);
                            UpdateCB4();
                            UpdateOptionEffect();
                        }
                        else
                        {
                            cbEdit3.SelectedIndex = cbEdit3OldSelectedIndex;
                        }
                    }
                    else
                    {
                        cbEdit3.SelectedIndex = cbEdit3OldSelectedIndex;
                    }
                }
                else
                {
                    if (data.supports[cb2.Text].ContainsKey(cbEdit3.Text) == false)
                    {
                        DialogResult result = MessageBox.Show("指定したカードは存在しません。新規作成しますか？", "Not Found Card.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.supports[cb2.Text].Add(cbEdit3.Text, new CardEvent { events = new List<Dictionary<string, List<OptionEffect>>>() });
                            data.supports[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            data.supports[cb2.Text][cbEdit3.Text].events[0].Add("新イベント名", new List<OptionEffect>());
                            data.supports[cb2.Text][cbEdit3.Text].events[0]["新イベント名"].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "効果1"
                            });
                            data.supports[cb2.Text][cbEdit3.Text].events[0]["新イベント名"].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "効果2"
                            });

                            UpdateCB3(data.supports[cb2.Text].Count - 1);
                            UpdateCB4();
                            UpdateOptionEffect();
                        }
                        else
                        {
                            cbEdit3.SelectedIndex = cbEdit3OldSelectedIndex;
                        }
                    }
                    else
                    {
                        cbEdit3.SelectedIndex = cbEdit3OldSelectedIndex;
                    }
                }
            }
        }

        void AddEvent()
        {
            if (string.IsNullOrEmpty(cbEdit4.Text))
            {
                cbEdit4.SelectedIndex = cbEdit4OldSelectedIndex;
            }
            else
            {
                if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
                {
                    bool isExistEvent = false;
                    for (int i = 0; i < data.charas[cb2.Text][cbEdit3.Text].events.Count; i++)
                    {
                        if (data.charas[cb2.Text][cbEdit3.Text].events[i].ContainsKey(cbEdit4.Text))
                        {
                            isExistEvent = true;
                            cbEdit4.SelectedIndex = i;
                            break;
                        }
                    }

                    if (isExistEvent == false)
                    {
                        DialogResult result = MessageBox.Show("指定したイベントは存在しません。新規作成しますか？", "Not Found Event.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.charas[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            data.charas[cb2.Text][cbEdit3.Text].events[data.charas[cb2.Text][cbEdit3.Text].events.Count - 1].Add(cbEdit4.Text, new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[data.charas[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "効果1"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[data.charas[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "効果2"
                            });

                            UpdateCB4(data.charas[cb2.Text][cbEdit3.Text].events.Count - 1);
                            UpdateOptionEffect();
                        }
                        else
                        {
                            var listEvent = new List<List<OptionEffect>>(data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4OldSelectedIndex].Values);
                            data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4OldSelectedIndex].Clear();
                            data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4OldSelectedIndex][cbEdit4.Text] = listEvent[0];
                            UpdateCB4(cbEdit4OldSelectedIndex);
                        }
                    }
                }
                else
                {
                    bool isExistEvent = false;
                    for (int i = 0; i < data.supports[cb2.Text][cbEdit3.Text].events.Count; i++)
                    {
                        if (data.supports[cb2.Text][cbEdit3.Text].events[i].ContainsKey(cbEdit4.Text))
                        {
                            isExistEvent = true;
                            cbEdit4.SelectedIndex = i;
                            break;
                        }
                    }

                    if (isExistEvent == false)
                    {
                        DialogResult result = MessageBox.Show("指定したイベントは存在しません。新規作成しますか？", "Not Found Event.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.supports[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            data.supports[cb2.Text][cbEdit3.Text].events[data.supports[cb2.Text][cbEdit3.Text].events.Count - 1].Add(cbEdit4.Text, new List<OptionEffect>());
                            data.supports[cb2.Text][cbEdit3.Text].events[data.supports[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "選択肢1",
                                effect = "効果1"
                            });
                            data.supports[cb2.Text][cbEdit3.Text].events[data.supports[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "選択肢2",
                                effect = "効果2"
                            });

                            UpdateCB4(data.supports[cb2.Text][cbEdit3.Text].events.Count - 1);
                            UpdateOptionEffect();
                        }
                        else
                        {
                            var listEvent = new List<List<OptionEffect>>(data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4OldSelectedIndex].Values);
                            data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4OldSelectedIndex].Clear();
                            data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4OldSelectedIndex][cbEdit4.Text] = listEvent[0];
                            UpdateCB4(cbEdit4OldSelectedIndex);
                        }
                    }
                }
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
            cbEdit3OldSelectedIndex = cbEdit3.SelectedIndex;
            UpdateCB4();
            UpdateOptionEffect();
        }

        private void cb4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEdit4OldSelectedIndex = cbEdit4.SelectedIndex;
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

            //【～～～】キャラ名　→　キャラ名【～～～】にする処理
            if (cbConvertText.Checked)
            {
                foreach (var v1 in data.charas.Keys.ToList())
                {
                    foreach (var v2 in data.charas[v1].Keys.ToList())
                    {
                        if (v2.StartsWith("【") && v2.Contains('】'))
                        {
                            string tmp = v2.Substring(0, v2.IndexOf('】', 0) + 1);
                            string tmp2 = v2.Substring(v2.IndexOf('】', 0) + 1);
                            data.charas[v1][tmp2 + tmp] = data.charas[v1][v2];
                            data.charas[v1].Remove(v2);
                        }
                    }
                }

                foreach (var v1 in data.supports.Keys.ToList())
                {
                    foreach (var v2 in data.supports[v1].Keys.ToList())
                    {
                        if (v2.StartsWith("[") && v2.Contains(']'))
                        {
                            string tmp = v2.Substring(0, v2.IndexOf(']', 0) + 1);
                            string tmp2 = v2.Substring(v2.IndexOf(']', 0) + 1);
                            data.supports[v1][tmp2 + tmp] = data.supports[v1][v2];
                            data.supports[v1].Remove(v2);
                        }
                    }
                }
            }

            //シリアライズ
            var str = JsonSerializer.Serialize(data, options);

            //バックアップ
            File.Copy("UmaLibrary/UmaMusumeLibrary.json", "UmaLibrary/UmaMusumeLibrary_backup.json", true);

            using (StreamWriter sw = new StreamWriter("UmaLibrary/UmaMusumeLibrary.json"))
            {
                sw.Write(str);
            }

            MessageBox.Show("保存しました。", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            AddTextToTextBox("\n");
        }

        private void cbEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AddCard();
            }
        }

        private void cbEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AddEvent();
            }
        }

        private void btnKakkoChara_Click(object sender, EventArgs e)
        {
            AddTextToTextBox("【】", 1);
        }

        private void btnKakkoSupport_Click(object sender, EventArgs e)
        {
            AddTextToTextBox("[]", 1);
        }

        private void cbEdit3_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = cbEdit3.TabIndex;
        }

        private void cbEdit4_Enter(object sender, EventArgs e)
        {
            nowActiveTextBoxIndex = cbEdit4.TabIndex;
        }

        private void GetCbEdit3SelectionStart()
        {
            cbEdit3OldSelectionStart = cbEdit3.SelectionStart;
        }

        private void GetCbEdit4SelectionStart()
        {
            cbEdit4OldSelectionStart = cbEdit4.SelectionStart;
        }

        private void cbEdit3_MouseUp(object sender, MouseEventArgs e)
        {
            GetCbEdit3SelectionStart();
        }

        private void cbEdit3_KeyUp(object sender, KeyEventArgs e)
        {
            GetCbEdit3SelectionStart();
        }

        private void cbEdit4_MouseUp(object sender, MouseEventArgs e)
        {
            GetCbEdit4SelectionStart();
        }

        private void cbEdit4_KeyUp(object sender, KeyEventArgs e)
        {
            GetCbEdit4SelectionStart();
        }

        private void UpdateData(object sender, EventArgs e)
        {
            //現在選択中のテキスト更新
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                //選択肢1
                if (string.IsNullOrEmpty(tbOption1.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 0)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = tbOption1.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = tbEffect1.Text;
                }
                //選択肢2
                if (string.IsNullOrEmpty(tbOption2.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 1)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = tbOption2.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = tbEffect2.Text;
                }
                //選択肢3
                if (string.IsNullOrEmpty(tbOption3.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 2)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].option = tbOption3.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].effect = tbEffect3.Text;
                }
                //選択肢4
                if (string.IsNullOrEmpty(tbOption4.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 3)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].option = tbOption4.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].effect = tbEffect4.Text;
                }
                //選択肢5
                if (string.IsNullOrEmpty(tbOption5.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 4)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].option = tbOption5.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].effect = tbEffect5.Text;
                }

                //空白チェック (OptionEffect両方空なら要素削除)
                if (string.IsNullOrEmpty(tbOption1.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect1.Text.Replace(" ", "")))
                {
                    //1個目の選択肢は空白許されない
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = "選択肢1";
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = "効果1";
                }
                if (string.IsNullOrEmpty(tbOption2.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect2.Text.Replace(" ", "")))
                {
                    //2個目の選択肢は空白許されない
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = "選択肢2";
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = "効果2";
                }
                if (string.IsNullOrEmpty(tbOption3.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect3.Text.Replace(" ", "")))
                {
                    //下に選択肢あったら上にシフト
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 4)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2] = data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3];
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(3);
                    }
                    else if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2] = data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3];
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3] = data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4];
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
                if (string.IsNullOrEmpty(tbOption4.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect4.Text.Replace(" ", "")))
                {
                    //下に選択肢あったら上にシフト
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3] = data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4];
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
                if (string.IsNullOrEmpty(tbOption5.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect5.Text.Replace(" ", "")))
                {
                    //要素削除
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
            }
            else
            {
                //選択肢1
                if (string.IsNullOrEmpty(tbOption1.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 0)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = tbOption1.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = tbEffect1.Text;
                }
                //選択肢2
                if (string.IsNullOrEmpty(tbOption2.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 1)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = tbOption2.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = tbEffect2.Text;
                }
                //選択肢3
                if (string.IsNullOrEmpty(tbOption3.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 2)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].option = tbOption3.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].effect = tbEffect3.Text;
                }
                //選択肢4
                if (string.IsNullOrEmpty(tbOption4.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 3)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].option = tbOption4.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].effect = tbEffect4.Text;
                }
                //選択肢5
                if (string.IsNullOrEmpty(tbOption5.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 4)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].option = tbOption5.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].effect = tbEffect5.Text;
                }

                //空白チェック (OptionEffect両方空なら要素削除)
                if (string.IsNullOrEmpty(tbOption1.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect1.Text.Replace(" ", "")))
                {
                    //1個目の選択肢は空白許されない
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = "選択肢1";
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = "効果1";
                }
                if (string.IsNullOrEmpty(tbOption2.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect2.Text.Replace(" ", "")))
                {
                    //2個目の選択肢は空白許されない
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = "選択肢2";
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = "効果2";
                }
                if (string.IsNullOrEmpty(tbOption3.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect3.Text.Replace(" ", "")))
                {
                    //下に選択肢あったら上にシフト
                    if (data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 4)
                    {
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2] = data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3];
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(3);
                    }
                    else if (data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2] = data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3];
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3] = data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4];
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
                if (string.IsNullOrEmpty(tbOption4.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect4.Text.Replace(" ", "")))
                {
                    //下に選択肢あったら上にシフト
                    if (data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3] = data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4];
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
                if (string.IsNullOrEmpty(tbOption5.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect5.Text.Replace(" ", "")))
                {
                    //要素削除
                    if (data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
            }

            UpdateOptionEffect();
        }

        private void btnOpenSaveDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", Environment.CurrentDirectory + @"\UmaLibrary");
        }
    }
}