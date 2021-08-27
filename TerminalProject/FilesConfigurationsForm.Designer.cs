namespace TerminalProject
{
    partial class FilesConfigurationsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.setFilesLibraryTextBox = new System.Windows.Forms.TextBox();
            this.saveFilesLibraryButton = new System.Windows.Forms.Button();
            this.saveConfErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(75, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set script files library ";
            // 
            // setFilesLibraryTextBox
            // 
            this.setFilesLibraryTextBox.Location = new System.Drawing.Point(12, 119);
            this.setFilesLibraryTextBox.Name = "setFilesLibraryTextBox";
            this.setFilesLibraryTextBox.Size = new System.Drawing.Size(322, 26);
            this.setFilesLibraryTextBox.TabIndex = 1;
            // 
            // saveFilesLibraryButton
            // 
            this.saveFilesLibraryButton.ForeColor = System.Drawing.Color.Teal;
            this.saveFilesLibraryButton.Location = new System.Drawing.Point(125, 230);
            this.saveFilesLibraryButton.Name = "saveFilesLibraryButton";
            this.saveFilesLibraryButton.Size = new System.Drawing.Size(75, 36);
            this.saveFilesLibraryButton.TabIndex = 2;
            this.saveFilesLibraryButton.Text = "Save";
            this.saveFilesLibraryButton.UseVisualStyleBackColor = true;
            this.saveFilesLibraryButton.Click += new System.EventHandler(this.saveFilesLibraryButton_Click);
            // 
            // saveConfErrorLabel
            // 
            this.saveConfErrorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveConfErrorLabel.ForeColor = System.Drawing.Color.Tomato;
            this.saveConfErrorLabel.Location = new System.Drawing.Point(0, 325);
            this.saveConfErrorLabel.Name = "saveConfErrorLabel";
            this.saveConfErrorLabel.Size = new System.Drawing.Size(346, 20);
            this.saveConfErrorLabel.TabIndex = 16;
            this.saveConfErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FilesConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 345);
            this.Controls.Add(this.saveConfErrorLabel);
            this.Controls.Add(this.saveFilesLibraryButton);
            this.Controls.Add(this.setFilesLibraryTextBox);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "FilesConfigurationsForm";
            this.Text = "Final Project";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.saveConfButton_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox setFilesLibraryTextBox;
        private System.Windows.Forms.Button saveFilesLibraryButton;
        private System.Windows.Forms.Label saveConfErrorLabel;
    }
}