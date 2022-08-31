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
            // JSON�I�v�V�����ݒ�
            var options = new JsonSerializerOptions
            {
                // ���{���ϊ����邽�߂̃G���R�[�h�ݒ�
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),

                // �C���f���g��t����
                WriteIndented = true
            };

            //UmaLibrary/UmaMusumeLibrary.json��ǂݍ���
            string str;
            using (StreamReader sr = new StreamReader("UmaLibrary/UmaMusumeLibrary.json"))
            {
                str = sr.ReadToEnd();
            }

            //�f�V���A���C�Y
            data = JsonSerializer.Deserialize<UmaUmaData>(str, options);

            //�\���p�ɃA�C�e���ǉ�
            cb1.Items.Add("Charactor");
            cb1.Items.Add("Support");

            //�ŏ��̗v�f��I����Ԃ�
            cb1.SelectedIndex = 0;

            //�X�V
            UpdateCB2();
            UpdateCB3();
            UpdateCB4();
            UpdateAddParam();
        }

        void UpdateCB2(int selectedIndex = 0)
        {
            //�܂����ׂăN���A
            cb2.Items.Clear();

            //�����N���
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas : data.supports)
            {
                cb2.Items.Add(v.Key);
            }

            //�ŏ��̗v�f��I����Ԃ�
            cb2.SelectedIndex = selectedIndex;
        }

        void UpdateCB3(int selectedIndex = 0)
        {
            //�܂����ׂăN���A
            cbEdit3.Items.Clear();

            //�J�[�h
            foreach (var v in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text] : data.supports[cb2.Text])
            {
                cbEdit3.Items.Add(v.Key);
            }

            //�ŏ��̗v�f��I����Ԃ�
            cbEdit3.SelectedIndex = selectedIndex;
        }

        void UpdateCB4(int selectedIndex = 0)
        {
            //�܂����ׂăN���A
            cbEdit4.Items.Clear();

            //�C�x���g���X�g
            foreach (var v1 in cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR ? data.charas[cb2.Text][cbEdit3.Text].events : data.supports[cb2.Text][cbEdit3.Text].events)
            {
                foreach (var v2 in v1.Keys)
                {
                    cbEdit4.Items.Add(v2);
                }
            }

            //�ŏ��̗v�f��I����Ԃ�
            cbEdit4.SelectedIndex = selectedIndex;
        }

        void UpdateOptionEffect()
        {
            //�܂����ׂăN���A
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

            //Option��Effect
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
                        MessageBox.Show("5�ȏ�̑I�������܂܂�Ă��܂��B\n�ҏW�ł���̂�5�̑I�����܂łł��B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }
            }
        }

        void UpdateAddParam()
        {
            btnSpeed.Text = "�X�s�[�h" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnStamina.Text = "�X�^�~�i" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnPower.Text = "�p���[" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnGuts.Text = "����" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnWise.Text = "����" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();
            btnAllParam.Text = "�S�X�e" + (numUDParam.Value >= 0 ? "+" : "") + numUDParam.Value.ToString();

            btnMotivation.Text = "���C" + (numUDMotivation.Value >= 0 ? "+" : "") + numUDMotivation.Value.ToString();
            btnHP.Text = "�̗�" + (numUDHP.Value >= 0 ? "+" : "") + numUDHP.Value.ToString();
            btnMaxHP.Text = "�̗͍ő�l" + (numUDMaxHp.Value >= 0 ? "+" : "") + numUDMaxHp.Value.ToString();
            btnSkillPt.Text = "�X�L��Pt" + (numUDSkillPt.Value >= 0 ? "+" : "") + numUDSkillPt.Value.ToString();
            btnBond.Text = "�J�Q�[�W" + (numUDBond.Value >= 0 ? "+" : "") + numUDBond.Value.ToString();

            btnHintLv.Text = cbHintLv.SelectedIndex != cbHintLv.Items.Count - 1 ? "�q���gLv+" + (cbHintLv.SelectedIndex + 1).ToString() : "�ɂȂ�";
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
            AddTextToTextBox((cbIsRandom.Checked ? "�����_����" : "") + "�u" + ((Button)sender).Text + "�v");
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
                        DialogResult result = MessageBox.Show("�w�肵���L�����N�^�[�͑��݂��܂���B�V�K�쐬���܂����H", "Not Found Charactor.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.charas[cb2.Text].Add(cbEdit3.Text, new CardEvent { events = new List<Dictionary<string, List<OptionEffect>>>() });

                            for (int i = 0; i < 11; i++)
                            {
                                data.charas[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            }

                            #region �ėp�C�x���g�ǉ�
                            data.charas[cb2.Text][cbEdit3.Text].events[0].Add("�_���X���b�X��", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[0]["�_���X���b�X��"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "����1"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[0]["�_���X���b�X��"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "����2"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[1].Add("�ǉ��̎���g��", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[1]["�ǉ��̎���g��"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�̗�-5\n���O�̃g���[�j���O���+5\n�H�엝�������J�Q�[�W+5"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[1]["�ǉ��̎���g��"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "�̗�+5"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[2].Add("�V�N�̕���", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[2]["�V�N�̕���"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "����1"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[2]["�V�N�̕���"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "�̗�+20"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[2]["�V�N�̕���"].Add(new OptionEffect
                            {
                                option = "�I����3",
                                effect = "�X�L��Pt+20"
                            });


                            data.charas[cb2.Text][cbEdit3.Text].events[3].Add("���w", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[3]["���w"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�̗�+30"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[3]["���w"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "5�킷�ׂ�+5"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[3]["���w"].Add(new OptionEffect
                            {
                                option = "�I����3",
                                effect = "�X�L��Pt+35"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[4].Add("�č��h�i2�N�ځj�ɂ�", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[4]["�č��h�i2�N�ځj�ɂ�"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�p���[+10"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[4]["�č��h�i2�N�ځj�ɂ�"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "����+10"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[5].Add("���[�X����", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[5]["���[�X����"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�̗�-20\n�����_��1��+2�`+8\n�X�L��Pt+20�`+45\n�����_���ŏo���������[�X���o���ԂȂǂɊւ���X�L���q���g"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[5]["���[�X����"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "�̗�-10/-30\n�����_��1��+2�`+8\n�X�L��Pt+20�`+45\n�����_���ŏo���������[�X���o���ԂȂǂɊւ���X�L���q���g"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[6].Add("���[�X����", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[6]["���[�X����"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�̗�-15\n�����_��1��+5�`+10\n�X�L��Pt+35/+45\n�����_���ŏo���������[�X���o���ԂȂǂɊւ���X�L���q���g"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[6]["���[�X����"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "�̗�-5/-20/-30\n�����_��1��+5�`+10\n�X�L��Pt+35/+45\n�����_���ŏo���������[�X���o���ԂȂǂɊւ���X�L���q���g"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[7].Add("���[�X�s�k", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[7]["���[�X�s�k"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�̗�-25\n�����_��1��+0�`+4\n�X�L��Pt+10�`+25\n�����_���ŏo���������[�X���o���ԂȂǂɊւ���X�L���q���g"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[7]["���[�X�s�k"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "�̗�-15/-25/-35\n�����_��1��+0�`+4\n�X�L��Pt+10�`+25\n�����_���ŏo���������[�X���o���ԂȂǂɊւ���X�L���q���g"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[8].Add("�уC�x(��)", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[8]["�уC�x(��)"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "�̗�+10�A�X�L��Pt+5"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[8]["�уC�x(��)"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "�̗�+30\n�X�L��Pt+10\n�X�s�[�h-5\n�p���[+5\n����C���ɂȂ�"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[9].Add("���厖�ɁI", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[9]["���厖�ɁI"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "���C-1\n���O�̃g���[�j���O���-5\n�����_���Łu���K�x�^�v"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[9]["���厖�ɁI"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "���s��\n�����C-1\n�����O�̃g���[�j���O���-10\n�������_���Łu���K�x�^�v\n������\n���u���K���Z�v"
                            });

                            data.charas[cb2.Text][cbEdit3.Text].events[10].Add("�����͌��ցI", new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[10]["�����͌��ցI"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "���C-3\n���O�̃g���[�j���O���-10\n5��X�e�[�^�X���烉���_����2��-10\n�����_���Łu���K�x�^�v"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[10]["�����͌��ցI"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "���s��\n�����C-3\n�����O�̃g���[�j���O���-10\n��5��X�e�[�^�X���烉���_����2��-10\n���u���K�x�^�v\n������\n���̗�+10\n���u���K���Z�v"
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
                        DialogResult result = MessageBox.Show("�w�肵���J�[�h�͑��݂��܂���B�V�K�쐬���܂����H", "Not Found Card.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.supports[cb2.Text].Add(cbEdit3.Text, new CardEvent { events = new List<Dictionary<string, List<OptionEffect>>>() });
                            data.supports[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            data.supports[cb2.Text][cbEdit3.Text].events[0].Add("�V�C�x���g��", new List<OptionEffect>());
                            data.supports[cb2.Text][cbEdit3.Text].events[0]["�V�C�x���g��"].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "����1"
                            });
                            data.supports[cb2.Text][cbEdit3.Text].events[0]["�V�C�x���g��"].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "����2"
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
                        DialogResult result = MessageBox.Show("�w�肵���C�x���g�͑��݂��܂���B�V�K�쐬���܂����H", "Not Found Event.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.charas[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            data.charas[cb2.Text][cbEdit3.Text].events[data.charas[cb2.Text][cbEdit3.Text].events.Count - 1].Add(cbEdit4.Text, new List<OptionEffect>());
                            data.charas[cb2.Text][cbEdit3.Text].events[data.charas[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "����1"
                            });
                            data.charas[cb2.Text][cbEdit3.Text].events[data.charas[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "����2"
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
                        DialogResult result = MessageBox.Show("�w�肵���C�x���g�͑��݂��܂���B�V�K�쐬���܂����H", "Not Found Event.", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            data.supports[cb2.Text][cbEdit3.Text].events.Add(new Dictionary<string, List<OptionEffect>>());
                            data.supports[cb2.Text][cbEdit3.Text].events[data.supports[cb2.Text][cbEdit3.Text].events.Count - 1].Add(cbEdit4.Text, new List<OptionEffect>());
                            data.supports[cb2.Text][cbEdit3.Text].events[data.supports[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "�I����1",
                                effect = "����1"
                            });
                            data.supports[cb2.Text][cbEdit3.Text].events[data.supports[cb2.Text][cbEdit3.Text].events.Count - 1][cbEdit4.Text].Add(new OptionEffect
                            {
                                option = "�I����2",
                                effect = "����2"
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
            //�A���h�D
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

            //���h�D
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
            // JSON�I�v�V�����ݒ�
            var options = new JsonSerializerOptions
            {
                // ���{���ϊ����邽�߂̃G���R�[�h�ݒ�
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

                // �C���f���g��t����
                WriteIndented = true
            };

            //�y�`�`�`�z�L�������@���@�L�������y�`�`�`�z�ɂ��鏈��
            if (cbConvertText.Checked)
            {
                foreach (var v1 in data.charas.Keys.ToList())
                {
                    foreach (var v2 in data.charas[v1].Keys.ToList())
                    {
                        if (v2.StartsWith("�y") && v2.Contains('�z'))
                        {
                            string tmp = v2.Substring(0, v2.IndexOf('�z', 0) + 1);
                            string tmp2 = v2.Substring(v2.IndexOf('�z', 0) + 1);
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

            //�V���A���C�Y
            var str = JsonSerializer.Serialize(data, options);

            //�o�b�N�A�b�v
            File.Copy("UmaLibrary/UmaMusumeLibrary.json", "UmaLibrary/UmaMusumeLibrary_backup.json", true);

            using (StreamWriter sw = new StreamWriter("UmaLibrary/UmaMusumeLibrary.json"))
            {
                sw.Write(str);
            }

            MessageBox.Show("�ۑ����܂����B", "�ۑ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string text = "�u" + cbSkillName.Text + "�v" + btnHintLv.Text;
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
            AddTextToTextBox("�y�z", 1);
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
            //���ݑI�𒆂̃e�L�X�g�X�V
            if (cb1.SelectedIndex == (int)Kinds.KIND_CHARACTOR)
            {
                //�I����1
                if (string.IsNullOrEmpty(tbOption1.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 0)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = tbOption1.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = tbEffect1.Text;
                }
                //�I����2
                if (string.IsNullOrEmpty(tbOption2.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 1)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = tbOption2.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = tbEffect2.Text;
                }
                //�I����3
                if (string.IsNullOrEmpty(tbOption3.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 2)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].option = tbOption3.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].effect = tbEffect3.Text;
                }
                //�I����4
                if (string.IsNullOrEmpty(tbOption4.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 3)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].option = tbOption4.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].effect = tbEffect4.Text;
                }
                //�I����5
                if (string.IsNullOrEmpty(tbOption5.Text) == false)
                {
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count <= 4)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Add(new OptionEffect());
                    }

                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].option = tbOption5.Text;
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].effect = tbEffect5.Text;
                }

                //�󔒃`�F�b�N (OptionEffect������Ȃ�v�f�폜)
                if (string.IsNullOrEmpty(tbOption1.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect1.Text.Replace(" ", "")))
                {
                    //1�ڂ̑I�����͋󔒋�����Ȃ�
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = "�I����1";
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = "����1";
                }
                if (string.IsNullOrEmpty(tbOption2.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect2.Text.Replace(" ", "")))
                {
                    //2�ڂ̑I�����͋󔒋�����Ȃ�
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = "�I����2";
                    data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = "����2";
                }
                if (string.IsNullOrEmpty(tbOption3.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect3.Text.Replace(" ", "")))
                {
                    //���ɑI�������������ɃV�t�g
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
                    //���ɑI�������������ɃV�t�g
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3] = data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4];
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
                if (string.IsNullOrEmpty(tbOption5.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect5.Text.Replace(" ", "")))
                {
                    //�v�f�폜
                    if (data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.charas[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
            }
            else
            {
                //�I����1
                if (string.IsNullOrEmpty(tbOption1.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 0)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = tbOption1.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = tbEffect1.Text;
                }
                //�I����2
                if (string.IsNullOrEmpty(tbOption2.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 1)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = tbOption2.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = tbEffect2.Text;
                }
                //�I����3
                if (string.IsNullOrEmpty(tbOption3.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 2)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].option = tbOption3.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][2].effect = tbEffect3.Text;
                }
                //�I����4
                if (string.IsNullOrEmpty(tbOption4.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 3)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].option = tbOption4.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3].effect = tbEffect4.Text;
                }
                //�I����5
                if (string.IsNullOrEmpty(tbOption5.Text) == false &&
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count > 4)
                {
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].option = tbOption5.Text;
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4].effect = tbEffect5.Text;
                }

                //�󔒃`�F�b�N (OptionEffect������Ȃ�v�f�폜)
                if (string.IsNullOrEmpty(tbOption1.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect1.Text.Replace(" ", "")))
                {
                    //1�ڂ̑I�����͋󔒋�����Ȃ�
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].option = "�I����1";
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][0].effect = "����1";
                }
                if (string.IsNullOrEmpty(tbOption2.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect2.Text.Replace(" ", "")))
                {
                    //2�ڂ̑I�����͋󔒋�����Ȃ�
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].option = "�I����2";
                    data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][1].effect = "����2";
                }
                if (string.IsNullOrEmpty(tbOption3.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect3.Text.Replace(" ", "")))
                {
                    //���ɑI�������������ɃV�t�g
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
                    //���ɑI�������������ɃV�t�g
                    if (data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].Count == 5)
                    {
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][3] = data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text][4];
                        data.supports[cb2.Text][cbEdit3.Text].events[cbEdit4.SelectedIndex][cbEdit4.Text].RemoveAt(4);
                    }
                }
                if (string.IsNullOrEmpty(tbOption5.Text.Replace(" ", "")) && string.IsNullOrEmpty(tbEffect5.Text.Replace(" ", "")))
                {
                    //�v�f�폜
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