using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTest : MonoBehaviour
{
    private Enemy[] m_Enemies = new Enemy[3] { new Enemy(), new Enemy(), new Enemy() };
    private Enemy Enemy_1 = new Enemy();

    private List<Enemy> m_EnemiesList = new List<Enemy>() { new Enemy(), new Enemy(), new Enemy() };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(SeveralCalls());
        }
    }

    public IEnumerator SeveralCalls()
    {


        for (int nInd = 0; nInd < m_Enemies.Length; nInd++)
        {
            //yield return StartCoroutine(Call());
            yield return StartCoroutine(m_Enemies[nInd].EnemyMove());
            //this plu stack or queue with removing possibility//or even list
        }

        //cal it from a separate method
    }


    private IEnumerator Caller()
    {
        //yield return StartCoroutine(Call());
        //yield return StartCoroutine(Call());

        Debug.Log(m_Enemies.Length);
        IEnumerator n = m_Enemies[0].EnemyMove();
        if (n != null)
        {

            Debug.Log("not null");
        }
        yield return StartCoroutine(m_Enemies[0].EnemyMove());
        Debug.Log("call finished");
    }

    private IEnumerator Call()
    {
        yield return new WaitForSeconds(2f);
    }




    /* what you do is 
     * you have enemies as objects with data 
     * then you add and remove them from a list
     * and the iteration itself for each enemy and is transform is done via calling the method
     */

}


public class Enemy
{
    // deprecated of the coorutines
    //all coroutine logic is done in the manager
    //only holds fields/position/data  + special methods
    public void MoveEnemyCoroutineCall()
    {
        //StartCoroutine(EnemyMove());
    }

    public IEnumerator EnemyMove()
    {
        int distance = 10;


        while (distance > 0)
        {
            Debug.Log(distance);
            distance--;
            yield return null;
        }

        yield break;
    }
}

/*
 * Ho the system works
 * Player makes two moves -- if int is == 2 then the enemy move is triggered
 * enemy move is a ienum list of enemies that evaluate
 * 
 * at the end player move counter is reset and control is given to the player
 * 
 * bool in iso game for the player move implement it
 * turn controller acceses it
 * 
 * enemies can subscribe to the list and once its ienumerator is triggered 
 * enemy move and hit(ai)
 * 
 * 
 */
