using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Would it be possible / better / easier / some word / if this would be an abstract Class like PlayerCombat, instead of Interface?
public abstract class EnemyDummy : MonoBehaviour
{
    protected Stats stats;

    public Stats GetStats { get { return stats; } }

    [SerializeField]
    protected AudioSource[] audioSources;

    [SerializeField]
    protected int attackValue;
    [SerializeField]
    protected int healthValue;

    [SerializeField]
    protected Animator anim;

    [SerializeField]
    protected GameObject m_MoveTile;

    protected DirectionsModel m_Directions;

    [SerializeField]
    private LayerMask collidablePlayers;

    private PathFinding m_PathFinding;

    public abstract IEnumerator Move();

    void Awake()
    {
        stats = new Stats(attackValue, healthValue);
        m_Directions = IsoGame.Access.Directions;
        m_PathFinding = IsoGame.Access.PathFinding;
    }

    public IEnumerator MoveToDir()
    {
        float elapsedTime = 0f;

        Vector3 originalPosition = transform.position;

        Vector3 direction = m_PathFinding.GetDirectionEnemy(transform.position);
        Vector3 targetPosition = originalPosition - direction;

        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        IsoGame.Access.GroupController.CheckMovableTiles();

        audioSources[2].Play();

        this.GetComponent<PositionRendererSorter>().Layer();
        foreach(Transform child in this.transform) {
            if (child.GetComponent<OffsetRendererSorter>() != null) {
                child.GetComponent<OffsetRendererSorter>().Layer();
            }
        }
    }

    abstract public void ReceiveDamage(int damage);

    protected void Attack(Collider2D playerCollider)
    {
        playerCollider.transform.GetComponent<PlayerCombat>().ReceiveDamage(stats.Attack);
        stats.Attack--;
        anim.SetTrigger("Attack");
        audioSources[1].Play();
    }

    protected void Die()
    {
        RemoveFromList();
        anim.SetTrigger("Die");
        audioSources[3].Play();
        //yield return StartCoroutine(WaitForAnimation(anim.GetCurrentAnimatorStateInfo(0)));
        //Destroy(this.gameObject);
    }

    //   private IEnumerator WaitForAnimation(Animation animation) {
    //       do {
    //           yield return null;
    //       } while (animation.isPlaying);
    //   }

    protected void AddToList()
    {
        IsoGame.Access.CurrentEnemeis.Add(this);
        UpdateEnemyMoveTile();
        IsoGame.Access.EnemyUIManager.UpdateEnemyUI();
    }

    protected void RemoveFromList()
    {
        IsoGame.Access.CurrentEnemeis.Remove(this);
        DisableEnemyMoveTile();
        IsoGame.Access.EnemyUIManager.UpdateEnemyUI();
    }

    public void UpdateEnemyMoveTile() {
        m_MoveTile.SetActive(true);
        m_MoveTile.transform.position = transform.position - m_PathFinding.GetDirectionEnemy(transform.position);
    }

    public void DisableEnemyMoveTile() {
        m_MoveTile.SetActive(false);
        m_MoveTile.transform.position = transform.position;
    }

    protected Collider2D GetPlayerCollider(Transform enemy, int range)
    {
        Collider2D playerCollider = null;

        for (int nInd = 0; nInd < m_Directions.directionsArr.Length; nInd++)
        {
            playerCollider = Physics2D.OverlapPoint(enemy.position + (m_Directions.directionsArr[nInd] * range), collidablePlayers);
            if (playerCollider != null && playerCollider.transform.GetComponent<PlayerCombat>().GetStats.Health > 0)
            {
                return playerCollider;
            }
        }

        return playerCollider;
    }
}
