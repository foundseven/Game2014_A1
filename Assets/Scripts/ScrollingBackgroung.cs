using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroung : MonoBehaviour
{
    //holding my variables 
    [SerializeField] 
    private float _speed = 3.0f;

    private Vector3 _direction = Vector3.down;

    [SerializeField] 
    private Boundry _boundry;

    [SerializeField] 
    private Vector3 _spawnPosition;

    // Update is called once per frame
    void Update()
    {
        //each frame move the backgrounds pos by multiplying the direction vector by the speed and time.deltatime
        transform.position += _direction * _speed * Time.deltaTime;
        //checks the boundry and "respawns" the background accordingly
        if (transform.position.y < _boundry.min)
        {
            transform.position = _spawnPosition;
        }
    }
}
