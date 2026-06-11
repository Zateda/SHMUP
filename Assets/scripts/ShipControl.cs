using System;
using UnityEngine;

public class ShipControl : MonoBehaviour {          // Hier steht die Klasse und was auch immer MonoBehaviour bedeutet.
    // >>> Hier stehen die Variablen die in diversen Funktionen dieser Klasse genutzt werden!
    private float moveSpeed = 5;            

    private bool moveUp;                

    private bool moveDown;

    private bool moveRight;

    private bool moveLeft;
    // <<< Hier stehen die Variablen die in diversen Funktionen dieser Klasse genutzt werden!
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update() {     // >>> Update ist etwas das wiederholt ausgeführt wird, hier werden richtungen mit Tasten verbunden.
        moveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        moveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }                   // <<< Update ist etwas das wiederholt ausgeführt wird, hier werden richtungen mit Tasten verbunden.

    private void FixedUpdate() {    // Eine weitere Updatefunktion?
        Vector3 pos = transform.position;       // Eine Vectorposition namens pos die transform.postion bekommt

        float moveAmount = moveSpeed * Time.fixedDeltaTime;     // eine Variable die eine Zahl enthält bestehend aus Speed * Time
        Vector3 move = Vector3.zero;

        if (moveUp) {
            move.y += moveAmount;
        }
        if (moveDown) {
            move.y -= moveAmount;
        }
        if (moveRight) {
            move.x += moveAmount;
        }
        if (moveLeft) {
            move.x -= moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount) {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

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

        pos += move;
        transform.position = pos;
    } 
}
