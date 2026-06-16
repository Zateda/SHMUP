using System;
using UnityEngine;

public class EnemyMoveLeft : MonoBehaviour {
    public float moveSpeed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Vector3 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (pos.x < -14) {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
