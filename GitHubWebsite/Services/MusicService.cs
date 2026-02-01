namespace GitHubWebsite.Services
{
    public class MusicService
    {
        public string CurrentPlaylistId { get; private set; }
        public bool IsPlaying { get; private set; }
        public event Action OnChange;

        public void SetPlaylist(string playlistId)
        {
            // Wenn die ID schon geladen ist, tu nichts -> Verhindert Doppelt-Start!
            if (CurrentPlaylistId == playlistId) return;

            CurrentPlaylistId = playlistId;
            IsPlaying = true;
            NotifyStateChanged();
        }

        public void TogglePlay()
        {
            IsPlaying = !IsPlaying;
            NotifyStateChanged();
        }

        public void Stop()
        {
            IsPlaying = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        
    }
}
