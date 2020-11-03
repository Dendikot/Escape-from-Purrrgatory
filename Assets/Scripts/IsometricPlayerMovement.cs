using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IsometricPlayerMovement : MonoBehaviour
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

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W) && !m_isMoving && CheckCollidableObject((transform.position) + IsoGame.Access.Directions.up)
            && CheckCollidableTile((transform.position) + IsoGame.Access.Directions.up / 2.0f))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
        }

        if (Input.GetKeyDown(KeyCode.A) && !m_isMoving && CheckCollidableObject((transform.position) + IsoGame.Access.Directions.left)
            && CheckCollidableTile((transform.position) + IsoGame.Access.Directions.left / 2.0f))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));
        }

        if (Input.GetKeyDown(KeyCode.S) && !m_isMoving && CheckCollidableObject((transform.position) + IsoGame.Access.Directions.down)
             && CheckCollidableTile((transform.position) + IsoGame.Access.Directions.down / 2.0f))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
        }

        if (Input.GetKeyDown(KeyCode.D) && !m_isMoving && CheckCollidableObject((transform.position) + IsoGame.Access.Directions.right)
             && CheckCollidableTile((transform.position) + IsoGame.Access.Directions.right / 2.0f))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.right));
        }
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

    private bool CheckCollidableObject(Vector3 posToCheck)
    {

        if (Physics2D.OverlapPoint(posToCheck, collidableObjects) != null)
        {
            Debug.Log("Found him " + Physics2D.OverlapPoint(posToCheck, collidableObjects).name);
            return false;
        }
        return true;
    }
    private bool CheckCollidableTile(Vector3 posToCheck)
    {

        if (Physics2D.OverlapPoint(posToCheck, collidableTiles) != null)
        {
            Debug.Log("Found him " + Physics2D.OverlapPoint(posToCheck, collidableTiles).name);
            return false;
        }
        return true;
    }
}