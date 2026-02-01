var player;
var isApiReady = false;

// Wird von der YouTube API aufgerufen, sobald das Skript geladen ist
function onYouTubeIframeAPIReady() {
    isApiReady = true;
}

window.youtubePlayer = {
    init: function (playlistId) {
        if (!isApiReady) {
            console.warn("YouTube API noch nicht bereit.");
            return;
        }

        if (!player) {
            // Player wird zum ersten Mal erstellt
            player = new YT.Player('youtube-player', {
                height: '0',
                width: '0',
                playerVars: {
                    'listType': 'playlist',
                    'list': playlistId,
                    'autoplay': 1,
                    'controls': 0,
                    'enablejsapi': 1,
                    'origin': window.location.origin
                },
                events: {
                    'onReady': function (event) {
                        event.target.playVideo();
                    },
                    'onError': function (e) {
                        console.error("YouTube Player Fehler:", e.data);
                    }
                }
            });
        } else {
            // Player existiert schon, lade nur neue Playlist
            player.cuePlaylist({
                listType: 'playlist',
                list: playlistId
            });
            // Kurz warten, damit YouTube die Playlist laden kann, dann Play
            setTimeout(() => { player.playVideo(); }, 500);
        }
    },
    play: () => { if (player) player.playVideo(); },
    pause: () => { if (player) player.pauseVideo(); },
    stop: () => { if (player) player.stopVideo(); },
    next: () => { if (player) player.nextVideo(); },
    previous: () => { if (player) player.previousVideo(); },
    setVolume: (vol) => { if (player) player.setVolume(vol); }
};