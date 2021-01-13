using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LayerModel
{
   public LayerModel(CollisionTilePrinter dummy) {

       this.dummy = dummy;

       collidableEnemies = 8;
       collidableNeutralEnemies = 9;
       collidableObjects = 10;
       collidablePlayers = 11;
   }

    private CollisionTilePrinter dummy;

    public LayerMask collidableEnemies;
    public LayerMask collidableNeutralEnemies;
    public LayerMask collidableObjects;
    public LayerMask collidablePlayers;
}
