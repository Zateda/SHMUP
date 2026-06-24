using System;
using UnityEngine;

public class ShipControl : MonoBehaviour {      // Hier steht die Klasse ShipControl
                                                // ...und MonoBehavior ist eine Unity Basis Klasse...
                                                // ...die auf Objekte gelegt werden kann, Start() und Update()...
                                                // ...verwenden kann und auf Unity Komponenten zugreifen kann.
    
    private Vector3 initialPosition;            // Hier wird ein Vector3 wert erstellt der die Startposition speichern soll
    
    public float moveSpeed = 5;                 // Variable die als Kommazahl 5 enthält
    private float speedMultiplier = 1;          // Variable die als Kommazahl 1 enthält

    private bool moveUp;                        // Ein bool der nur true or false wiedergeben kann, um zu merken das die Taste für hoch gedrückt wird
    private bool moveDown;                      // Ein bool um zu merken das die Taste für runter gedrückt wird
    private bool moveRight;                     // Ein bool um zu merken das die Taste für rechts gedrückt wird
    private bool moveLeft;                      // Ein bool um zu merken das die Taste für links gedrückt wird
    private bool shoot;                         // Ein bool um zu merken ob geschossen wird
    
    public Gun[] guns;                          // Ein Gun array ... das guns heist? gerade verwwirt mich das.
    private int powerUpGunLevel = 0;                // Ganzzahl in powerUpGunLevel

    private GameObject shield;                  // ein Gameobject names shield
    
    private int hits = 3;                       // Ein Ganzzahlenwert der dem Schiff 3 trefferpunkte gibt.
    private Renderer[] shipRenderers;              // Ein Renderer für die Ship modelle
    private float renderInterval = 0.1f;
    public GameObject model3Hp;                 // Plätze für GameObjecte ...
    public GameObject model2Hp;                 // ... um darzustellen ...
    public GameObject model1Hp;                 // ... wie das Schiff shaden bekommt.
        
    private bool invincible = false;            // Ein bool namens invincible auf false
    private float invincibleTime = 1 ;          // eine Kommazahl auf 1
    private float invincibleTimer = 0 ;         // eine Kommazahl auf 0

    private void Awake() {                      // Eine Funktion namens Awake
        initialPosition = transform.position;   // Speichert die jetzige Position als Startposition
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()                                // Hier kommen Funktionen rein die beim Start von ShipControl ausgeführt werden
    {
        guns = transform.GetComponentsInChildren<Gun>();    // guns soll Componenten aus dem script Gun hohlen
        foreach (Gun gun in guns) {
            gun.isActive = true;
            if (gun.powerUpLevelRequirement != 0) {
                gun.gameObject.SetActive(false);
            }
        }

        shipRenderers = GetComponentsInChildren<Renderer>();// Hier bekommt der shipRenderer einen Renderer
        
        shield = transform.Find("Shield").gameObject;       // shield soll das Spielobiect Shield finden
        DeactivateShield();                                 // Shield soll zu beginn aus sein
        updateShipModel();
    }
    // Update is called once per frame
    void Update() {                                         // Update ist etwas das einmal pro Bild ausgeführt wird (60FPS=60aufrufe),
        // >>> hier werden richtungen mit Tasten verbunden.
        moveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);          // Wir eine dieser Tasten...
        moveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);      // ...Gedrückt bekommt die Variable...
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);    // ...Den wert...
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);      // ...true
        // <<< hier werden richtungen mit Tasten verbunden.
        shoot = Input.GetKeyDown(KeyCode.LeftControl);                              // Mit leftControl wird shoot genutzt
        if (shoot) {                                                                // wird shoot aufgerufen ...
            shoot = false;                                                          // ... Shoot wird auf false gesetzt ...
            foreach(Gun gun in guns) {                                              // ... Für jede Gun im array guns ...
                if (gun.gameObject.activeSelf) {
                    gun.shoot();                                                    // ... aktiviere shoot?
                }
            }
        }

        if (invincible) {                                               // Wenn invincible true ist ...
            // TODO HIER WAS EINBAUEN DAS OBJECKT FLACKERN LÄST
            if (invincibleTimer >= invincibleTime) {                    // Wenn Timer größer Time ist ...
                invincibleTimer = 0;                                    // Timer auf 0
                invincible = false;                                     // invincegle auf false
                SetShipVisible(true); // Renderer auf true
            } else {                                                    // ansonsten
                invincibleTimer += Time.deltaTime;                      // Timer + Time
                
                if (invincibleTime >= renderInterval) {
                    ToggleShipVisible();           // Renderer auf false fals er true war
                    invincibleTimer = 0;
                }
                // TODO HIER WAS EINBAUEN DAS OBJECKT NICHT MEHR FLACKERN LÄST
            }
        }
    }

    private void FixedUpdate() {        // Eine Update Funktion die normalerweise 50mal pro sekunde aufgerufen wird...
                                        // ...und somit nicht von den FPS des spiels abhängt.
        Vector3 pos = transform.position;                     // Eine Koordinate namens pos die die Position dieses Objects bekommt. bekommt
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
        }                   // Hierdurch beweget sich das Schiff Diagonal nicht mehr mit der geschwindugkeit aus zwei richtungen.
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

    void AddGuns() {                                                // Funktion zum hinzufügen von Waffen
        powerUpGunLevel++;                                          // den PowerUpGunLevel plus 1 setzen
        foreach (Gun gun in guns) {                                 // Für jede typ Gun, gun im array guns ...
            if (gun.powerUpLevelRequirement <= powerUpGunLevel) {   // ... wenn die powerUpLevelRequierments kleiner sind als das powerUpLEvel ...
                gun.gameObject.SetActive(true);                     // ...setze das Gameobject gun auf true ...
            } else {                                                // ... wenn nicht ...
                gun.gameObject.SetActive(false);                    // ... setze es auf false.
            }
        }
    }

    void SetSpeedMultiplier(float mult) {           // Funktion die einen wert namens mult ausgibt?
        speedMultiplier = mult;                     // der speedMultiplier erhält den wert mult
    }

    void resetShip() {                              // Funktion namens resetShip die ...
        transform.position = initialPosition;       // ... jetzige position auf startposition setzt ...
        DeactivateShield();                         // ... DeactivateShield ausführt ...
        powerUpGunLevel = -1;                       // ... PowerUpGunLevel auf -1 setzt ...
        AddGuns();                                  // ... AddGuns ausführt ...
        SetSpeedMultiplier(1);                 // ... SetSpeedMultiplier mit mult 1 ausführt ...
        hits = 3;                                   // ... hits auf 3 setzt ...
        updateShipModel();
        Level.instance.ResetLevel();                // ... im Level script ResetLevel() ausführt
    }

    void Hit(GameObject gameObjectHit) {        // Funktion Hit die gameObjectHit liefert
        if (HasShield()) {                      // Wwenn es ein Schild hat ...
            DeactivateShield();                 // ... schallte Shild aus.
        } else { // ansonsten                   // ansonsten
            if (!invincible) {                  // Wenn invincible false ist ...
                hits--;                         // ... Ziehe 1 von hits ab ...
                updateShipModel();              // ...Update das Shiffmodel
                if (hits == 0) {                // Wenn hits gleich 0 ist ...
                    resetShip();                // ... führe resetShip aus.
                } else {                        // ansonsten ...
                    invincible = true;          // ... schalte invincible auf true
                }

                Destroy(gameObjectHit);         // Zerstöre gameObjectHit!
            }
        }
    }

    void updateShipModel() {
        model3Hp.SetActive(hits == 3);
        model2Hp.SetActive(hits == 2);
        model1Hp.SetActive(hits == 1);
    }

    void SetShipVisible(bool visible) {
        foreach (Renderer r in shipRenderers) {
            r.enabled = visible;
        }
    }

    void ToggleShipVisible() {
        foreach (Renderer r in shipRenderers) {
            r.enabled = !r.enabled;
        }
    }

    private void OnTriggerEnter(Collider collision) {       // Funktion die aktiv wird sobald sich etwas berührt
        
        Bullet bullet = collision.GetComponent<Bullet>();   // Berrürt eine Bullet vom typ Bullet etwas...
        if (bullet != null) {                            // Wenn die Kugel nicht null ist...
            if (bullet.isEnemy) {                           // Wenn die Kugel von einem Gegner ist...
                Hit(bullet.gameObject);                     // ...und Zerstöre die Kugel!
            }
        }
        
        Destruct destructable = collision.GetComponent<Destruct>(); // Zerstöre Zerstörbares wenn es mit etwas Zerstörbarem collidiert
        if (destructable != null) {                              // Wenn Zerstörbar nicht leer ist ...
            Hit(destructable.gameObject);                           // ... zerstöre das Object
        }

        PowerUP powerUp = collision.GetComponent<PowerUP>();        // Power up ein Power up wenn dieses Object ein PowerUp Berührt
        if (powerUp) {                                              // wenn ein powerUp ...
            if (powerUp.activateShield) {                           // Wenn das powerup ein activateShield it ...
                    ActivateShield();                               // ... Aktiviere das Shild.
            }

            if (powerUp.addGuns) {                                  // Wenn es AddGuns ist ...
                    AddGuns();                                      // ... AddGuns!
            }

            if (powerUp.speedUp) {                                  // Wenn es speedUp ist ...
                    SetSpeedMultiplier(speedMultiplier + 1);   // ...gib dem speedMultiplier +1
            }

            Level.instance.addScore(powerUp.PointValue);            // Ruft im script Level dir funktion addScore auf und üebrgibt PointValue vom powerUp
            Destroy(powerUp.gameObject);                            // Zerstöre das Powerup Object.
        }
        
    }
}
