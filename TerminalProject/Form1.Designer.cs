namespace TerminalProject
{
    partial class Form1
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serialButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.dataToSendTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataRecieveRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialButton
            // 
            this.serialButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serialButton.Location = new System.Drawing.Point(398, 162);
            this.serialButton.Name = "serialButton";
            this.serialButton.Size = new System.Drawing.Size(175, 123);
            this.serialButton.TabIndex = 0;
            this.serialButton.Text = "Read Serial";
            this.serialButton.UseVisualStyleBackColor = true;
            this.serialButton.Click += new System.EventHandler(this.serialButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendButton.Location = new System.Drawing.Point(170, 162);
            this.sendButton.Name = "button2";
            this.sendButton.Size = new System.Drawing.Size(175, 123);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // dataToSendTextBox
            // 
            this.dataToSendTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataToSendTextBox.Location = new System.Drawing.Point(170, 75);
            this.dataToSendTextBox.Name = "dataToSendTextBox";
            this.dataToSendTextBox.Size = new System.Drawing.Size(403, 26);
            this.dataToSendTextBox.TabIndex = 0;
            this.dataToSendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataToSendTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(166, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Some Text";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ItemSize = new System.Drawing.Size(238, 20);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(720, 400);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sendButton);
            this.tabPage1.Controls.Add(this.serialButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataToSendTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(712, 372);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataRecieveRichTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(712, 372);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataRecieveRichTextBox
            // 
            this.dataRecieveRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRecieveRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.dataRecieveRichTextBox.Name = "dataRecieveRichTextBox";
            this.dataRecieveRichTextBox.Size = new System.Drawing.Size(706, 366);
            this.dataRecieveRichTextBox.TabIndex = 0;
            this.dataRecieveRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 421);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Connecting...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(515, 400);
            this.Name = "Form1";
            this.Text = "Terminal Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Buttons
        private System.Windows.Forms.Button serialButton;
        private System.Windows.Forms.Button sendButton;

        // Text Box
        private System.Windows.Forms.TextBox dataToSendTextBox;
        private System.Windows.Forms.RichTextBox dataRecieveRichTextBox;

        // Labels
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        // Tabs
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        
    }
}

