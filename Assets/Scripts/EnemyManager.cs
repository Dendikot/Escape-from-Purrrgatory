using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Transform[] EnemyGroup;
    public Transform[] GetEnemyGroup { get { return EnemyGroup; } }

    // Manages all the variables needed for Enemy-Logic outside of combat, e.g. TilePrinter
    void Awake() {
        UpdateEnemyGroup();
    }

    public void UpdateEnemyGroup() {
        int ArraySize = transform.childCount;
        EnemyGroup = new Transform[ArraySize];
        for (int nInd = 0; nInd < ArraySize; nInd++) {
            EnemyGroup[nInd] = transform.GetChild(nInd);
        }
    }

}
