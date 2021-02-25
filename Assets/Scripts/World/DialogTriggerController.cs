using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogTriggerController : MonoBehaviour
{
    public bool onEnemyDefeated = false;
    public bool onSeeingCat = false;
    public bool onSeeingTotem = false;
    public bool onSavingCat = false;

    [SerializeField]
    private LayerMask dialogTriggerLayer;

    private GameObject[][] allTriggers;

    [SerializeField]
    private GameObject[] seeingCatTriggers;
    [SerializeField]
    private GameObject[] seeingTotemTriggers;
    [SerializeField]
    private GameObject[] savingCatTriggers;

    void Start() {
        allTriggers = new GameObject[][] {
            seeingCatTriggers,
            seeingTotemTriggers,
            savingCatTriggers
        };
    }

    public void CheckForTrigger(Transform player) {
        Collider2D col = null;
        foreach(GameObject[] triggers in allTriggers) {

            //pls don't kill me
            //I'm more than capable of doing that myself
            for(int i = 0; i < seeingCatTriggers.Length; i++) {
                col = Physics2D.OverlapPoint(player.position, dialogTriggerLayer);
                if(col != null) {
                    if(col.gameObject == seeingCatTriggers[i] && onSeeingCat == false) {
                        SendMessage("SeeingCat");    
                        onSeeingCat = true;
                        col.transform.parent.gameObject.SetActive(false);
                    }
                }
            }

            for(int i = 0; i < seeingTotemTriggers.Length; i++) {
                col = Physics2D.OverlapPoint(player.position, dialogTriggerLayer);
                if(col != null) {
                    if(col.gameObject == seeingTotemTriggers[i] && onSeeingTotem == false) {
                        SendMessage("SeeingTotem");
                        onSeeingTotem = true;
                        col.transform.parent.gameObject.SetActive(false);
                    }
                }
            }

            for(int i = 0; i < savingCatTriggers.Length; i++) {
                col = Physics2D.OverlapPoint(player.position, dialogTriggerLayer);
                if(col != null) {
                    if(col.gameObject == savingCatTriggers[i] && onSavingCat == false) {
                        SendMessage("SavingCat");
                        onSavingCat = true;
                        col.transform.parent.gameObject.SetActive(false);
                    }               
                }
            }
        }
    }


    public void SendMessage(string message) {
        Fungus.Flowchart.BroadcastFungusMessage(message);
    }

}
