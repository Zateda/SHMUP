using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 direction = new Vector3(1,0,0);

    public Vector3 velocity;

    public float speed = 7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update() {
        velocity = direction * speed;
    }

    private void FixedUpdate() {
        Vector3 pos = transform.position;
        
        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
