using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 5f;

    void Update()
    {
        //moves the porjectile upwards every frame
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    //destroy
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(HandleEnemyHit(collision));
        }
    }

    private IEnumerator HandleEnemyHit(Collider2D enemyCollider)
    {
        yield return enemyCollider.GetComponent<EnemyBehaviour>().DyingRoutine();
        DestroyProjectile();
    }
}
