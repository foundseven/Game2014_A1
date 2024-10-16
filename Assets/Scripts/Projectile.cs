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

    //destroy  projectile when it leaves the screen for rn

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
