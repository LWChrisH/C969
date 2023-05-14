namespace C969___Task_1
{
    partial class LogonForm
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
            this.logonButton = new System.Windows.Forms.Button();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.loginHeaderLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.languageLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.sampleDataButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logonButton
            // 
            this.logonButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logonButton.Location = new System.Drawing.Point(148, 132);
            this.logonButton.Name = "logonButton";
            this.logonButton.Size = new System.Drawing.Size(112, 23);
            this.logonButton.TabIndex = 0;
            this.logonButton.Text = "#logon";
            this.logonButton.UseVisualStyleBackColor = true;
            this.logonButton.Click += new System.EventHandler(this.logonButton_Click);
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBox.Location = new System.Drawing.Point(117, 79);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(143, 20);
            this.usernameTextBox.TabIndex = 1;
            // 
            // loginHeaderLabel
            // 
            this.loginHeaderLabel.AutoSize = true;
            this.loginHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.loginHeaderLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loginHeaderLabel.Location = new System.Drawing.Point(10, 9);
            this.loginHeaderLabel.Name = "loginHeaderLabel";
            this.loginHeaderLabel.Size = new System.Drawing.Size(142, 22);
            this.loginHeaderLabel.TabIndex = 2;
            this.loginHeaderLabel.Text = "#please #logon :";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.usernameLabel.Location = new System.Drawing.Point(11, 82);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(66, 13);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "#username :";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(117, 106);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(143, 20);
            this.passwordTextBox.TabIndex = 4;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.passwordLabel.Location = new System.Drawing.Point(11, 109);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(65, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "#password :";
            // 
            // languageLabel
            // 
            this.languageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.languageLabel.AutoSize = true;
            this.languageLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.languageLabel.Location = new System.Drawing.Point(11, 178);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(73, 13);
            this.languageLabel.TabIndex = 6;
            this.languageLabel.Text = "#languagetag";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "test/test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(107, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "chris/abc123";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sampleDataButton
            // 
            this.sampleDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sampleDataButton.Location = new System.Drawing.Point(117, 178);
            this.sampleDataButton.Name = "sampleDataButton";
            this.sampleDataButton.Size = new System.Drawing.Size(159, 23);
            this.sampleDataButton.TabIndex = 9;
            this.sampleDataButton.Text = "#sampledata";
            this.sampleDataButton.UseVisualStyleBackColor = true;
            this.sampleDataButton.Click += new System.EventHandler(this.sampleDataButton_Click);
            // 
            // LogonForm
            // 
            this.AcceptButton = this.logonButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 200);
            this.Controls.Add(this.sampleDataButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.loginHeaderLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.logonButton);
            this.MaximumSize = new System.Drawing.Size(291, 239);
            this.MinimumSize = new System.Drawing.Size(291, 239);
            this.Name = "LogonForm";
            this.Text = "#logon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button logonButton;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label loginHeaderLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button sampleDataButton;
    }
}