using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IsometricPlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_Rbody;

    private bool m_isMoving;

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
        if (Input.GetKeyDown(KeyCode.W) && !m_isMoving && CheckCollider((transform.position) + IsoGame.Access.Directions.up))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.up)); //Use 1/2 Cell Size here depending on the move direction, maybe theres a inherit grid solution
        }

        if (Input.GetKeyDown(KeyCode.A) && !m_isMoving && CheckCollider((transform.position) + IsoGame.Access.Directions.left))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));
        }

        if (Input.GetKeyDown(KeyCode.S) && !m_isMoving && CheckCollider((transform.position) + IsoGame.Access.Directions.down))
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
        }

        if (Input.GetKeyDown(KeyCode.D) && !m_isMoving && CheckCollider((transform.position) + IsoGame.Access.Directions.right))
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
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) *IsoGame.Access.LerpSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        
        m_isMoving = false;
    }

    private bool CheckCollider(Vector3 posToCheck)
    {

        Collider2D col = Physics2D.OverlapPoint(posToCheck);
        if (col != null)
        {
            Debug.Log("Found him " + col.name);
            return false;
        }

        return true;
    }
}