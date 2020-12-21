using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        Transform projTransform = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Transform>();
        StartCoroutine(SendProjectile(projTransform));
    }

    private IEnumerator SendProjectile(Transform projectileTransform)
    {
        float elapsedTime = 0f;

        Vector3 originalPosition = projectileTransform.position;
        Vector3 targetPosition = originalPosition + (IsoGame.Access.Directions.left * 12);


        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
        {
            projectileTransform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        projectileTransform.position = targetPosition;
    }

    //Refractor player group Movement
    //Create attack in different directions solution(maybe connected to player movement)
    //Projectile detection
}