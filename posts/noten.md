
# NotenOrganizer: C# trifft auf Cloud-Sync

Die Verwaltung von digitalen Notenblättern für Musiker ist oft ein Chaos aus PDFs und lokalen Ordnern. Mein Ziel war es, eine App zu bauen, die nicht nur Ordnung schafft, sondern die Daten über verschiedene Geräte hinweg synchronisiert.

## Die technische Herausforderung
Eine lokale **SQLite-Datenbank** ist schnell und zuverlässig, aber sie "lebt" normalerweise nur auf einem Gerät. Für die Synchronisation musste ich einen Weg finden, die Datenbankdatei sicher in die Cloud zu schieben, ohne dass es bei gleichzeitigem Zugriff zu Datenverlust kommt.



### Der Stack
* **Sprache:** C# (.NET)
* **Datenbank:** SQLite mit Entity Framework Core
* **Cloud-Schnittstelle:** Mega.nz API-Wrapper

### Code-Snippet: Datenbank-Backup-Logik
Hier ist ein Ausschnitt, wie ich den Export der Datenbank für den Sync vorbereite:

```csharp
public async Task SyncDatabaseAsync(string localPath)
{
    // Prüfen, ob die Datei existiert
    if (File.Exists(localPath))
    {
        // Upload-Logik zu Mega.nz
        await _megaClient.UploadFileAsync(localPath, "NotenApp/backups/");
        Console.WriteLine("Sync erfolgreich!");
    }
}