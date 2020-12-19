using Shipwreck.MusicPlayer.Models;
using System;
using System.Linq;
using System.Windows.Input;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IInteractionService Interaction { get; } = new InteractionService();

        #region Playlist

        private PlaylistViewModel _Playlist;

        public PlaylistViewModel Playlist
        {
            get => _Playlist ??= new PlaylistViewModel();
            private set => SetProperty(ref _Playlist, value);
        }

        #endregion Playlist

        #region CurrentTrack

        private MusicViewModel _CurrentTrack;

        public MusicViewModel CurrentTrack
        {
            get => _CurrentTrack;
            private set => SetProperty(ref _CurrentTrack, value);
        }

        #endregion CurrentTrack

        #region OpenPlaylistCommand

        private ICommand _OpenPlaylistCommand;

        public ICommand OpenPlaylistCommand
            => _OpenPlaylistCommand ??= new SimpleCommand(() =>
            {
                var pl = Interaction.OpenPlaylist();
                if (!string.IsNullOrEmpty(pl))
                {
                    try
                    {
                        var pvm = new PlaylistViewModel();
                        foreach (var e in M3uParser.Load(pl))
                        {
                            if (e.Uri.IsFile)
                            {
                                pvm.Items.Add(new MusicViewModel(e.Uri.LocalPath));
                            }
                        }
                        if (CurrentTrack?.IsPlaying == true)
                        {
                            PauseTrackCommand.Execute(CurrentTrack);
                        }
                        Playlist = pvm;
                        CurrentTrack = pvm.Items.FirstOrDefault();
                    }
                    catch { }
                }
            });

        #endregion OpenPlaylistCommand

        #region AddTrackCommand

        private ICommand _AddTrackCommand;

        public ICommand AddTrackCommand
            => _AddTrackCommand ??= new SimpleCommand(() =>
            {
                var fs = Interaction.OpenTracks();
                if (fs?.Length > 0)
                {
                    foreach (var f in fs)
                    {
                        if (!Playlist.Items.Any(e => e.FullPath == f))
                        {
                            var m = new MusicViewModel(f);
                            Playlist.Items.Add(m);
                            CurrentTrack ??= m;
                        }
                    }
                }
            });

        #endregion AddTrackCommand

        #region PlayTrackCommand

        private ICommand _PlayTrackCommand;

        public ICommand PlayTrackCommand
            => _PlayTrackCommand
            ??= new ParameteredCommand(p =>
            {
                if (p is MusicViewModel m)
                {
                    if (_CurrentTrack != m)
                    {
                        if (_CurrentTrack != null)
                        {
                            _CurrentTrack.IsPlaying = false;
                            _CurrentTrack.Position = null;
                            Interaction.Pause(_CurrentTrack);
                        }
                        CurrentTrack = m;

                        m.IsPlaying = true;
                        Interaction.Play(m);
                        Interaction.Seek(TimeSpan.Zero);
                    }
                    else if (!m.IsPlaying)
                    {
                        m.IsPlaying = true;
                        Interaction.Play(m);
                    }
                }
            });

        #endregion PlayTrackCommand

        #region PauseTrackCommand

        private ICommand _PauseTrackCommand;

        public ICommand PauseTrackCommand
            => _PauseTrackCommand
            ??= new ParameteredCommand(p =>
            {
                if (p is MusicViewModel m)
                {
                    m.IsPlaying = false;
                    Interaction.Pause(m);
                }
            });

        #endregion PauseTrackCommand
    }
}