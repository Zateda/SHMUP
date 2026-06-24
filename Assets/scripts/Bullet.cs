using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 direction = new Vector3(1,0,0);      // Erstelle direction mit dem wert 1 auf x

    public Vector3 velocity;                            // Erstelle velocity, speichert bewegungsrichtung und geschwindigkeit

    public bool isEnemy = false;                        // Erstelle einen bool isEnemy als false

    public float speed = 7;                             // Erstelle speed mit dem kommawert 7
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,4);                  // Zerstöre dieses Object nach 4 sec
        DontDestroyOnLoad(gameObject);            // Zerstöre dieses Object nicht wenn geladen wird
    }

    // Update is called once per frame
    void Update() {                             
        velocity = direction * speed;           // lass velocity die richtung mal geschwindigkeit sein
    }

    private void FixedUpdate() {
        Vector3 pos = transform.position;       // mach ne pos 
        
        pos += velocity * Time.fixedDeltaTime;  // addiere die pos mit velocity mal zeit

        transform.position = pos;               // speichere neue position in pos
    }
}
