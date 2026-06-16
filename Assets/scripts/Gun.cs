using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet bullet;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() {
        direction = (transform.localRotation * Vector3.right).normalized;
    }

    public void shoot() {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}

