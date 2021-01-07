using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroupController : MonoBehaviour
{
    [SerializeField]
    private IsometricCharacterMovement[] m_CharactersControllers;

    [SerializeField]
    private LayerMask collidableObjects;

    [SerializeField]
    private Transform[] Characters;

    private Vector3[] Positions;
    private SpriteRenderer[] Sprites;

    private int subInd = 0;

    private bool m_isMoving = false;

    [SerializeField]
    private Collider2D m_CollisionChecker;

    private enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    private int SubInd
    {
        get
        {
            return subInd;
        }
        set
        {
            if (value < 0)
            {
                value = 3;
            }
            else if (value >= Characters.Length)
            {
                value -= Characters.Length;
            }
            subInd = value;
        }
    }

    private void Awake()
    {
        Positions = new Vector3[Characters.Length];
        Sprites = new SpriteRenderer[Characters.Length];

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Positions[nInd] = Characters[nInd].localPosition;
            Sprites[nInd] = Characters[nInd].GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        //check new input unity system once several consoles are confurmed
        Rotation();
        Movement();
    }

    private void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W) && !m_isMoving)
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
        }
        if (Input.GetKeyDown(KeyCode.S) && !m_isMoving)
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
        }
        if (Input.GetKeyDown(KeyCode.A) && !m_isMoving)
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));
        }
        if (Input.GetKeyDown(KeyCode.D) && !m_isMoving)
        {
            StartCoroutine(MovePlayer(IsoGame.Access.Directions.right));
        }
    }

    private void Rotate(int value)
    {
        SubInd += value;

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Characters[nInd].localPosition = Positions[IndexProcessor(SubInd, nInd)];
        }
    }


    /// <summary>
    /// Method to calculate final index for Positions array
    /// </summary>
    /// <param name="value1">SubInd</param>
    /// <param name="value2">nInd</param>
    /// <returns></returns>
    private int IndexProcessor(int value1, int value2)
    {
        int finalInd = value1 + value2;

        while (finalInd >= Positions.Length)
        {
            finalInd -= Positions.Length;
        }

        return finalInd;
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        if (CheckCollision(direction))
        {
            yield break;
        }
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

    public bool CheckCollision(Vector3 direction)
    {
        Collider2D Collider;

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Collider = Physics2D.OverlapPoint(Characters[nInd].position + direction, collidableObjects);
            if (Collider != null)
            {
                return true;
            }
        }
        return false;
    }
}