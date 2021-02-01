using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Button[] ButtonList;

    private CharacterGroupController groupController;
    private TurnController turnController;

    //why Color? Because you can't set alpha directly and Lena wants the image on the button to be transparent, too
    private Color c;

    void Awake() {
        groupController = IsoGame.Access.GroupController;
        turnController = IsoGame.Access.TurnController;
        c = Color.white;
    }

    // ButtonList[0] always needs to be Attack, [3] always needs to be end turn, rest doesn't matter
    void Update()
    {
        if(groupController.PlayerTurn == false) {
            c.a = 0.5f;
            for(int i = 0; i < ButtonList.Length - 1; i++) {
                ButtonList[i].interactable = false;
            }
            ButtonList[0].transform.GetChild(0).GetComponent<Image>().color = c;
            if(turnController.EnemyTurn) {
                ButtonList[3].interactable = false;
                ButtonList[3].transform.GetChild(0).GetComponent<Image>().color = c;
            }
        }

        if(groupController.PlayerTurn) {
            c.a = 1f;
            for(int i = 0; i < ButtonList.Length - 1; i++) {
                ButtonList[i].interactable = true;
            }
            ButtonList[0].transform.GetChild(0).GetComponent<Image>().color = c;
            if(turnController.EnemyTurn == false) {
                ButtonList[3].interactable = true;
                ButtonList[3].transform.GetChild(0).GetComponent<Image>().color = c;
            }        
        }
    }
}
