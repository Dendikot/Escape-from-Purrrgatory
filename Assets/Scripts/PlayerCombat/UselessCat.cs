using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessCat : PlayerCombat
{
    void Awake()
    {
        base.Awake();
        forceBar = null;
    }

    public override IEnumerator Attack() {
        //Debug.Log("Useless Cat is Useless");
        yield return null;
    }
}
