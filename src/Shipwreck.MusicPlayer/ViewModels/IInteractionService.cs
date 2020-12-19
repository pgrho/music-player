using System;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public interface IInteractionService
    {
        string OpenPlaylist();

        string[] OpenTracks();

        void Play(MusicViewModel music);

        void Pause(MusicViewModel music);

        void Seek(TimeSpan position);
    }
}