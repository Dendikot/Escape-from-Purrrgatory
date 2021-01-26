using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    B b1 = new B();

    A a1;
    A a2 = new A();

    object[] Arr = new object[3];

    [SerializeField]
    CharacterGroupController m;

    private void Start()
    {
        //Arr[0] = b1;
        //Arr[1] = false;
        //Arr[2] = 2;

        //for (int i = 0; i < Arr.Length; i++)
        //{
        //    A t = Arr[i] as A;
        //    if (t != null)
        //    {
        //        Debug.Log("I am a good programmer");
        //    }
        //}
    }
}

class A
{
    public int i = 1;
}

class B : A
{

}