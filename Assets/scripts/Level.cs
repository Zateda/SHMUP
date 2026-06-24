using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour {
    private int numDstruct = 0;

    private bool startNextLevel = false;

    private float nextLevelTimer = 3;

    private string[] levels = { "Level 1", "Level 2" };

    private int currentLevel = 1;
    private int score = 0;
    private TextMeshProUGUI scoreText;

    public static Level instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
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

    public void ResetLevel() {
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>()) {
            Destroy(b.gameObject);
        }
        numDstruct = 0;
        score = 0;
        addScore(score);
        string sceneName = levels[currentLevel - 1];
        SceneManager.LoadScene(sceneName); 
    }

    public void addScore(int scoreUp) {
        score += scoreUp;
        scoreText.text = score.ToString();
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
