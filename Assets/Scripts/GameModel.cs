using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameModel : MonoBehaviour
{
    //THis is for Testing and Stuff
    #region Singleton

    public static GameModel instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField]
    public Grid grid;
    [SerializeField]
    public Tilemap tilemap;

    [SerializeField]
    public Enemy[] enemies;

}
