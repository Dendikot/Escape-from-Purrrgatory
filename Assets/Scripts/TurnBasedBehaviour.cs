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
                IsoGame.Access.GroupController.DestroyMovableTiles();
                playerTurn = true;
                IsoGame.Access.GroupController.PrintMovableTiles();
                state = State.Waiting;
                break;
            case State.EnemyTurn:
                IsoGame.Access.GroupController.DestroyMovableTiles();
                enemyTurn = true;
                IsoGame.Access.GroupController.PrintMovableTiles();
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
