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
            this.SuspendLayout();
            // 
            // setBaudrateLabel
            // 
            this.setBaudrateLabel.AutoSize = true;
            this.setBaudrateLabel.Location = new System.Drawing.Point(12, 131);
            this.setBaudrateLabel.Name = "setBaudrateLabel";
            this.setBaudrateLabel.Size = new System.Drawing.Size(108, 20);
            this.setBaudrateLabel.TabIndex = 0;
            this.setBaudrateLabel.Text = "Set Baudrate:";
            // 
            // setPortLabel
            // 
            this.setPortLabel.AutoSize = true;
            this.setPortLabel.Location = new System.Drawing.Point(49, 93);
            this.setPortLabel.Name = "setPortLabel";
            this.setPortLabel.Size = new System.Drawing.Size(71, 20);
            this.setPortLabel.TabIndex = 1;
            this.setPortLabel.Text = "Set Port:";
            // 
            // configurationsTitle
            // 
            this.configurationsTitle.AutoSize = true;
            this.configurationsTitle.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configurationsTitle.Location = new System.Drawing.Point(58, 45);
            this.configurationsTitle.Name = "configurationsTitle";
            this.configurationsTitle.Size = new System.Drawing.Size(154, 20);
            this.configurationsTitle.TabIndex = 2;
            this.configurationsTitle.Text = "Set Uart Configurations";
            // 
            // setPortcomboBox
            // 
            this.setPortcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.setPortcomboBox.FormattingEnabled = true;
            this.setPortcomboBox.Location = new System.Drawing.Point(135, 90);
            this.setPortcomboBox.Name = "setPortcomboBox";
            this.setPortcomboBox.Size = new System.Drawing.Size(121, 28);
            this.setPortcomboBox.TabIndex = 3;
            // 
            // setBaudratecomboBox
            // 
            this.setBaudratecomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.setBaudratecomboBox.FormattingEnabled = true;
            this.setBaudratecomboBox.Location = new System.Drawing.Point(135, 131);
            this.setBaudratecomboBox.Name = "setBaudratecomboBox";
            this.setBaudratecomboBox.Size = new System.Drawing.Size(121, 28);
            this.setBaudratecomboBox.TabIndex = 4;
            // 
            // saveConfButton
            // 
            this.saveConfButton.Location = new System.Drawing.Point(94, 188);
            this.saveConfButton.Name = "saveConfButton";
            this.saveConfButton.Size = new System.Drawing.Size(81, 35);
            this.saveConfButton.TabIndex = 5;
            this.saveConfButton.Text = " Save";
            this.saveConfButton.UseVisualStyleBackColor = true;
            this.saveConfButton.Click += new System.EventHandler(this.saveConfButton_Click);
            // 
            // saveConfErrorLabel
            // 
            this.saveConfErrorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveConfErrorLabel.Location = new System.Drawing.Point(0, 263);
            this.saveConfErrorLabel.Name = "saveConfErrorLabel";
            this.saveConfErrorLabel.Size = new System.Drawing.Size(274, 20);
            this.saveConfErrorLabel.TabIndex = 7;
            this.saveConfErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 283);
            this.Controls.Add(this.saveConfErrorLabel);
            this.Controls.Add(this.saveConfButton);
            this.Controls.Add(this.setBaudratecomboBox);
            this.Controls.Add(this.setPortcomboBox);
            this.Controls.Add(this.configurationsTitle);
            this.Controls.Add(this.setPortLabel);
            this.Controls.Add(this.setBaudrateLabel);
            this.Name = "ConfigurationsForm";
            this.Text = "configurationsFormcs";
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
    }
}