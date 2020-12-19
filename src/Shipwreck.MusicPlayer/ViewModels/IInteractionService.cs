using System;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public interface IInteractionService
    {
        string[] OpenFiles();

        void Play(MusicViewModel music);

        void Pause(MusicViewModel music);

        void Seek(TimeSpan position);
    }
}