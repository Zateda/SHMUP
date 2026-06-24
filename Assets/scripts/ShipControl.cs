using System;
using UnityEngine;

public class ShipControl : MonoBehaviour {          // Hier steht die Klasse ShipControl
                                                    // ...und MonoBehavior ist eine Unity Basis Klasse...
                                                    // ...die auf Objekte gelegt werden kann, Start() und Update()...
                                                    // ...verwenden kann und auf Unity Komponenten zugreifen kann.
    // >>> Hier stehen die Variablen die in diversen Funktionen dieser Klasse genutzt werden!
    private Vector3 initialPosition;
    
    public float moveSpeed = 5;     // Private Variable die als Kommazahl 5 enthält

    private float speedMultiplier = 1;

    private bool moveUp;             // Ein bool der nur true or false wiedergeben kann

    private bool moveDown;           // Ein bool der nur true or false wiedergeben kann

    private bool moveRight;          // Ein bool der nur true or false wiedergeben kann

    private bool moveLeft;           // Ein bool der nur true or false wiedergeben kann
    
    private bool shoot;
    // <<< Hier stehen die Variablen die in diversen Funktionen dieser Klasse genutzt werden!
    public Gun[] guns;               // Ein Gun array ... das guns heist? gerade verwwirt mich das.

    private GameObject shield;      // ein Gameobject names shield

    private int powerUpGunLevel = 0;

    private int hits = 3;
    private bool invincible = false;
    private float invincibleTime = 1 ;
    private float invincibleTimer = 0 ;

    private void Awake() {
        initialPosition = transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()                                    // Hier kommen Funktionen rein die beim Start von ShipControl ausgeführt werden
    {
        guns = transform.GetComponentsInChildren<Gun>();    // guns soll Componenten aus dem script Gun hohlen
        foreach (Gun gun in guns) {
            gun.isActive = true;
        }

        shield = transform.Find("Shield").gameObject;       // shield soll das Spielobiect Shield finden
        DeactivateShield();                                 // Shield soll zu beginn aus sein
    }
    // Update is called once per frame
    void Update() {     // Update ist etwas das einmal pro Bild ausgeführt wird (60FPS=60aufrufe),
        // >>> hier werden richtungen mit Tasten verbunden.
        moveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);          // Wir eine dieser Tasten...
        moveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);      // ...Gedrückt bekommt die Variable...
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);    // ...Den wert...
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);      // ...true
        // <<< hier werden richtungen mit Tasten verbunden.
        shoot = Input.GetKeyDown(KeyCode.LeftControl);                              // Mit leftControl wird shoot genutzt
        if (shoot) {                                                                // wird shoot aufgerufen ...
            shoot = false;                                                          // ... Shoot ist negativ ...
            foreach(Gun gun in guns) {                                              // ... Für jede Gun im array guns ...
                if (gun.gameObject.activeSelf) {
                    gun.shoot();                                                    // ... aktiviere shoot?
                }
            }
        }

        if (invincible) {
            // TODO HIER WAS EINBAUEN DAS OBJECKT FLACKERN LÄST
            if (invincibleTimer >= invincibleTime) {
                invincibleTimer = 0;
                invincible = false;
            } else {
                invincibleTimer += Time.deltaTime;
                // TODO HIER WAS EINBAUEN DAS OBJECKT NICHT MEHR FLACKERN LÄST
            }
        }
    }

    private void FixedUpdate() {        // Eine Update Funktion die normalerweise 50mal pro sekunde aufgerufen wird...
                                        // ...und somit nicht von den FPS des spiels abhängt.
        Vector3 pos = transform.position;       // Eine Koordinate namens pos die die Position dieses Objects bekommt. bekommt
        float moveAmount = moveSpeed * speedMultiplier * Time.fixedDeltaTime;   // eine Variable die eine Zahl enthält bestehend aus Speed * Time
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

    void ActivateShield() {         // Funktion zum aktivieren eines Schildes
        shield.SetActive(true);     // Das Schild wird auf aktiv gesetzt.
    }

    void DeactivateShield() {       // Funktion zum deaktivieren deines Schildes
        shield.SetActive(false);    // Das Schild wird auf inaktiv gesetzt.
    }

    bool HasShield() {              // Bool der beim erhalt eine Schildes geschaltet wird
        return shield.activeSelf;   // gibt zurück das das Schild aktiv ist.
    }

    void addGuns() {
        powerUpGunLevel++;
        foreach (Gun gun in guns) {
            if (gun.powerUpLevelRequierment <= powerUpGunLevel) {
                gun.gameObject.SetActive(true);
            }
            else {
                gun.gameObject.SetActive(false);
            }
        }
    }

    void SetSpeedMultiplier(float mult) {
        speedMultiplier = mult;
    }

    void resetShip() {
        transform.position = initialPosition;
        DeactivateShield();
        powerUpGunLevel = -1;
        addGuns();
        SetSpeedMultiplier(1);
        hits = 3;
        Level.instance.ResetLevel();
    }

    void Hit(GameObject gameObjectHit) {
        if (HasShield()) { // ... wenn es ein Schild hat ...
            DeactivateShield(); // ... schallte Shild aus.
        }
        else { // ansonsten
            if (!invincible) {
                hits--;
                if (hits == 0) {
                    resetShip();
                }
                else {
                    invincible = true;
                }

                Destroy(gameObjectHit);
            }
        }
    }
    
    private void OnTriggerEnter(Collider collision) { // Funktion die aktiv wird sobald sich etwas berührt
        
        Bullet bullet = collision.GetComponent<Bullet>(); // Berrürt eine Bullet vom typ Bullet etwas...
        if (bullet != null) { // Wenn die Kugel nicht null ist...
            if (bullet.isEnemy) { // Wenn die Kugel von einem Gegner ist...
                Hit(bullet.gameObject); // ...und Zerstöre die Kugel!
            }
        }
        
        Destruct destructable = collision.GetComponent<Destruct>(); // Zerstöre Terstörbares wenn es mit etwas Zerstörbarem collidiert
        if (destructable != null) { // Wenn Zerstörbar nicht leer ist ...
            Hit(destructable.gameObject); // ... zerstöre das Object
        }

        PowerUP powerUp = collision.GetComponent<PowerUP>(); // Power up ein Power up wenn dieses Object ein PowerUp Berührt
        if (powerUp) { // wenn ein powerUp ...
            if (powerUp.activateShield) { // ... ist das powerup ein activateShield ...
                    ActivateShield(); // ... Aktiviere das Shild.
            }

            if (powerUp.addGuns) {
                    addGuns();
            }

            if (powerUp.speedUp) {
                    SetSpeedMultiplier(speedMultiplier + 1);
            }

            Level.instance.addScore(powerUp.PointValue);
            Destroy(powerUp.gameObject); // Zerstöre das Powerup Object.
        }
        
    }
}
