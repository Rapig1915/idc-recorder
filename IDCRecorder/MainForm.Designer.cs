namespace IDCRecorder
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
            this.components = new System.ComponentModel.Container();
            this.ScreenComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.QualityTextBox = new System.Windows.Forms.TextBox();
            this.VideoSizeCombobox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AudioOutputsComboBox = new System.Windows.Forms.ComboBox();
            this.AudioInputsComboBox = new System.Windows.Forms.ComboBox();
            this.ChkIsAudioOutEnabled = new System.Windows.Forms.CheckBox();
            this.ChkIsAudioInEnabled = new System.Windows.Forms.CheckBox();
            this.TimeStampTextBlock = new System.Windows.Forms.Label();
            this.StatusTextBlock = new System.Windows.Forms.Label();
            this.RecordButton = new System.Windows.Forms.Button();
            this.ErrorTextBlock = new System.Windows.Forms.Label();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.FramerateCombobox = new System.Windows.Forms.ComboBox();
            this.ChkIsAudioEnabled = new System.Windows.Forms.CheckBox();
            this.ChkIsMousePointerEnabled = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.GameScreenShotPicture = new System.Windows.Forms.PictureBox();
            this.ScreenshotButton = new System.Windows.Forms.Button();
            this.RefreshGameListButton = new System.Windows.Forms.Button();
            this.ProcessComboBox = new System.Windows.Forms.ComboBox();
            this.OutputResultTextBlock = new System.Windows.Forms.LinkLabel();
            this.TargetPanel = new System.Windows.Forms.Panel();
            this.SelectRecordGame = new System.Windows.Forms.RadioButton();
            this.SelectRecordDisplay = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DeleteTempFile = new System.Windows.Forms.Button();
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameScreenShotPicture)).BeginInit();
            this.TargetPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ScreenComboBox
            // 
            this.ScreenComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScreenComboBox.FormattingEnabled = true;
            this.ScreenComboBox.Location = new System.Drawing.Point(104, 55);
            this.ScreenComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ScreenComboBox.Name = "ScreenComboBox";
            this.ScreenComboBox.Size = new System.Drawing.Size(384, 28);
            this.ScreenComboBox.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Framerate";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(23, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Quality";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // QualityTextBox
            // 
            this.QualityTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mainFormBindingSource, "VideoQuality", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.QualityTextBox.Location = new System.Drawing.Point(104, 116);
            this.QualityTextBox.Name = "QualityTextBox";
            this.QualityTextBox.Size = new System.Drawing.Size(72, 27);
            this.QualityTextBox.TabIndex = 2;
            this.QualityTextBox.Text = "70";
            this.QualityTextBox.TextChanged += new System.EventHandler(this.QualityTextBox_TextChanged);
            // 
            // VideoSizeCombobox
            // 
            this.VideoSizeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoSizeCombobox.FormattingEnabled = true;
            this.VideoSizeCombobox.Location = new System.Drawing.Point(104, 51);
            this.VideoSizeCombobox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.VideoSizeCombobox.Name = "VideoSizeCombobox";
            this.VideoSizeCombobox.Size = new System.Drawing.Size(207, 28);
            this.VideoSizeCombobox.TabIndex = 0;
            this.VideoSizeCombobox.SelectedIndexChanged += new System.EventHandler(this.VideoSizeCombobox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(11, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "Video Size";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AudioOutputsComboBox
            // 
            this.AudioOutputsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioOutputsComboBox.FormattingEnabled = true;
            this.AudioOutputsComboBox.Location = new System.Drawing.Point(138, 219);
            this.AudioOutputsComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AudioOutputsComboBox.Name = "AudioOutputsComboBox";
            this.AudioOutputsComboBox.Size = new System.Drawing.Size(350, 28);
            this.AudioOutputsComboBox.TabIndex = 0;
            // 
            // AudioInputsComboBox
            // 
            this.AudioInputsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioInputsComboBox.FormattingEnabled = true;
            this.AudioInputsComboBox.Location = new System.Drawing.Point(138, 255);
            this.AudioInputsComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AudioInputsComboBox.Name = "AudioInputsComboBox";
            this.AudioInputsComboBox.Size = new System.Drawing.Size(350, 28);
            this.AudioInputsComboBox.TabIndex = 0;
            // 
            // ChkIsAudioOutEnabled
            // 
            this.ChkIsAudioOutEnabled.AutoSize = true;
            this.ChkIsAudioOutEnabled.Location = new System.Drawing.Point(30, 222);
            this.ChkIsAudioOutEnabled.Name = "ChkIsAudioOutEnabled";
            this.ChkIsAudioOutEnabled.Size = new System.Drawing.Size(94, 24);
            this.ChkIsAudioOutEnabled.TabIndex = 3;
            this.ChkIsAudioOutEnabled.Text = "Audio out";
            this.ChkIsAudioOutEnabled.UseVisualStyleBackColor = true;
            this.ChkIsAudioOutEnabled.CheckedChanged += new System.EventHandler(this.ChkIsAudioOutEnabled_CheckedChanged);
            // 
            // ChkIsAudioInEnabled
            // 
            this.ChkIsAudioInEnabled.AutoSize = true;
            this.ChkIsAudioInEnabled.Location = new System.Drawing.Point(30, 255);
            this.ChkIsAudioInEnabled.Name = "ChkIsAudioInEnabled";
            this.ChkIsAudioInEnabled.Size = new System.Drawing.Size(84, 24);
            this.ChkIsAudioInEnabled.TabIndex = 3;
            this.ChkIsAudioInEnabled.Text = "Audio in";
            this.ChkIsAudioInEnabled.UseVisualStyleBackColor = true;
            this.ChkIsAudioInEnabled.CheckedChanged += new System.EventHandler(this.ChkIsAudioInEnabled_CheckedChanged);
            // 
            // TimeStampTextBlock
            // 
            this.TimeStampTextBlock.AutoSize = true;
            this.TimeStampTextBlock.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeStampTextBlock.Location = new System.Drawing.Point(448, 477);
            this.TimeStampTextBlock.Name = "TimeStampTextBlock";
            this.TimeStampTextBlock.Size = new System.Drawing.Size(150, 47);
            this.TimeStampTextBlock.TabIndex = 5;
            this.TimeStampTextBlock.Text = "00:00:00";
            // 
            // StatusTextBlock
            // 
            this.StatusTextBlock.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusTextBlock.ForeColor = System.Drawing.Color.Crimson;
            this.StatusTextBlock.Location = new System.Drawing.Point(614, 482);
            this.StatusTextBlock.Name = "StatusTextBlock";
            this.StatusTextBlock.Size = new System.Drawing.Size(181, 37);
            this.StatusTextBlock.TabIndex = 5;
            this.StatusTextBlock.Text = "Idle";
            this.StatusTextBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(3, 41);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(279, 33);
            this.RecordButton.TabIndex = 4;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // ErrorTextBlock
            // 
            this.ErrorTextBlock.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorTextBlock.ForeColor = System.Drawing.Color.DarkRed;
            this.ErrorTextBlock.Location = new System.Drawing.Point(3, 556);
            this.ErrorTextBlock.Name = "ErrorTextBlock";
            this.ErrorTextBlock.Size = new System.Drawing.Size(1115, 31);
            this.ErrorTextBlock.TabIndex = 5;
            this.ErrorTextBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SettingsPanel.Controls.Add(this.ChkIsAudioInEnabled);
            this.SettingsPanel.Controls.Add(this.label1);
            this.SettingsPanel.Controls.Add(this.AudioOutputsComboBox);
            this.SettingsPanel.Controls.Add(this.AudioInputsComboBox);
            this.SettingsPanel.Controls.Add(this.FramerateCombobox);
            this.SettingsPanel.Controls.Add(this.VideoSizeCombobox);
            this.SettingsPanel.Controls.Add(this.ChkIsAudioOutEnabled);
            this.SettingsPanel.Controls.Add(this.ChkIsAudioEnabled);
            this.SettingsPanel.Controls.Add(this.QualityTextBox);
            this.SettingsPanel.Controls.Add(this.ChkIsMousePointerEnabled);
            this.SettingsPanel.Controls.Add(this.label7);
            this.SettingsPanel.Controls.Add(this.label18);
            this.SettingsPanel.Controls.Add(this.label5);
            this.SettingsPanel.Controls.Add(this.label6);
            this.SettingsPanel.Location = new System.Drawing.Point(12, 155);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(575, 314);
            this.SettingsPanel.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.label1.Location = new System.Drawing.Point(2, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "options...";
            // 
            // FramerateCombobox
            // 
            this.FramerateCombobox.FormattingEnabled = true;
            this.FramerateCombobox.Location = new System.Drawing.Point(104, 84);
            this.FramerateCombobox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.FramerateCombobox.Name = "FramerateCombobox";
            this.FramerateCombobox.Size = new System.Drawing.Size(72, 28);
            this.FramerateCombobox.TabIndex = 0;
            this.FramerateCombobox.TextChanged += new System.EventHandler(this.FramerateCombobox_TextChanged);
            // 
            // ChkIsAudioEnabled
            // 
            this.ChkIsAudioEnabled.AutoSize = true;
            this.ChkIsAudioEnabled.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.mainFormBindingSource, "IsAudioEnabled", true));
            this.ChkIsAudioEnabled.Location = new System.Drawing.Point(30, 189);
            this.ChkIsAudioEnabled.Name = "ChkIsAudioEnabled";
            this.ChkIsAudioEnabled.Size = new System.Drawing.Size(117, 24);
            this.ChkIsAudioEnabled.TabIndex = 3;
            this.ChkIsAudioEnabled.Text = "Record audio";
            this.ChkIsAudioEnabled.UseVisualStyleBackColor = true;
            this.ChkIsAudioEnabled.CheckedChanged += new System.EventHandler(this.ChkIsAudioEnabled_CheckedChanged);
            // 
            // ChkIsMousePointerEnabled
            // 
            this.ChkIsMousePointerEnabled.AutoSize = true;
            this.ChkIsMousePointerEnabled.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.mainFormBindingSource, "IsMousePointerEnabled", true));
            this.ChkIsMousePointerEnabled.Location = new System.Drawing.Point(30, 156);
            this.ChkIsMousePointerEnabled.Name = "ChkIsMousePointerEnabled";
            this.ChkIsMousePointerEnabled.Size = new System.Drawing.Size(164, 24);
            this.ChkIsMousePointerEnabled.TabIndex = 3;
            this.ChkIsMousePointerEnabled.Text = "Show mouse pointer";
            this.ChkIsMousePointerEnabled.UseVisualStyleBackColor = true;
            this.ChkIsMousePointerEnabled.Visible = false;
            this.ChkIsMousePointerEnabled.CheckedChanged += new System.EventHandler(this.ChkIsMousePointerEnabled_CheckedChanged);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(171, 88);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 20);
            this.label18.TabIndex = 1;
            this.label18.Text = "fps";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 47);
            this.label20.TabIndex = 5;
            this.label20.Text = "Record";
            // 
            // GameScreenShotPicture
            // 
            this.GameScreenShotPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GameScreenShotPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameScreenShotPicture.Location = new System.Drawing.Point(593, 12);
            this.GameScreenShotPicture.MaximumSize = new System.Drawing.Size(818, 457);
            this.GameScreenShotPicture.MinimumSize = new System.Drawing.Size(818, 457);
            this.GameScreenShotPicture.Name = "GameScreenShotPicture";
            this.GameScreenShotPicture.Size = new System.Drawing.Size(818, 457);
            this.GameScreenShotPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GameScreenShotPicture.TabIndex = 5;
            this.GameScreenShotPicture.TabStop = false;
            // 
            // ScreenshotButton
            // 
            this.ScreenshotButton.Location = new System.Drawing.Point(3, 2);
            this.ScreenshotButton.Name = "ScreenshotButton";
            this.ScreenshotButton.Size = new System.Drawing.Size(279, 33);
            this.ScreenshotButton.TabIndex = 4;
            this.ScreenshotButton.Text = "Take Screenshot";
            this.ScreenshotButton.UseVisualStyleBackColor = true;
            this.ScreenshotButton.Click += new System.EventHandler(this.ScreenshotButton_Click);
            // 
            // RefreshGameListButton
            // 
            this.RefreshGameListButton.Location = new System.Drawing.Point(501, 85);
            this.RefreshGameListButton.Name = "RefreshGameListButton";
            this.RefreshGameListButton.Size = new System.Drawing.Size(66, 33);
            this.RefreshGameListButton.TabIndex = 4;
            this.RefreshGameListButton.Text = "Refresh";
            this.RefreshGameListButton.UseVisualStyleBackColor = true;
            this.RefreshGameListButton.Click += new System.EventHandler(this.RefreshGameListButton_Click);
            // 
            // ProcessComboBox
            // 
            this.ProcessComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProcessComboBox.FormattingEnabled = true;
            this.ProcessComboBox.Location = new System.Drawing.Point(104, 88);
            this.ProcessComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ProcessComboBox.Name = "ProcessComboBox";
            this.ProcessComboBox.Size = new System.Drawing.Size(384, 28);
            this.ProcessComboBox.TabIndex = 0;
            // 
            // OutputResultTextBlock
            // 
            this.OutputResultTextBlock.Location = new System.Drawing.Point(3, 525);
            this.OutputResultTextBlock.Name = "OutputResultTextBlock";
            this.OutputResultTextBlock.Size = new System.Drawing.Size(1115, 28);
            this.OutputResultTextBlock.TabIndex = 8;
            this.OutputResultTextBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OutputResultTextBlock.Click += new System.EventHandler(this.Hyperlink_Click);
            // 
            // TargetPanel
            // 
            this.TargetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TargetPanel.Controls.Add(this.SelectRecordGame);
            this.TargetPanel.Controls.Add(this.ScreenComboBox);
            this.TargetPanel.Controls.Add(this.RefreshGameListButton);
            this.TargetPanel.Controls.Add(this.SelectRecordDisplay);
            this.TargetPanel.Controls.Add(this.ProcessComboBox);
            this.TargetPanel.Controls.Add(this.label20);
            this.TargetPanel.Location = new System.Drawing.Point(12, 12);
            this.TargetPanel.Name = "TargetPanel";
            this.TargetPanel.Size = new System.Drawing.Size(575, 137);
            this.TargetPanel.TabIndex = 9;
            // 
            // SelectRecordGame
            // 
            this.SelectRecordGame.AutoSize = true;
            this.SelectRecordGame.Location = new System.Drawing.Point(15, 90);
            this.SelectRecordGame.Name = "SelectRecordGame";
            this.SelectRecordGame.Size = new System.Drawing.Size(66, 24);
            this.SelectRecordGame.TabIndex = 0;
            this.SelectRecordGame.TabStop = true;
            this.SelectRecordGame.Text = "Game";
            this.SelectRecordGame.UseVisualStyleBackColor = true;
            this.SelectRecordGame.CheckedChanged += new System.EventHandler(this.OnRecordTargetChange);
            // 
            // SelectRecordDisplay
            // 
            this.SelectRecordDisplay.AutoSize = true;
            this.SelectRecordDisplay.Location = new System.Drawing.Point(15, 59);
            this.SelectRecordDisplay.Name = "SelectRecordDisplay";
            this.SelectRecordDisplay.Size = new System.Drawing.Size(76, 24);
            this.SelectRecordDisplay.TabIndex = 0;
            this.SelectRecordDisplay.TabStop = true;
            this.SelectRecordDisplay.Text = "Display";
            this.SelectRecordDisplay.UseVisualStyleBackColor = true;
            this.SelectRecordDisplay.CheckedChanged += new System.EventHandler(this.OnRecordTargetChange);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DeleteTempFile);
            this.panel1.Controls.Add(this.RecordButton);
            this.panel1.Controls.Add(this.ScreenshotButton);
            this.panel1.Location = new System.Drawing.Point(1124, 475);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 121);
            this.panel1.TabIndex = 10;
            // 
            // DeleteTempFile
            // 
            this.DeleteTempFile.Location = new System.Drawing.Point(3, 80);
            this.DeleteTempFile.Name = "DeleteTempFile";
            this.DeleteTempFile.Size = new System.Drawing.Size(279, 33);
            this.DeleteTempFile.TabIndex = 4;
            this.DeleteTempFile.Text = "Delete Temp Files";
            this.DeleteTempFile.UseVisualStyleBackColor = true;
            this.DeleteTempFile.Click += new System.EventHandler(this.DeleteTempFilesButton_Click);
            // 
            // mainFormBindingSource
            // 
            this.mainFormBindingSource.DataSource = typeof(IDCRecorder.MainForm);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1420, 599);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GameScreenShotPicture);
            this.Controls.Add(this.TargetPanel);
            this.Controls.Add(this.TimeStampTextBlock);
            this.Controls.Add(this.OutputResultTextBlock);
            this.Controls.Add(this.StatusTextBlock);
            this.Controls.Add(this.ErrorTextBlock);
            this.Controls.Add(this.SettingsPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximumSize = new System.Drawing.Size(1436, 638);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1436, 638);
            this.Name = "MainForm";
            this.Text = "IDCRecorder";
            this.SettingsPanel.ResumeLayout(false);
            this.SettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameScreenShotPicture)).EndInit();
            this.TargetPanel.ResumeLayout(false);
            this.TargetPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ScreenComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox QualityTextBox;
        private System.Windows.Forms.ComboBox VideoSizeCombobox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox AudioOutputsComboBox;
        private System.Windows.Forms.ComboBox AudioInputsComboBox;
        private System.Windows.Forms.CheckBox ChkIsAudioOutEnabled;
        private System.Windows.Forms.CheckBox ChkIsAudioInEnabled;
        private System.Windows.Forms.Label TimeStampTextBlock;
        private System.Windows.Forms.Label StatusTextBlock;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.Label ErrorTextBlock;
        private System.Windows.Forms.Panel SettingsPanel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.BindingSource mainFormBindingSource;
        private System.Windows.Forms.ComboBox ProcessComboBox;
        private System.Windows.Forms.Button RefreshGameListButton;
        private System.Windows.Forms.PictureBox GameScreenShotPicture;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.LinkLabel OutputResultTextBlock;
        private System.Windows.Forms.Button ScreenshotButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkIsAudioEnabled;
        private System.Windows.Forms.CheckBox ChkIsMousePointerEnabled;
        private System.Windows.Forms.Panel TargetPanel;
        private System.Windows.Forms.RadioButton SelectRecordGame;
        private System.Windows.Forms.RadioButton SelectRecordDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox FramerateCombobox;
        private System.Windows.Forms.Button DeleteTempFile;
    }
}

