using Microsoft.Win32;
using Shipwreck.MusicPlayer.Views;
using System;
using System.Windows;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public sealed class InteractionService : IInteractionService
    {
        public string[] OpenFiles()
        {
            var mw = Application.Current?.MainWindow;
            var dialog = new OpenFileDialog()
            {
                Filter = "MP3|*.mp3",
                Multiselect = true,
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