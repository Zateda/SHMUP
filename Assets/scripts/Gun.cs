using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet bullet;                   // Erstelle bullet das auf oder aus Bullet kommt
    private Vector3 direction;              // Erstelle direction das von Vector 3 kommt
    public bool autoShoot = false;          // Erstelle den bool autoShoot auf false gestellt
    public float shootInterval = 0.5f;      // Erstelle shootInterval als kommazahl 0,5
    public float shootDelay = 0.0f;         // Erstelle shootDelay als kommatahl 0
    private float shootTimer = 0f;          // Erstelle shootTimer privat
    private float delayTimer = 0f;          // Erstelle delay Timer privat
    public int powerUpLevelRequierment = 0;

    public bool isActive = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() {

        if (!isActive) {
            return;
        }

        direction = (transform.localRotation * Vector3.right).normalized;   // Gib der direction eine drehung nach rechts und sag ihr das es der normalzustand ist.

        if (autoShoot) {                                // wird autoshoot ausgeführt...
            if (delayTimer >= shootDelay) {             // ist der delayTimer größer oder gleich dem shootIntervale ...
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
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);    // Keine ahung was ich hier mache!
        Bullet goBullet = go.GetComponent<Bullet>();                                                // SAME
        goBullet.direction = direction;                                                             // Ich sage was auch immer bekommt als richtung die richtun?
    }
}

