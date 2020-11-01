using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IsometricPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_MovementSpeed = 1f;

    [SerializeField]
    private Transform m_MovementPoint;

    private Rigidbody2D m_Rbody;

    void Awake()
    {
        m_Rbody = GetComponent<Rigidbody2D>();
        m_MovementPoint.position = transform.position;
        
    }

    private void Update()
    {
        Movement2();
    }

    private void Movement()
    {
        Vector2 currentPos = m_Rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector,1);
        Vector2 movement = inputVector * m_MovementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //m_IsoRenderer.SetDirection(movement);
        m_Rbody.MovePosition(newPos); 
    }

    private void Movement2()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_MovementPoint.position += Vector3.right;

            if (CheckCollider(m_MovementPoint))
            {
                StartCoroutine(MoveCoroutine());
            }
            else
            {
                m_MovementPoint.position = transform.position;
            }


        }
    }

    IEnumerator MoveCoroutine()
    {
        while (Vector3.Distance(transform.position, m_MovementPoint.position)> 0.005f)
        {
            transform.position = Vector3.Lerp(transform.position, m_MovementPoint.position, m_MovementSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = m_MovementPoint.position;
        Debug.Log("Reached target " + transform.position);
    }

    private bool CheckCollider(Transform posToCheck)
    {
        Collider2D col = Physics2D.OverlapPoint(posToCheck.position);
        if (col != null)
        {
            Debug.Log("Found him " + col.name);
            return false;
        }
        return true;
    }

}
