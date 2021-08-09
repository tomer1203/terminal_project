namespace TerminalProject
{
    partial class MainForm
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
            this.enterStringLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.stringRecieveRichTextBox = new TerminalProject.Source_files.DisabledRichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataRecieveRichTextBox = new System.Windows.Forms.RichTextBox();
            this.connectingLabel = new System.Windows.Forms.Label();
            this.configurationsButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialButton
            // 
            this.serialButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serialButton.Location = new System.Drawing.Point(523, 53);
            this.serialButton.Name = "serialButton";
            this.serialButton.Size = new System.Drawing.Size(109, 31);
            this.serialButton.TabIndex = 0;
            this.serialButton.Text = "Read Serial";
            this.serialButton.UseVisualStyleBackColor = true;
            this.serialButton.Click += new System.EventHandler(this.serialButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendButton.Location = new System.Drawing.Point(447, 53);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(61, 31);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // dataToSendTextBox
            // 
            this.dataToSendTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataToSendTextBox.Location = new System.Drawing.Point(19, 55);
            this.dataToSendTextBox.Name = "dataToSendTextBox";
            this.dataToSendTextBox.Size = new System.Drawing.Size(403, 26);
            this.dataToSendTextBox.TabIndex = 0;
            this.dataToSendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataToSendTextBox_KeyDown);
            // 
            // enterStringLabel
            // 
            this.enterStringLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enterStringLabel.Location = new System.Drawing.Point(15, 27);
            this.enterStringLabel.Name = "enterStringLabel";
            this.enterStringLabel.Size = new System.Drawing.Size(200, 25);
            this.enterStringLabel.TabIndex = 0;
            this.enterStringLabel.Text = "Enter String:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ItemSize = new System.Drawing.Size(238, 20);
            this.tabControl1.Location = new System.Drawing.Point(12, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(818, 464);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.stringRecieveRichTextBox);
            this.tabPage1.Controls.Add(this.sendButton);
            this.tabPage1.Controls.Add(this.serialButton);
            this.tabPage1.Controls.Add(this.enterStringLabel);
            this.tabPage1.Controls.Add(this.dataToSendTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(810, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // stringRecieveRichTextBox
            // 
            this.stringRecieveRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stringRecieveRichTextBox.Location = new System.Drawing.Point(6, 106);
            this.stringRecieveRichTextBox.Name = "stringRecieveRichTextBox";
            this.stringRecieveRichTextBox.Size = new System.Drawing.Size(798, 316);
            this.stringRecieveRichTextBox.TabIndex = 1;
            this.stringRecieveRichTextBox.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataRecieveRichTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(810, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataRecieveRichTextBox
            // 
            this.dataRecieveRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRecieveRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.dataRecieveRichTextBox.Name = "dataRecieveRichTextBox";
            this.dataRecieveRichTextBox.Size = new System.Drawing.Size(804, 430);
            this.dataRecieveRichTextBox.TabIndex = 0;
            this.dataRecieveRichTextBox.Text = "";
            // 
            // connectingLabel
            // 
            this.connectingLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.connectingLabel.Location = new System.Drawing.Point(0, 531);
            this.connectingLabel.Name = "connectingLabel";
            this.connectingLabel.Size = new System.Drawing.Size(850, 20);
            this.connectingLabel.TabIndex = 2;
            this.connectingLabel.Text = "Connecting...";
            this.connectingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // configurationsButton
            // 
            this.configurationsButton.Location = new System.Drawing.Point(12, 6);
            this.configurationsButton.Name = "configurationsButton";
            this.configurationsButton.Size = new System.Drawing.Size(95, 37);
            this.configurationsButton.TabIndex = 3;
            this.configurationsButton.Text = "Settings";
            this.configurationsButton.UseVisualStyleBackColor = true;
            this.configurationsButton.Click += new System.EventHandler(this.configurationButton_click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 551);
            this.Controls.Add(this.configurationsButton);
            this.Controls.Add(this.connectingLabel);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(515, 400);
            this.Name = "MainForm";
            this.Text = "Terminal Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Buttons
        private System.Windows.Forms.Button serialButton;
        private System.Windows.Forms.Button sendButton;

        // Text Box
        private System.Windows.Forms.TextBox dataToSendTextBox;
        private System.Windows.Forms.RichTextBox dataRecieveRichTextBox;
        private Source_files.DisabledRichTextBox stringRecieveRichTextBox;

        // Labels
        private System.Windows.Forms.Label enterStringLabel;
        private System.Windows.Forms.Label connectingLabel;

        // Tabs
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button configurationsButton;
    }
}

