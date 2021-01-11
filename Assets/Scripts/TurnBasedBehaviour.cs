using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBehaviour : MonoBehaviour
{



    private enum State {
        PlayerTurn,
        Waiting,
        EnemyTurn
    }

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        switch(state) {
            case State.PlayerTurn:
                state = State.Waiting;
                break;
            case State.EnemyTurn:
                state = State.Waiting;
                break;
            case State.Waiting:
                break;
        }
    }
}
