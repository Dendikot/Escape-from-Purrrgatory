using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour, IEnemyDummy
{

    //How can we move this to parent interface in a smart way? (Too stupid to understand right now)
    Stats stats;

    //Will move all of this to a centralized place instead of having it here, it's just for testing and the sake of not creating a new class for that right now
    private DirectionsModel m_Directions;
    [SerializeField]
    private LayerMask collidablePlayers;

    void Awake() {
        stats = new Stats(10, 30);
        m_Directions = IsoGame.Access.Directions;
    }
    
    public IEnumerator Move()
    {
        Collider2D playerCollider = GetPlayerCollider(gameObject.transform, 1);
        if (playerCollider != null) {
            Debug.Log(gameObject + " found ");
            Debug.Log(playerCollider.transform.parent);
            playerCollider.transform.parent.GetComponent<PlayerCombat>().ReceiveDamage(stats.Attack);
            stats.Attack--;
            yield break;
        }

        int I = 10;
        Debug.Log("Didn't find the Player");
        while (I > 0)
        {
            I--;
        }

        playerCollider = GetPlayerCollider(gameObject.transform, 1); 
        if (playerCollider != null) {
            Debug.Log("Attack 2!");
            playerCollider.transform.parent.GetComponent<PlayerCombat>().ReceiveDamage(stats.Attack);
            stats.Attack--;            
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


    //Will move this to centralized place so we have it only once
    Collider2D GetPlayerCollider(Transform enemy, int range) {
        Collider2D playerCollider = null;

        for(int nInd = 0; nInd < m_Directions.directionsArr.Length; nInd++) {
            playerCollider = Physics2D.OverlapPoint(enemy.position + (m_Directions.directionsArr[nInd] * range), collidablePlayers);
            if (playerCollider != null) {
                return playerCollider;
            }
        }

        return playerCollider;
    }
}