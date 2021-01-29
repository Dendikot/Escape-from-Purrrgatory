using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : MonoBehaviour, IEnemyDummy
{

    //How can we move this to parent interface in a smart way? (Too stupid to understand right now)
    Stats stats;

    void Awake() {
        stats = new Stats(10, 30);
    }
    
    public IEnumerator Move()
    {
        int I = 10;
        while (I > 0)
        {
//            Debug.Log(I);
            I--;
        }
        yield return null;
    }

    public void ReceiveDamage(int damage) {
        stats.Health -= damage;
        Debug.Log(stats.Health);
    }

    void Start()
    {
        IsoGame.Access.CurrentEnemeis.Add(this);
    }
}