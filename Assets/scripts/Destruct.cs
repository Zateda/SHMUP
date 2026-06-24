using Unity.VisualScripting;
using UnityEngine;


public class Destruct : MonoBehaviour {
    private bool canBeDestroyed = false;            // Ein schallter namens canBeDestroyed

    public int scoreValue = 1;                      // Ein ganzzahlenwert namens scoreValue mit 1
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Level.instance.addEnemies();                // Fürt beim Start einer Level instanz addEnemie aus.
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -14) {                              // wird auf der x achse position -14 überschritten...
            Destroy(gameObject);                        // NUKE dieses gameObject!
        }
        if (transform.position.x < 10.50f && !canBeDestroyed) {        // Wenn dieses kleiner ist als position 10,50 
            canBeDestroyed = true;                  // schalte canBeDestroyed auf true
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns) {
                gun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter(Collider Collision) {           // Funktion namens OnTriggerEnter
        if (!canBeDestroyed) {                                  // wenn canBeDestroyed false ist
            return;                                             // zurück
        }

        Bullet bullet = Collision.GetComponent<Bullet>();       // 
        if (bullet != null) {                                //
            if (!bullet.isEnemy) {                              //
                Level.instance.addScore(scoreValue);            //
                Level.instance.removeEnemies();     // führe removeEnemies aus.
                Destroy(gameObject);                            //
                Destroy(bullet.gameObject);                     //
            }

        }
    
    }

}
