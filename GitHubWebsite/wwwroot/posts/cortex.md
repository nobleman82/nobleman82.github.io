# Projekt-Fokus: CortexCommand 🚀
### Die hybride Steuerungszentrale für CNC, Laser und 3D-Druck

CortexCommand ist mein aktuelles Herzensprojekt. Es ist eine moderne, plattformübergreifende Steuerungssoftware, die die Lücke zwischen intuitiver Web-UI und hardwarenaher Echtzeit-Kommunikation schließt.

![CortexCommand Architektur-Schema](/images/blog-cortex-schema.png)
*Abbildung 1: Systemarchitektur und Datenfluss von CortexCommand*

---

## 🏗 Die Architektur: "Hybrid & Modular"

Im Gegensatz zu klassischen monolithischen Anwendungen habe ich mich bei CortexCommand für einen **hybriden Ansatz** entschieden. Das System besteht aus zwei Hauptkomponenten, die über WebSockets miteinander kommunizieren:

1. **Frontend/Server (C# / Blazor):** Das "Gehirn", das für die Benutzerführung, Job-Verwaltung und 3D-Visualisierung zuständig ist.
2. **Worker (Python):** Der "Muskel", der direkt an der Hardware sitzt, das GRBL-Protokoll versteht und die serielle Kommunikation übernimmt.

### Warum dieser Mix?
Durch die Verwendung von **Blazor** kann ich eine hochreaktive Benutzeroberfläche in C# schreiben, während **Python** auf der Hardware-Seite unschlagbar flexibel ist, wenn es um die Ansteuerung verschiedener serieller Schnittstellen oder GPIOs (z.B. auf einem Raspberry Pi) geht.

---

## 💻 Ein Blick in den Code

### 1. Die Shared Logic (CortexCommand.RCL)
Um Code-Duplizierung zu vermeiden, nutze ich eine **Razor Class Library**. Hier liegen alle UI-Komponenten, die sowohl im Web-Server als auch im WPF-Desktop-Wrapper verwendet werden.

```csharp
// Beispiel für eine modulare Widget-Komponente
public partial class ConnectionWidget
{
    [Parameter] public string MachineName { get; set; }
    [Inject] private IConnectionService Connection { get; set; }

    private async Task ToggleConnect() 
    {
        // Logik für die Verbindung zum Python-Worker
    }
}
2. Der Python Worker (GRBL Kommunikation)
Der Worker ist darauf optimiert, schlank und schnell zu sein. Hier ein Ausschnitt aus der Protokoll-Verarbeitung:

Python
# grbl_protocol.py (Auszug)
def process_status_report(data):
    # Extrahiert Arbeitskoordinaten (WPos) und Maschinenzustand
    if "<" in data and ">" in data:
        status = data[data.find("<")+1:data.find(">")]
        parts = status.split("|")
        return {"state": parts[0], "coords": parts[1]}
🛠 Aktueller Stand & Roadmap
Das Projekt befindet sich aktuell in der Early-Alpha-Phase (ca. 10% Fortschritt). Die Grundpfeiler der Kommunikation stehen bereits, und die ersten G-Code-Befehle werden erfolgreich vom Server zum Worker gestreamt.

Nächste Meilensteine:

[ ] Implementierung eines interaktiven 3D-Viewers mit three.js.

[ ] Dynamisches Widget-System für das Dashboard.

[ ] Vollständige Integration der Kamera-Feeds zur Prozessüberwachung.

🌟 Fazit
CortexCommand ist für mich mehr als nur ein Tool – es ist das Experiment, wie man moderne Web-Technologien nutzt, um betagte Hardware-Schnittstellen ins 21. Jahrhundert zu holen.

Du möchtest mehr erfahren oder das Projekt auf GitHub verfolgen? Hier geht's zum Repository!