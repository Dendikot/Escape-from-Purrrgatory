using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBehaviour : MonoBehaviour
{

    private bool playerTurn = false;
    private bool enemyTurn = false;

    private int playerActions = 0;
    private int enemyActions = 0;

    private enum State {
        PlayerTurn,
        Waiting,
        EnemyTurn
    }

    private State state;

    // Start is called before the first frame update
    void Awake()
    {
        state = State.PlayerTurn;   
    }

    // Update is called once per frame
    void Update() {
        switch(state) {
            case State.PlayerTurn: 
                IsoGame.Access.TilePrinter.DestroyMovableTiles();
                for(int nInd = 0; nInd < IsoGame.Access.EnemyManager.GetEnemyGroup.Length; nInd++) {
                    if (IsoGame.Access.EnemyManager.GetEnemyGroup[nInd] != null) {
                        IsoGame.Access.TilePrinter.DestroyCollisionTiles(IsoGame.Access.EnemyManager.GetEnemyGroup[nInd].GetComponent<EnemyDummy>());
                    }
                }
                playerTurn = true;

                for(int nInd = 0; nInd < IsoGame.Access.EnemyManager.GetEnemyGroup.Length; nInd++) {
                    if (IsoGame.Access.EnemyManager.GetEnemyGroup[nInd] != null) {
                        IsoGame.Access.TilePrinter.PrintCollisionTiles(IsoGame.Access.EnemyManager.GetEnemyGroup[nInd].GetComponent<EnemyDummy>());
                    }
                }
                IsoGame.Access.TilePrinter.PrintMovableTiles();                
                state = State.Waiting;
                break;
            case State.EnemyTurn:
                IsoGame.Access.EnemyManager.UpdateEnemyGroup();
                IsoGame.Access.TilePrinter.DestroyMovableTiles();
                for(int nInd = 0; nInd < IsoGame.Access.EnemyManager.GetEnemyGroup.Length; nInd++) {
                    if (IsoGame.Access.EnemyManager.GetEnemyGroup[nInd] != null) {
                        IsoGame.Access.TilePrinter.DestroyCollisionTiles(IsoGame.Access.EnemyManager.GetEnemyGroup[nInd].GetComponent<EnemyDummy>());
                    }
                }
                enemyTurn = true;
                IsoGame.Access.TilePrinter.PrintMovableTiles();
                for(int nInd = 0; nInd < IsoGame.Access.EnemyManager.GetEnemyGroup.Length; nInd++) {
                    if (IsoGame.Access.EnemyManager.GetEnemyGroup[nInd] != null) {
                        IsoGame.Access.TilePrinter.PrintCollisionTiles(IsoGame.Access.EnemyManager.GetEnemyGroup[nInd].GetComponent<EnemyDummy>());
                    }
                    IsoGame.Access.EnemyManager.TriggerEnemyAttacks();
                }
                state = State.Waiting;
                break;
            case State.Waiting:
                if (playerActions >= 2) {
                    playerTurn = false;
                    playerActions = 0;
                    state = State.EnemyTurn;
                }
                if (enemyActions == 1) {
                    enemyTurn = false;         
                    enemyActions = 0; 
                    state = State.PlayerTurn;
                }
                break;
        }
    }

    public bool isPlayerTurn() {
        return playerTurn;
    }

    public bool isEnemyTurn() {
        return enemyTurn;
    }

    public void IncreasePlayerActions() {
        playerActions += 1;
    }

    //Possibly a Placeholder for Testing
    public void EndEnemyTurn() {
        enemyActions = 1;
    }
}
