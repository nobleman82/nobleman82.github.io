# Hardware-Hacking: Das DIY USB-Fußpedal

Als Gitarrist und Entwickler wollte ich meine Effekte am PC steuern, ohne die Hände vom Instrument zu nehmen. Kommerzielle Lösungen waren mir entweder zu teuer oder nicht flexibel genug. Also: **Selber bauen!**

## Das Konzept
Das Herzstück ist ein **Arduino Nano**. Dieser liest die analogen Signale von Fußtastern ein und sendet sie als Tastaturbefehle (HID - Human Interface Device) an den Computer.



[Image of Arduino Nano pinout diagram]


### Bauteile
1. Arduino Nano (oder Pro Micro für nativen USB-Support)
2. 3x Robuste Fußtaster (Momentary Switches)
3. Stabiles Aluminium-Gehäuse
4. Klinkenbuchsen für externe Erweiterungen

### Die Firmware
Hier ein Teil der Logik, die das "Prellen" der Taster verhindert (Debouncing):

```cpp
const int buttonPin = 2;
int buttonState = 0;

void setup() {
  pinMode(buttonPin, INPUT_PULLUP);
  Keyboard.begin();
}

void loop() {
  buttonState = digitalRead(buttonPin);
  if (buttonState == LOW) {
    Keyboard.press(' '); // Sendet ein Leerzeichen (z.B. für Play/Pause)
    delay(200);          // Einfaches Debouncing
    Keyboard.releaseAll();
  }
}