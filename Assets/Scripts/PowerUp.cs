using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private bool isHealth;
    [SerializeField]
    private bool isForce;

    [SerializeField]
    private int value;

    public void TriggerPowerUp(Transform character) {
        if(character != null) {
            if (isHealth) {
                character.GetComponent<PlayerCombat>().GetStats.Health += value;
                Destroy(this.transform.GetComponentInChildren<PolygonCollider2D>().gameObject);
            }
            if (isForce) {
                character.GetComponent<PlayerCombat>().GetStats.Attack += value;
                Destroy(this.transform.GetComponentInChildren<PolygonCollider2D>().gameObject);
            }
        }
    }
}
