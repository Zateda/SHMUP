/*
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject{
    [SerializeField] List<WaveBatch> Batches;
    public int Reward;

    public GameObject GetTopMostEnemy(){
        return Batches[0].EnemyType;
    }

    public float GetTopmostDelay(){
        return Batches[0].Amount <= 1 ? Batches[0].EndDelay : Batches[0].DelayPerEnemy;
    }

    public void Pop(){
        WaveBatch waveBatch = Batches[0]; // Wir holen uns die erste Batch als Kopie
        waveBatch.Amount -= 1; // Senken den Amount (Die übrigen zu spawnenden Gegner) um 1
        if (waveBatch.Amount <= 0){ // Sollte der Batch leer sein:
            Batches.RemoveAt(0); // Batch löschen
        }
        else{
            Batches[0] = waveBatch; // Kopie ersetzt die erste Batch
        }
    }

    public bool IsEmpty(){
        return Batches.Count <= 0;
    }
}*/
