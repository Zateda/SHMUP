using UnityEngine;

public class MoveSinus : MonoBehaviour {
    private float sinCenter;                    // Erstelle sinCenter als kommazahl

    public float amplitude = 2;                 // Erstelle amplitude als kommazahl, wie hoch die wellen sind
    public float frequency = 2;                 // Erstelle freauency als kommazahl, wie eng die wellen sind
    public bool invert = false;                 // Erstelle invert als bool mit false als start
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        sinCenter = transform.position.y;       // hier setzen wir sinCenter auf position y?
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {                                    // Wir erstellen FixedUpdate
        Vector3 pos = transform.position;                           // wir erstelen pos als position

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;     // wir erstellen sin und geben ihm ne formel die eine Sinuswelle erzeugt.
        if (invert) {                                               // wenn invert aktiv ist ...
            sin *= -1;                                              // ... setze den wert auf minus und invertiere die Sinuswellen
        }

        pos.y = sinCenter + sin;                                    // gib pos y sinCenter zusammen mit sin 

        transform.position = pos;                                   // mach die neue position zur pos
    }
}
