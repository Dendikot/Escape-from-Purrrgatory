using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    private List<EnemyDummy> m_Enemies;

    [SerializeField]
    private GameObject enemyMeleePrefab;
    [SerializeField]
    private GameObject enemyRangePrefab;
    [SerializeField]
    private GameObject enemyNeutralPrefab;


    //is there a Way to call this every time there's a change in CurrentEnemies?   
    public void UpdateEnemyUI() {
        m_Enemies = IsoGame.Access.CurrentEnemeis;
        foreach(Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < m_Enemies.Count; i++) {
            GameObject enemyStatusBox;

            //Sorry for uglying the Code! I will think of something to make it smarter than this like a method who does this shit
            if(m_Enemies[i].GetType() == typeof(EnemyNeutral)) {
                enemyStatusBox = Instantiate(enemyNeutralPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                InstantiateStatusBars(m_Enemies[i], enemyStatusBox);
            }
            if(m_Enemies[i].GetType() == typeof(EnemyMelee)) {
                enemyStatusBox = Instantiate(enemyMeleePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                InstantiateStatusBars(m_Enemies[i], enemyStatusBox);                
            }
            if(m_Enemies[i].GetType() == typeof(EnemyRange)) {
                enemyStatusBox = Instantiate(enemyRangePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                InstantiateStatusBars(m_Enemies[i], enemyStatusBox);                
            }
        }
    }

    private void InstantiateStatusBars(EnemyDummy enemy, GameObject enemyStatusBox) {
        enemy.GetStats.HealthBar = enemyStatusBox.transform.GetChild(0).gameObject.GetComponent<StatusBar>();
        enemy.GetStats.ForceBar = enemyStatusBox.transform.GetChild(1).gameObject.GetComponent<StatusBar>();

         //why is this neccessary? So that Statusbar doesn't have it's default value
        enemy.GetStats.Health = enemy.GetStats.Health;
        enemy.GetStats.Attack = enemy.GetStats.Attack;
    }
}
