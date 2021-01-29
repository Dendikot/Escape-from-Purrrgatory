using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDummy 
{
    IEnumerator Move();
    void ReceiveDamage(int damage);
}
