using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using WindowsDisplayAPI;

namespace IDCRecorder
{
    public partial class MainForm : Form, INotifyPropertyChanged
    {
        private Recorder _rec;
        private GameRecorder _rec_game;
        private DispatcherTimer _progressTimer;
        private int _secondsElapsed;
        private Stream _outputStream;

        private bool _isRecording;
        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                _isRecording = value;
            }
        }

        #region "Video Record Options"
        public int VideoBitrate { get; set; } = 7000;
        public int VideoFramerate { get; set; } = 60;
        public int VideoQuality { get; set; } = 70;
        public bool IsAudioEnabled { get; set; } = true;
        public bool IsMousePointerEnabled { get; set; } = true;
        public bool IsFixedFramerate { get; set; } = false;
        public bool IsThrottlingDisabled { get; set; } = false;
        public bool IsHardwareEncodingEnabled { get; set; } = true;
        public bool IsLowLatencyEnabled { get; set; } = false;
        public bool IsMp4FastStartEnabled { get; set; } = false;
        public bool IsMouseClicksDetected { get; set; } = false;
        public string MouseLeftClickColor { get; set; } = "#ffff00";
        public string MouseRightClickColor { get; set; } = "#ffff00";
        public int MouseClickRadius { get; set; } = 20;
        public int MouseClickDuration { get; set; } = 50;
        public List<string> AudioInputsList { get; set; } = new List<string>();
        public List<string> AudioOutputsList { get; set; } = new List<string>();
        public bool IsAudioInEnabled { get; set; } = false;
        public bool IsAudioOutEnabled { get; set; } = true;

        public int OutputVideoWidth { get; set; } = 0;
        public int OutputVideoHeight { get; set; } = 0;

        private bool _recordToStream = false;
        public bool RecordToStream
        {
            get { return _recordToStream; }
            set
            {
                if (_recordToStream != value)
                {
                    _recordToStream = value;
                    RaisePropertyChanged("RecordToStream");
                }
            }
        }

        private RecorderMode _currentRecordingMode = RecorderMode.Video;
        public RecorderMode CurrentRecordingMode
        {
            get { return _currentRecordingMode; }
            set
            {
                if (_currentRecordingMode != value)
                {
                    _currentRecordingMode = value;
                    RaisePropertyChanged("CurrentRecordingMode");
                }
            }
        }

        private BitrateControlMode _currentVideoBitrateMode = BitrateControlMode.Quality;
        public BitrateControlMode CurrentVideoBitrateMode
        {
            get { return _currentVideoBitrateMode; }
            set
            {
                if (_currentVideoBitrateMode != value)
                {
                    _currentVideoBitrateMode = value;
                    RaisePropertyChanged("CurrentVideoBitrateMode");
                }
            }
        }

        private ImageFormat _currentImageFormat = ImageFormat.BMP;
        public ImageFormat CurrentImageFormat
        {
            get { return _currentImageFormat; }
            set
            {
                if (_currentImageFormat != value)
                {
                    _currentImageFormat = value;
                    RaisePropertyChanged("CurrentImageFormat");
                }
            }
        }

        private MouseDetectionMode _currentMouseDetectionMode = MouseDetectionMode.Hook;
        public MouseDetectionMode CurrentMouseDetectionMode
        {
            get { return _currentMouseDetectionMode; }
            set
            {
                if (_currentMouseDetectionMode != value)
                {
                    _currentMouseDetectionMode = value;
                    RaisePropertyChanged("CurrentMouseDetectionMode");
                }
            }
        }

        public H264Profile CurrentH264Profile { get; set; } = H264Profile.Main;

        public Size[] OutputVideoSizes = {
            new Size(0, 0),
            new Size(640, 360),
            new Size(640, 480),
            new Size(800, 450),
            new Size(800, 600),
            new Size(960, 540),
            new Size(960, 720),
            new Size(1024, 576),
            new Size(1024, 768),
            new Size(1280, 720),
            new Size(1280, 960),
            new Size(1600, 900),
            new Size(1600, 1200),
            new Size(1920, 1080),
            new Size(1920, 1440),
            new Size(2048, 1152),
            new Size(2048, 1536),
            new Size(2560, 1440),
            new Size(2560, 1920)
        };

        public int[] OutputFrameRates = { 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 100, 110, 120 };
        #endregion

        public MainForm()
        {
            InitializeComponent();

            // Init check boxes and text boxes
            ChkIsMousePointerEnabled.Checked = IsMousePointerEnabled;
            ChkIsAudioEnabled.Checked = IsAudioEnabled;
            ChkIsAudioOutEnabled.Checked = IsAudioOutEnabled;
            ChkIsAudioInEnabled.Checked = IsAudioInEnabled;

            // Init Screen List
            foreach (var target in WindowsDisplayAPI.Display.GetDisplays())
            {
                this.ScreenComboBox.Items.Add(target);
            }

            AudioOutputsList.Add("Default playback device");
            AudioInputsList.Add("Default recording device");
            AudioOutputsList.AddRange(Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices));
            AudioInputsList.AddRange(Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices));

            AudioOutputsComboBox.DataSource = AudioOutputsList;
            AudioInputsComboBox.DataSource = AudioInputsList;

            ScreenComboBox.SelectedIndex = 0;
            AudioOutputsComboBox.SelectedIndex = 0;
            AudioInputsComboBox.SelectedIndex = 0;

            //RecordingModeComboBox.DataSource = Enum.GetValues(typeof(RecorderMode)).Cast<RecorderMode>();
            //RecordingBitrateModeComboBox.DataSource = Enum.GetValues(typeof(BitrateControlMode)).Cast<BitrateControlMode>();
            //VideoProfileComboBox.DataSource = Enum.GetValues(typeof(H264Profile)).Cast<H264Profile>();
            //VideoSizeCombobox.DataSource = Enum.GetValues(typeof(MouseDetectionMode)).Cast<MouseDetectionMode>();

            ProcessComboBox.ValueMember = "Value";
            ProcessComboBox.DisplayMember = "Text";

            // Init Video Size Combobox
            foreach(var sz in OutputVideoSizes)
            {
                VideoSizeCombobox.Items.Add(new { Text = sz.Width>0?$"{sz.Width}x{sz.Height}":"Original", Value = sz });
            }
            VideoSizeCombobox.ValueMember = "Value";
            VideoSizeCombobox.DisplayMember = "Text";
            if (VideoSizeCombobox.Items.Count > 0)
                VideoSizeCombobox.SelectedIndex = 0;

            // Init framerates combobox
            foreach(var fps in OutputFrameRates)
            {
                FramerateCombobox.Items.Add(fps.ToString());
            }
            FramerateCombobox.Text = VideoFramerate.ToString();
            QualityTextBox.Text = VideoQuality.ToString();

            SelectRecordDisplay.Checked = true;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string GetImageExtension()
        {
            switch (CurrentImageFormat)
            {
                case ImageFormat.JPEG:
                    return ".jpg";
                case ImageFormat.TIFF:
                    return ".tiff";
                case ImageFormat.BMP:
                    return ".bmp";
                default:
                case ImageFormat.PNG:
                    return ".png";
            }
        }
        private void CleanupResources()
        {
            _outputStream?.Flush();
            _outputStream?.Dispose();
            _outputStream = null;

            _progressTimer?.Stop();
            _progressTimer = null;
            _secondsElapsed = 0;

            _rec?.Dispose();
            _rec = null;
        }
        private void Hyperlink_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(this.OutputResultTextBlock.Text);
            }
            catch (Exception) { }
        }
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void _progressTimer_Tick(object sender, EventArgs e)
        {
            _secondsElapsed++;
            UpdateProgress();
        }
        private void UpdateProgress()
        {
            TimeStampTextBlock.Text = TimeSpan.FromSeconds(_secondsElapsed).ToString();
        }
        private void OnRecordTargetChange(object sender, EventArgs e)
        { 
            if(SelectRecordDisplay.Checked)
            {
                ScreenComboBox.Enabled = true;
                ProcessComboBox.Enabled = false;
                RefreshGameListButton.Enabled = false;
            }
            else
            {
                ScreenComboBox.Enabled = false;
                ProcessComboBox.Enabled = true;
                RefreshGameListButton.Enabled = true;
                RefreshGameListButton_Click(null, null);
            }
        }
        private void DeleteTempFilesButton_Click(object sender, EventArgs e)
        {
            try
            {
                Helper.DeleteTempFiles();
                MessageBox.Show("Temp files deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while deleting files: " + ex.Message);
            }
        }

        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            this.BeginInvoke((Action)(() =>
            {
                RecordButton.Text = "Record";
                RecordButton.Enabled = true;
                ScreenshotButton.Enabled = true;
                StatusTextBlock.Text = "Error";
                ErrorTextBlock.Visible = true;
                ErrorTextBlock.Text = e.Error;
                IsRecording = false;
                CleanupResources();
            }));
        }
        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            this.BeginInvoke((Action)(() =>
            {
                string filePath = e.FilePath;
                if (RecordToStream)
                {
                    filePath = ((FileStream)_outputStream)?.Name;
                }

                OutputResultTextBlock.Text = filePath;
                RecordButton.Text = "Record";
                RecordButton.Enabled = true;
                ScreenshotButton.Enabled = true;
                this.StatusTextBlock.Text = "Completed";
                IsRecording = false;
                CleanupResources();

                if (filePath.Contains("Shot"))
                    GameScreenShotPicture.ImageLocation = filePath;
            }));
        }
        private void _rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            this.BeginInvoke((Action)(() =>
            {
                ErrorTextBlock.Visible = false;
                switch (e.Status)
                {
                    case RecorderStatus.Idle:
                        this.StatusTextBlock.Text = "Idle";
                        this.SettingsPanel.Enabled = true;
                        this.TargetPanel.Enabled = true;
                        this.ScreenshotButton.Enabled = true;
                        break;
                    case RecorderStatus.Recording:
                        if (_progressTimer != null)
                            _progressTimer.IsEnabled = true;
                        RecordButton.Text = "Stop";
                        this.StatusTextBlock.Text = "Recording";
                        this.SettingsPanel.Enabled = false;
                        this.TargetPanel.Enabled = false;
                        this.ScreenshotButton.Enabled = false;
                        break;
                    case RecorderStatus.Paused:
                        if (_progressTimer != null)
                            _progressTimer.IsEnabled = false;
                        this.StatusTextBlock.Text = "Paused";
                        break;
                    case RecorderStatus.Finishing:
                        this.StatusTextBlock.Text = "Finalizing video";
                        break;
                    default:
                        break;
                }
            }));
        }

        private void ScreenshotButton_Click(object sender, EventArgs e)
        {
            string targetImage = "";

            if (SelectRecordDisplay.Checked)
            {
                targetImage = Helper.GetNextFileName("ScreenShot", GetImageExtension());               
                RecorderOptions options = GetScreenRecordOption();
                options.RecorderMode = RecorderMode.Snapshot;
                Recorder shoter = Recorder.CreateRecorder(options);
                shoter.OnRecordingComplete += Rec_OnRecordingComplete;
                shoter.Record(targetImage);
            }
            else
            {
                if (ProcessComboBox.SelectedIndex == -1)
                    return;

                GameProcessInfo game = (GameProcessInfo)((dynamic)ProcessComboBox.SelectedItem).Value;
                if (game != null)
                {
                    targetImage = Helper.GetNextFileName("GameShot");
                    if (GameRecorder.ShotGame(Path.GetFileName(game.ExeFilePath), targetImage) == 1)
                    {
                        targetImage += "_0.bmp";
                    }
                }
            }

            // If game screenshot mode => wait till it is loaded
            // otherwise in Screenshot mode => handler does it ;)
            if (!SelectRecordDisplay.Checked)
            {
                if (!File.Exists(targetImage))
                    Thread.Sleep(100);

                if (File.Exists(targetImage))
                    GameScreenShotPicture.ImageLocation = targetImage;

                Rec_OnRecordingComplete(null, new RecordingCompleteEventArgs(targetImage, null));
            }
        }
        private void RefreshGameListButton_Click(object sender, EventArgs e)
        {
            ProcessComboBox.Items.Clear();

            List<GameProcessInfo> processes = GameRecorder.LoadGameList();
            foreach(var process in processes)
            {
                ProcessComboBox.Items.Add(new { Text = process.WindowTitle, Value = process });
            }
            if (ProcessComboBox.SelectedIndex == -1 && processes.Count > 0)
                ProcessComboBox.SelectedIndex = 0;
        }

        private RecorderOptions GetScreenRecordOption()
        {
            Display selectedDisplay = (Display)this.ScreenComboBox.SelectedItem;

            var audioOutputDevice = (IsAudioEnabled && IsAudioOutEnabled && AudioOutputsComboBox.SelectedIndex >= 0) ? AudioOutputsComboBox.SelectedItem.ToString() : string.Empty;
            var audioInputDevice = (IsAudioEnabled && IsAudioInEnabled && AudioInputsComboBox.SelectedIndex >= 0) ? AudioInputsComboBox.SelectedItem.ToString() : string.Empty;

            return new RecorderOptions
            {
                RecorderMode = CurrentRecordingMode,
                IsThrottlingDisabled = this.IsThrottlingDisabled,
                IsHardwareEncodingEnabled = this.IsHardwareEncodingEnabled,
                IsLowLatencyEnabled = this.IsLowLatencyEnabled,
                IsMp4FastStartEnabled = this.IsMp4FastStartEnabled,
                AudioOptions = new AudioOptions
                {
                    Bitrate = AudioBitrate.bitrate_96kbps,
                    Channels = AudioChannels.Stereo,
                    IsAudioEnabled = this.IsAudioEnabled,
                    IsOutputDeviceEnabled = IsAudioOutEnabled,
                    IsInputDeviceEnabled = IsAudioInEnabled,
                    AudioOutputDevice = audioOutputDevice,
                    AudioInputDevice = audioInputDevice
                },
                VideoOptions = new VideoOptions
                {
                    OutputWidth = OutputVideoWidth,
                    OutputHeight = OutputVideoHeight,
                    BitrateMode = this.CurrentVideoBitrateMode,
                    Bitrate = VideoBitrate * 1000,
                    Framerate = this.VideoFramerate,
                    Quality = this.VideoQuality,
                    IsFixedFramerate = this.IsFixedFramerate,
                    EncoderProfile = this.CurrentH264Profile,
                    SnapshotFormat = CurrentImageFormat
                },
                DisplayOptions = new DisplayOptions(selectedDisplay.DisplayName, 0, 0, 0, 0),
                MouseOptions = new MouseOptions
                {
                    IsMouseClicksDetected = this.IsMouseClicksDetected,
                    IsMousePointerEnabled = this.IsMousePointerEnabled,
                    MouseClickDetectionColor = this.MouseLeftClickColor,
                    MouseRightClickDetectionColor = this.MouseRightClickColor,
                    MouseClickDetectionRadius = this.MouseClickRadius,
                    MouseClickDetectionDuration = this.MouseClickDuration,
                    MouseClickDetectionMode = this.CurrentMouseDetectionMode
                }
            };
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            // If target is display
            if (SelectRecordDisplay.Checked)
            {
                if (IsRecording)
                {
                    _rec.Stop();
                    _progressTimer?.Stop();
                    _progressTimer = null;
                    _secondsElapsed = 0;
                    RecordButton.Enabled = false;
                    return;
                }

                OutputResultTextBlock.Text = "";
                UpdateProgress();
                string videoPath = "";

                // we always use Video Mode
                if (CurrentRecordingMode == RecorderMode.Video)
                {
                    videoPath = Helper.GetNextFileName("ScreenRecoder", ".mp4");
                }
                _progressTimer = new DispatcherTimer();
                _progressTimer.Tick += _progressTimer_Tick;
                _progressTimer.Interval = TimeSpan.FromSeconds(1);
                _progressTimer.Start();
                
                RecorderOptions options = GetScreenRecordOption();

                if (_rec == null)
                {
                    _rec = Recorder.CreateRecorder(options);
                    _rec.OnRecordingComplete += Rec_OnRecordingComplete;
                    _rec.OnRecordingFailed += Rec_OnRecordingFailed;
                    _rec.OnStatusChanged += _rec_OnStatusChanged;
                }
                else
                {
                    _rec.SetOptions(options);
                }
                if (RecordToStream)
                {
                    _outputStream = new FileStream(videoPath, FileMode.Create);
                    _rec.Record(_outputStream);
                }
                else
                {
                    _rec.Record(videoPath);
                }
                _secondsElapsed = 0;
                IsRecording = true;
            }
            else // Target is Game
            {
                if(IsRecording)
                {
                    if(_rec_game != null)
                    {
                        _rec_game.Stop();
                        OutputResultTextBlock.Text = _rec_game.m_VideoPath;
                    }
                    _progressTimer?.Stop();
                    _progressTimer = null;
                    _secondsElapsed = 0;

                    SettingsPanel.Enabled = true;
                    TargetPanel.Enabled = true;
                    ScreenshotButton.Enabled = true;
                    RecordButton.Text = "Record";
                    StatusTextBlock.Text = "Idle";
                    IsRecording = false;
                    return;
                }

                if (ProcessComboBox.SelectedIndex == -1)
                    return;

                GameProcessInfo game = (GameProcessInfo)((dynamic)ProcessComboBox.SelectedItem).Value;
                string targetVideo = Helper.GetNextFileName("GameRecorder");

                if (_rec_game == null)
                    _rec_game = new GameRecorder();

                if(_rec_game.Start(Path.GetFileName(game.ExeFilePath), targetVideo, GetScreenRecordOption()) == 0)
                {
                    _progressTimer = new DispatcherTimer();
                    _progressTimer.Tick += _progressTimer_Tick;
                    _progressTimer.Interval = TimeSpan.FromSeconds(1);
                    _progressTimer.Start();

                    StatusTextBlock.Text = "Recording";
                    OutputResultTextBlock.Text = "";
                    ErrorTextBlock.Text = "";

                    SettingsPanel.Enabled = false;
                    TargetPanel.Enabled = false;
                    ScreenshotButton.Enabled = false;
                    RecordButton.Text = "Stop";
                    IsRecording = true;
                }
            }
        }

        private void VideoSizeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sz = (Size)((dynamic)VideoSizeCombobox.SelectedItem).Value;
            OutputVideoWidth = sz.Width;
            OutputVideoHeight = sz.Height;
        }

        private void FramerateCombobox_TextChanged(object sender, EventArgs e)
        {
            int fps = VideoFramerate;
            Int32.TryParse(FramerateCombobox.Text, out fps);
            VideoFramerate = fps;
        }

        private void QualityTextBox_TextChanged(object sender, EventArgs e)
        {
            int quality = VideoQuality;
            Int32.TryParse(QualityTextBox.Text, out quality);
            VideoQuality = quality;
        }

        private void ChkIsMousePointerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            IsMousePointerEnabled = ChkIsMousePointerEnabled.Checked;
        }

        private void ChkIsAudioEnabled_CheckedChanged(object sender, EventArgs e)
        {
            IsAudioEnabled = ChkIsAudioEnabled.Checked;
        }

        private void ChkIsAudioOutEnabled_CheckedChanged(object sender, EventArgs e)
        {
            IsAudioOutEnabled = ChkIsAudioOutEnabled.Checked;
        }

        private void ChkIsAudioInEnabled_CheckedChanged(object sender, EventArgs e)
        {
            IsAudioInEnabled = ChkIsAudioInEnabled.Checked;
        }
    }
}
