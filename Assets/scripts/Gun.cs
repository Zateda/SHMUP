using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet bullet;
    private Vector3 direction;
    public bool autoShoot = false;
    public float shootInterval = 0.5f;
    public float shootDelay = 0.0f;
    private float shootTimer = 0f;
    private float delayTimer = 0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() {
        direction = (transform.localRotation * Vector3.right).normalized;

        if (autoShoot) {
            if (delayTimer >= shootDelay) {
                if (shootTimer >= shootInterval) {
                    shoot();
                    shootTimer = 0f;
                } else {
                    shootTimer += Time.deltaTime;
                }
            } else {
                delayTimer += Time.deltaTime;
            }
        }
    }

    public void shoot() {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}

