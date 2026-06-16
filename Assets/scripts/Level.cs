using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    private int numDstruct = 0;

    private bool startNextLevel = false;

    private float nextLevelTimer = 3;

    private string[] levels = { "Level 1", "Level 2" };

    private int currentLevel = 1;

    public static Level instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startNextLevel) {
            if (nextLevelTimer <= 0) {
                currentLevel++;
                if (currentLevel <= levels.Length) {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                }else {
                    Debug.Log("GAME OVER!");
                }

                nextLevelTimer = 3;
                startNextLevel = false;
            } else {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    public void addEnemies() {
        numDstruct++;
    }

    public void removeEnemies() {
        numDstruct--;
        if (numDstruct == 0) {
            startNextLevel = true;

        }
    }
}
