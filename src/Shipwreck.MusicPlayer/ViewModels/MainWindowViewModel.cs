using System;
using System.Linq;
using System.Windows.Input;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IInteractionService Interaction { get; } = new InteractionService();

        public PlaylistViewModel Playlist { get; } = new PlaylistViewModel();

        #region CurrentTrack

        private MusicViewModel _CurrentTrack;

        public MusicViewModel CurrentTrack
        {
            get => _CurrentTrack;
            private set => SetProperty(ref _CurrentTrack, value);
        }

        #endregion CurrentTrack

        #region AddTrackCommand

        private ICommand _AddTrackCommand;

        public ICommand AddTrackCommand
            => _AddTrackCommand ??= new SimpleCommand(() =>
            {
                var fs = Interaction.OpenFiles();
                if (fs?.Length > 0)
                {
                    foreach (var f in fs)
                    {
                        if (!Playlist.Items.Any(e => e.FullPath == f))
                        {
                            Playlist.Items.Add(new MusicViewModel(f));
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