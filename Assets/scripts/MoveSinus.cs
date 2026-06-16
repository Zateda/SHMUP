using UnityEngine;

public class MoveSinus : MonoBehaviour {
    private float sinCenter;

    public float amplitude = 2;
    public float frequency = 2;
    public bool invert = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        sinCenter = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        Vector3 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;
        if (invert) {
            sin *= -1;
        }

        pos.y = sinCenter + sin;

        transform.position = pos;
    }
}
