using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour {
    private bool canBeDestroyed = false;            // Ein schallter namens canBeDestroyed

    public int scoreValue = 1;                      // Ein ganzzahlenwert namens scoreValue mit 1
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Level.instance.AddEnemies();                // Meldet dieses Object an Level?
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -12) { // wird auf der x achse position -12 überschritten...
            Level.instance.RemoveEnemies();
            Destroy(gameObject);                                    // NUKE dieses gameObject!
        }
        if (transform.position.x < 10.50f && !canBeDestroyed) {     // Wenn dieses kleiner ist als position 10,50 UND canBeDestroyed false ist
            canBeDestroyed = true;                                  // schalte canBeDestroyed auf true
            Gun[] guns = transform.GetComponentsInChildren<Gun>();  // sucht alle Gun und packt sie ins array guns
            foreach (Gun gun in guns) {                             // Für jede gun aus dem guns array aus dem Gun script ...
                gun.isActive = true;                                // ... setze isActive auf true
            }
        }
    }

    private void OnTriggerEnter(Collider collision) {           // Funktion namens OnTriggerEnter wir aufgerufen soblad ein trigger berührt wird
        if (!canBeDestroyed) {                                  // wenn canBeDestroyed false ist
            return;                                             // zurück
        }

        Bullet bullet = collision.GetComponent<Bullet>();       // Prüft on das berürte object eine bullet hat und speichert es in bullet
        if (bullet != null) {                                // Wenn bullet null ist ...
            if (!bullet.isEnemy) {                              // Wenn bullet nicht isEnemy bullet ist ...
                Level.instance.addScore(scoreValue);            // führe aus Level addScore aus mit dem wert von scoreValue
                Level.instance.RemoveEnemies();                 // führe RemoveEnemies aus.
                Destroy(gameObject);                            // Zerstöre das Gameobject
                Destroy(bullet.gameObject);                     // Zerstöre das bulletObject
            }

        }
    
    }

}
