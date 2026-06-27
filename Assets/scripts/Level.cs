using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour {
    private int numEnemies = 0;                             // Variable mit zahlenwert 0

    private bool startNextLevel = false;                    // bool auf false

    private float nextLevelTimer = 3;                       // Kommerwert 3

    private string[] levels = { "Level 1", "Level 2" };     // Array mit Leveln

    private int currentLevel = 1;                           // Ganzzahl mit 1
    private int score = 0;                                  // Ganzzahl mit 0
    private TextMeshProUGUI scoreText;                      // UI Element mit scoreText

    public static Level instance;                           // Variable instance die nicht über ein Object sonern über die Klasse von überall aufgerufen werden kann

    private void Awake() {                                                              // Funktion namens Awake die noch vor Start aufgerufen werden kann.
        if (instance == null) {                                                      // wenn instance gleich null ist ...
            instance = this;                                                            // instance bekommt this (dieses Object/script)
            DontDestroyOnLoad(gameObject);                                              // führe DontDestroyOnLoad aus
            scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();  // gib dem scoreText die Info wo der Text im UI ist
        } else {                                                                        // ansonsten ...
            Destroy(gameObject);                                                        // ... mach kaputt!
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startNextLevel) {                                       // Wenn startNextLevel true ist ...
            if (nextLevelTimer <= 0) {                              // Wenn nextLevelTimer kleiner gleich 0 ist ...
                currentLevel++;                                     // ... currentLEvel +1
                if (currentLevel <= levels.Length) {                // Wenn currentLevel kleiner gleich levels länge ist ...
                    string sceneName = levels[currentLevel - 1];    // Hole namen der scene aus dem array - 1
                    SceneManager.LoadSceneAsync(sceneName);         // Läd das nächste LEvel?
                }else {                                             // ansonsten
                    Debug.Log("GAME OVER!");                        // Mach einen Debug Log und sag in der console GAME OVER!
                }

                nextLevelTimer = 3;                                 // ... setze den nextLevelTimer auf 3
                startNextLevel = false;                             // ... setze startNextLevel auf false
            } else {                                                // ansonsten
                nextLevelTimer -= Time.deltaTime;                   // ziehe vom nextLevelTimer die vergangene Zeit ab
            }
        }
    }

    public void ResetLevel() {                                          // Funktion ResetLevel
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>()) {  // Jede Bullet (b) wird gesucht
            Destroy(b.gameObject);                                      // zerstöre das gemeObject b
        }
        numEnemies = 0;                                                 // setze numDestruct auf 0
        score = 0;                                                      // setze Score auf 0
        addScore(score);                                                // setze addScore auf score
        string sceneName = levels[currentLevel - 1];                    // hold sceneName aus dem array Levels
        SceneManager.LoadScene(sceneName);                              // führe LoadScene aus
    }

    public void addScore(int scoreUp) {                                 // führe addScore aus
        score += scoreUp;                                               // addiere scoreUp auf den score
        scoreText.text = score.ToString();                              // Wandelt scoreText in einen String
    }

    public void AddEnemies() {                                          // führe die Funktion AddEnemies aus
        numEnemies++;                                                   // erhöhe numEnemies um 1
    }

    public void RemoveEnemies() {                                       // Führe die Funktion RemoveEnemies aus
        numEnemies--;                                                   // ziehe von numEnemies 1 ab
        if (numEnemies == 0) {                                          // wenn numEnemies gleich 0 ist ...
            FindAnyObjectByType<WaveSpawner>().StartNextWave();                        // schalte startNextLevel auf true
        }
    }
    public void StartNextLevelTimer() {
        startNextLevel = true;
    }
}
