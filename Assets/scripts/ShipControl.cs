using System;
using UnityEngine;

public class ShipControl : MonoBehaviour {          // Hier steht die Klasse ShipControl
                                                    // ...und MonoBehavior ist eine Unity Basis Klasse...
                                                    // ...die auf Objekte gelegt werden kann, Start() und Update()...
                                                    // ...verwenden kann und auf Unity Komponenten zugreifen kann.
    // >>> Hier stehen die Variablen die in diversen Funktionen dieser Klasse genutzt werden!
    
    public float moveSpeed = 5;     // Private Variable die als Kommazahl 5 enthält

    private bool moveUp;             // Ein bool der nur true or false wiedergeben kann

    private bool moveDown;           // Ein bool der nur true or false wiedergeben kann

    private bool moveRight;          // Ein bool der nur true or false wiedergeben kann

    private bool moveLeft;           // Ein bool der nur true or false wiedergeben kann
    
    private bool shoot;
    // <<< Hier stehen die Variablen die in diversen Funktionen dieser Klasse genutzt werden!
    public Gun[] guns;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()                                    // Hier kommen Funktionen rein die beim Start von ShipControl ausgeführt werden
    {
        guns = transform.GetComponentsInChildren<Gun>();
    }
    // Update is called once per frame
    void Update() {     // Update ist etwas das einmal pro Bild ausgeführt wird (60FPS=60aufrufe),
        // >>> hier werden richtungen mit Tasten verbunden.
        moveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);          // Wir eine dieser Tasten...
        moveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);      // ...Gedrückt bekommt die Variable...
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);    // ...Den wert...
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);      // ...true
        // <<< hier werden richtungen mit Tasten verbunden.
        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot) {
            shoot = false;
            foreach(Gun gun in guns) {
                gun.shoot();
            }
        }
    }

    private void FixedUpdate() {        // Eine Update Funktion die normalerweise 50mal pro sekunde aufgerufen wird...
                                        // ...und somit nicht von den FPS des spiels abhängt.
        Vector3 pos = transform.position;       // Eine Koordinate namens pos die die Position dieses Objects bekommt. bekommt
        float moveAmount = moveSpeed * Time.fixedDeltaTime;   // eine Variable die eine Zahl enthält bestehend aus Speed * Time
                                                              // ...und dadurch unabhängig von den FPS wird.
        Vector3 move = Vector3.zero;                          // Eine Koordinate names move die als Koordinaten 0.0,0 bekommt

        if (moveUp) {                   // Wenn moveUp true wird...
            move.y += moveAmount;       // verändere move in richtung y + um den wert in moveAmount.
        }
        if (moveDown) {                 // Wenn moveDown true wird...
            move.y -= moveAmount;       // verändere move in richtung y - um den wert in moveAmount.
        }
        if (moveRight) {                // Wenn moveRight true wird...
            move.x += moveAmount;       // verändere move in richtung x + um den wert in moveAmount.
        }
        if (moveLeft) {                 // Wenn moveLeft true wird...
            move.x -= moveAmount;       // verändere move in richtung x - um den wert in moveAmount.
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);    // erstelle moveMagnitude mit inhalt der Quadratwurzel als ganzzahl aus allen moves.
        if (moveMagnitude > moveAmount) {                                         // Ist Magnitude größer als Amount...
            float ratio = moveAmount / moveMagnitude;                             // ...erstele ratio mit einer ganzzahl aus Amount durch Magnitude...
            move *= ratio;                                                        // ...und nim move mal ratio.
        }   // Hierdurch beweget sich das Schiff Diagonal nicht mehr mit der geschwindugkeit aus zwei richtungen.
        pos += move;        // Hier sage ich addiere zur position das bewegen
        // >>> Hier gebe ich dem Objekt grenzen die es nicht überschreiten kann.
        if (pos.x >= 8f) {
            pos.x = 8f;
        }
        if (pos.x <= -8f) {
            pos.x = -8f;
        }
        if (pos.y >= 4.5f) {
            pos.y = 4.5f;
        }
        if (pos.y <= -4.5f) {
            pos.y = -4.5f;
        }
        // <<< Hier gebe ich dem Objekt grenzen die es nicht überschreiten kann.
        transform.position = pos;   // Verändere den wert in der Position zur neuen Position.
    }

    private void OnTriggerEnter(Collider collision) {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null) {
            if (bullet.isEnemy) {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }

        Destruct destructable = collision.GetComponent<Destruct>();
        if (destructable != null) {
            Destroy(gameObject);
            Destroy(destructable.gameObject);
        }
    }
}
