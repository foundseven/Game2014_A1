using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public static HeartSystem instance { get; set; }

    public GameObject[] _hearts;

    private int _life;
    private bool _dead;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _life = _hearts.Length;
    }
    void Update()
    {
        if(_dead == true)
        {
            //set dead code
            Debug.Log("GAME OVER");
        }
    }

    public void TakeHeartDamage(int d)
    {
        if(_life >= 1)
        {
            _life -= d;
            Destroy(_hearts[_life].gameObject);

            if(_life < 1)
            {
                _dead = true;
            }
        }
    }
}
