using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IsometricCharacterMovement : MonoBehaviour
{
    private Rigidbody2D m_Rbody;

    private bool m_isMoving;

    [SerializeField]
    private LayerMask collidableObjects;
    [SerializeField]
    private LayerMask collidableTiles;

    void Awake()
    {
        m_Rbody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        m_isMoving = true;

        float elapsedTime = 0f;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + direction;

        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        m_isMoving = false;
    }

    public bool CheckCollidableObject(Vector3 posToCheck, LayerMask mask)
    {
        posToCheck += transform.position;

        //Replace with checking area
        //Check what is more exprensive try cast/try get
        //or double overlap check
        //Apply those
        //Get to the manager
        // Physics2D.OverlapCircle()
        Collider2D Collider = Physics2D.OverlapPoint(posToCheck, mask);
        if (Collider != null)
        {
            Debug.Log("Found him " + Collider.name);
            return false;
        }
        return true;
    }
}