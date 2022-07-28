namespace UmaUmaEditor
{
    partial class UmaUmaEditor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb4 = new System.Windows.Forms.ComboBox();
            this.cb3 = new System.Windows.Forms.ComboBox();
            this.cb2 = new System.Windows.Forms.ComboBox();
            this.cb1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(307, 412);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(174, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb4);
            this.groupBox1.Controls.Add(this.cb3);
            this.groupBox1.Controls.Add(this.cb2);
            this.groupBox1.Controls.Add(this.cb1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 394);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "編集";
            // 
            // cb4
            // 
            this.cb4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb4.FormattingEnabled = true;
            this.cb4.Location = new System.Drawing.Point(130, 109);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(242, 23);
            this.cb4.TabIndex = 4;
            // 
            // cb3
            // 
            this.cb3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb3.FormattingEnabled = true;
            this.cb3.Location = new System.Drawing.Point(87, 80);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(285, 23);
            this.cb3.TabIndex = 3;
            this.cb3.SelectedIndexChanged += new System.EventHandler(this.cb3_SelectedIndexChanged);
            // 
            // cb2
            // 
            this.cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb2.FormattingEnabled = true;
            this.cb2.Location = new System.Drawing.Point(45, 51);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(327, 23);
            this.cb2.TabIndex = 2;
            this.cb2.SelectedIndexChanged += new System.EventHandler(this.cb2_SelectedIndexChanged);
            // 
            // cb1
            // 
            this.cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb1.FormattingEnabled = true;
            this.cb1.Location = new System.Drawing.Point(6, 22);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(366, 23);
            this.cb1.TabIndex = 0;
            this.cb1.SelectedIndexChanged += new System.EventHandler(this.cb1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(396, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 394);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "追加";
            // 
            // UmaUmaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UmaUmaEditor";
            this.Text = "UmaUmaEditor";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnSave;
        private GroupBox groupBox1;
        private ComboBox cb4;
        private ComboBox cb3;
        private ComboBox cb2;
        private ComboBox cb1;
        private GroupBox groupBox2;
    }
}