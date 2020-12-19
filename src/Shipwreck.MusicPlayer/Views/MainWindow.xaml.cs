using Shipwreck.MusicPlayer.ViewModels;
using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace Shipwreck.MusicPlayer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MediaPlayer _Player;
        private DispatcherTimer _PlayerTimer;

        internal void Play(MusicViewModel music)
        {
            if (_Player == null)
            {
                _Player = new MediaPlayer();
                _PlayerTimer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(0.25)
                };
                _PlayerTimer.Tick += _PlayerTimer_Tick;
            }
            var u = new Uri(music.FullPath);
            if (_Player.Source != u)
            {
                _Player.Open(u);
            }
            _Player.Play();
            _PlayerTimer?.Start();
        }

        internal void Pause(MusicViewModel music)
        {
            _Player?.Pause();
            _PlayerTimer?.Stop();
        }

        internal void Seek(TimeSpan position)
        {
            if (_Player != null)
            {
                _Player.Position = position;
            }
        }

        private void _PlayerTimer_Tick(object sender, EventArgs e)
        {
            var m = (DataContext as MainWindowViewModel)?.CurrentTrack;
            if (m?.IsPlaying == true && _Player != null)
            {
                m.Position = _Player.Position;
            }
        }
    }
}