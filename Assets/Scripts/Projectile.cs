using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 3f;

    private Vector2 _targetDirection;
    public GameObject _explosionEffect;

    [SerializeField]
    AudioClip _hitSound;



    void Update()
    {
        //moves the porjectile upwards every frame
        //transform.Translate(Vector2.up * _speed * Time.deltaTime);
        //move towards enemy
        transform.Translate(_targetDirection * _speed * Time.deltaTime);
    }

    public void MoveToEnemy(Transform enemyTransform)
    {
        _targetDirection = (enemyTransform.position - transform.position).normalized;
    }

    //destroy
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit the enemy!");
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Debug.Log("Explosion instantiated at: " + transform.position);
            SoundManager.instance.PlayAudioClip(_hitSound);
            StartCoroutine(HandleEnemyHit(collision));
        }
    }

    private IEnumerator HandleEnemyHit(Collider2D enemyCollider)
    {
        yield return enemyCollider.GetComponent<EnemyBehaviour>().DyingRoutine();
        ScoreManager.instance.ChangeScore(10);
        DestroyProjectile();
    }
}
