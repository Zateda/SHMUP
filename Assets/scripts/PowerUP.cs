using UnityEngine;

public class PowerUP : MonoBehaviour {      // Öffentliche Classe Power Up wird erstellt
    public bool activateShield;             // Ein öffentlicher bool(schalter) namens activeShield wird erstellt

    public bool addGuns;                    // Ein öffentlicher bool namens addGuns wird erstellt

    public bool speedUp;                    // Ein öffentlicher bool namens speedUp wird erstellt

    public int PointValue = 10;             // Zahlenwert Variable die auf 10 gesetzt ist
    
    // Dadurch das ich diese bools Öffentlich erstelle und das Script mit dem Gameobject PowerUP verbunden habe...
    // ... habe ich in Unity am PowerUP Obiect die mögliechkeit ein und aus zu schalten was das powerUp machen soll!
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -14) {                           // wird auf der x achse position -14 überschritten...
            Destroy(gameObject);                                    // NUKE dieses gameObject!
        }
    }
}
