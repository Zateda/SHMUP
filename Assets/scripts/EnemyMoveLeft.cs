using System;
using UnityEngine;

public class EnemyMoveLeft : MonoBehaviour {
    public float moveSpeed = 5;             // Erstelle moveSpeed mit dem wert 5 öffentlich so das ich ihn anpassen kann.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {                        
        Vector3 pos = transform.position;               // Erstelle pos und gib ihm eine position

        pos.x -= moveSpeed * Time.fixedDeltaTime;       // gib dem x wert von pos ein minus moveSpeed mal der Zeit die vergeht.

        

        transform.position = pos;                       // pack die neue postion in unsere pos.
    }
}
