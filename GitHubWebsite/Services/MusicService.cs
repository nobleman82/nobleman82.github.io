using Microsoft.JSInterop;

namespace GitHubWebsite.Services
{
    public class MusicService
    {
        private readonly IJSRuntime _jsRuntime;
        public string CurrentPlaylistId { get; private set; }
        public bool IsPlaying { get; private set; }
        public event Action OnChange;

        // Die JSRuntime wird hier über den Konstruktor injiziert
        public MusicService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetPlaylist(string playlistId)
        {
            if (CurrentPlaylistId == playlistId) return;

            CurrentPlaylistId = playlistId;
            IsPlaying = true;

            // Initialisiert den Player mit der neuen Playlist
            await _jsRuntime.InvokeVoidAsync("youtubePlayer.init", playlistId);
            NotifyStateChanged();
        }

        public async Task TogglePlay()
        {
            if (IsPlaying)
            {
                await _jsRuntime.InvokeVoidAsync("youtubePlayer.pause");
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("youtubePlayer.play");
            }

            IsPlaying = !IsPlaying;
            NotifyStateChanged();
        }

        public async Task Stop()
        {
            IsPlaying = false;
            await _jsRuntime.InvokeVoidAsync("youtubePlayer.stop");
            NotifyStateChanged();
        }

        public async Task Next()
        {
            if (IsPlaying) await _jsRuntime.InvokeVoidAsync("youtubePlayer.next");
        }

        public async Task Previous()
        {
            if (IsPlaying) await _jsRuntime.InvokeVoidAsync("youtubePlayer.previous");
        }

        public async Task SetVolume(int level)
        {
            await _jsRuntime.InvokeVoidAsync("youtubePlayer.setVolume", level);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}