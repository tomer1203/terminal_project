namespace TerminalProject
{
    partial class ConfigurationsForm
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
            this.setBaudrateLabel = new System.Windows.Forms.Label();
            this.setPortLabel = new System.Windows.Forms.Label();
            this.configurationsTitle = new System.Windows.Forms.Label();
            this.setPortcomboBox = new System.Windows.Forms.ComboBox();
            this.setBaudratecomboBox = new System.Windows.Forms.ComboBox();
            this.saveConfButton = new System.Windows.Forms.Button();
            this.saveConfErrorLabel = new System.Windows.Forms.Label();
            this.sendToMcucheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // setBaudrateLabel
            // 
            this.setBaudrateLabel.AutoSize = true;
            this.setBaudrateLabel.Location = new System.Drawing.Point(25, 126);
            this.setBaudrateLabel.Name = "setBaudrateLabel";
            this.setBaudrateLabel.Size = new System.Drawing.Size(79, 20);
            this.setBaudrateLabel.TabIndex = 0;
            this.setBaudrateLabel.Text = "Baudrate:";
            // 
            // setPortLabel
            // 
            this.setPortLabel.AutoSize = true;
            this.setPortLabel.Location = new System.Drawing.Point(62, 88);
            this.setPortLabel.Name = "setPortLabel";
            this.setPortLabel.Size = new System.Drawing.Size(42, 20);
            this.setPortLabel.TabIndex = 1;
            this.setPortLabel.Text = "Port:";
            // 
            // configurationsTitle
            // 
            this.configurationsTitle.AutoSize = true;
            this.configurationsTitle.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configurationsTitle.Location = new System.Drawing.Point(12, 34);
            this.configurationsTitle.Name = "configurationsTitle";
            this.configurationsTitle.Size = new System.Drawing.Size(244, 24);
            this.configurationsTitle.TabIndex = 2;
            this.configurationsTitle.Text = "Set Serial Port Configurations:";
            // 
            // setPortcomboBox
            // 
            this.setPortcomboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.setPortcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.setPortcomboBox.FormattingEnabled = true;
            this.setPortcomboBox.Location = new System.Drawing.Point(110, 82);
            this.setPortcomboBox.Name = "setPortcomboBox";
            this.setPortcomboBox.Size = new System.Drawing.Size(121, 28);
            this.setPortcomboBox.TabIndex = 3;
            // 
            // setBaudratecomboBox
            // 
            this.setBaudratecomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.setBaudratecomboBox.FormattingEnabled = true;
            this.setBaudratecomboBox.Location = new System.Drawing.Point(110, 123);
            this.setBaudratecomboBox.Name = "setBaudratecomboBox";
            this.setBaudratecomboBox.Size = new System.Drawing.Size(121, 28);
            this.setBaudratecomboBox.TabIndex = 4;
            // 
            // saveConfButton
            // 
            this.saveConfButton.BackColor = System.Drawing.SystemColors.MenuBar;
            this.saveConfButton.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.saveConfButton.ForeColor = System.Drawing.Color.Teal;
            this.saveConfButton.Location = new System.Drawing.Point(90, 206);
            this.saveConfButton.Name = "saveConfButton";
            this.saveConfButton.Size = new System.Drawing.Size(81, 35);
            this.saveConfButton.TabIndex = 5;
            this.saveConfButton.Text = " Save";
            this.saveConfButton.UseVisualStyleBackColor = false;
            // 
            // saveConfErrorLabel
            // 
            this.saveConfErrorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveConfErrorLabel.ForeColor = System.Drawing.Color.Tomato;
            this.saveConfErrorLabel.Location = new System.Drawing.Point(0, 263);
            this.saveConfErrorLabel.Name = "saveConfErrorLabel";
            this.saveConfErrorLabel.Size = new System.Drawing.Size(274, 20);
            this.saveConfErrorLabel.TabIndex = 7;
            this.saveConfErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sendToMcucheckBox
            // 
            this.sendToMcucheckBox.AutoSize = true;
            this.sendToMcucheckBox.Checked = true;
            this.sendToMcucheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sendToMcucheckBox.Location = new System.Drawing.Point(66, 167);
            this.sendToMcucheckBox.Name = "sendToMcucheckBox";
            this.sendToMcucheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.sendToMcucheckBox.Size = new System.Drawing.Size(128, 24);
            this.sendToMcucheckBox.TabIndex = 8;
            this.sendToMcucheckBox.Text = "send to MCU\r\n";
            this.sendToMcucheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(274, 283);
            this.Controls.Add(this.sendToMcucheckBox);
            this.Controls.Add(this.saveConfErrorLabel);
            this.Controls.Add(this.saveConfButton);
            this.Controls.Add(this.setBaudratecomboBox);
            this.Controls.Add(this.setPortcomboBox);
            this.Controls.Add(this.configurationsTitle);
            this.Controls.Add(this.setPortLabel);
            this.Controls.Add(this.setBaudrateLabel);
            this.KeyPreview = true;
            this.Name = "ConfigurationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal Projet";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.saveConfButton_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label setBaudrateLabel;
        private System.Windows.Forms.Label setPortLabel;
        private System.Windows.Forms.Label configurationsTitle;
        private System.Windows.Forms.ComboBox setPortcomboBox;
        private System.Windows.Forms.ComboBox setBaudratecomboBox;
        private System.Windows.Forms.Button saveConfButton;
        private System.Windows.Forms.Label saveConfErrorLabel;
        private System.Windows.Forms.CheckBox sendToMcucheckBox;
    }
}