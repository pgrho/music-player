using Microsoft.Win32;
using Shipwreck.MusicPlayer.Views;
using System;
using System.Linq;
using System.Windows;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public sealed class InteractionService : IInteractionService
    {
        public string OpenPlaylist()
            => OpenFiles("Playlist|*.m3u;*.m3u8", false)?.FirstOrDefault();

        public string[] OpenTracks()
            => OpenFiles("MP3|*.mp3", true);

        private string[] OpenFiles(string filter, bool multiSelect)
        {
            var mw = Application.Current?.MainWindow;
            var dialog = new OpenFileDialog()
            {
                Filter = filter,
                Multiselect = multiSelect,
            };
            if (dialog.ShowDialog(Application.Current?.MainWindow) == true)
            {
                return dialog.FileNames;
            }
            return null;
        }

        public void Play(MusicViewModel music)
            => (Application.Current?.MainWindow as MainWindow)?.Play(music);

        public void Pause(MusicViewModel music)
            => (Application.Current?.MainWindow as MainWindow)?.Pause(music);

        public void Seek(TimeSpan position)
            => (Application.Current?.MainWindow as MainWindow)?.Seek(position);
    }
}