using UnityEngine;


public class WaveSpawner : MonoBehaviour {
    public GameObject[] waves;
    
    private int currentWave = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartNextWave();
    }
    // Update is called once per frame
    void Update(){
    }
    public void StartNextWave() {
        if (currentWave >= waves.Length) {
            Debug.Log("FIN!");
            Level.instance.StartNextLevelTimer();
            return;
        }
        Instantiate(waves[currentWave], transform.position, Quaternion.identity);
        currentWave++;
    }

}
