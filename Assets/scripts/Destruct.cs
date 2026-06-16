using Unity.VisualScripting;
using UnityEngine;


public class Destruct : MonoBehaviour {
    private bool canBeDestroyed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Level.instance.addEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 10.50f) {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerEnter(Collider Collision) {
        if (!canBeDestroyed) {
            return;
        }

        Bullet bullet = Collision.GetComponent<Bullet>();
        if (bullet != null) {
            if (!bullet.isEnemy) {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }

        }
    
    }

    private void OnDestroy() {
        Level.instance.removeEnemies();
    }
}
