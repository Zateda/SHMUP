using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet bullet;                   // Erstelle reference auf das Bullet prefab
    private Vector3 direction;              // Erstelle direction das von Vector 3 kommt
    public bool autoShoot = false;          // Erstelle den bool autoShoot auf false gestellt
    public float shootInterval = 0.5f;      // Erstelle shootInterval als kommazahl 0,5
    public float shootDelay = 0.0f;         // Erstelle shootDelay als kommatahl 0
    private float shootTimer = 0f;          // Erstelle shootTimer privat
    private float delayTimer = 0f;          // Erstelle delay Timer privat
    public int powerUpLevelRequirement = 0; // Ganzzahlenwert der auf 0 Startet

    public bool isActive = false;           // bool auf false
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() {

        if (!isActive) {        // Wenn isActive auf false ist ...
            return;             // ... mach nix?
        }

        direction = (transform.localRotation * Vector3.right).normalized;   // Gibt direction eine berrechnung für die flugrichtung

        if (autoShoot) {                                // wenn autoShot aktive ist...
            if (delayTimer >= shootDelay) {             // ist der delayTimer größer oder gleich dem shootDelay ...
                if (shootTimer >= shootInterval) {      // ist der shootTimer größer oder gleich dem shootInterval ...
                    shoot();                            // ... schieß!
                    shootTimer = 0f;                    // setze den shootTimer auf 0
                } else {                                // ansonsten
                    shootTimer += Time.deltaTime;       // addiere Zeit auf den wert des shootTimer
                }
            } else {                                    // ansonsten
                delayTimer += Time.deltaTime;           // addiere Zeit auf den wer des shootTimer
            }
        }
    }

    public void shoot() {                                                                           // Erstelle shoot
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);    // Erstelle eine neue bullet an dieser Position, ohne rotation
        Bullet goBullet = go.GetComponent<Bullet>();                                                // Hohl das Bullet script der Bullet
        goBullet.direction = direction;                                                             // gib der neuen Bullet die direction
    }
}

