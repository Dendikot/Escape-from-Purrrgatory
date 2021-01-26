using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : MonoBehaviour, IEnemyDummy
{
    
    public IEnumerator Move()
    {
        int I = 10;
        while (I > 0)
        {
            Debug.Log(I);
            I--;
        }
        yield return null;
    }

    void Start()
    {
        IsoGame.Access.CurrentEnemeis.Add(this);
    }
}