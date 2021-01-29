using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbCat : MonoBehaviour, IEnemyDummy
{
    public IEnumerator Move()
    {
        Debug.Log("Dumb cat rulez");
        yield return null;
    }

    public void ReceiveDamage(int damage) {

    }

    // Start is called before the first frame update
    void Start()
    {
        IsoGame.Access.CurrentEnemeis.Add(this);
    }

    //enemy attack system + attack enemey logic with Flo
}