using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float _verticalSpeed;
    private float _horizontalSpeed;

    private float _speed = 3;

    [SerializeField] Boundry _verticalSpeedRange;
    [SerializeField] Boundry _horizonalSpeedRange;

    [SerializeField] Boundry _verticalBoundry;
    [SerializeField] Boundry _horizontalBoundry;

    SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveEnemy();
        if(PlayerBehaviour.instance != null) 
        {
            TrackPlayer();
        }
        else
        {
            Debug.Log("No instance set!");
        }
    }

    void MoveEnemy()
    {
        transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time,
                           _horizontalBoundry.max - _horizontalBoundry.min) + _horizontalBoundry.min,
                           transform.position.y + _verticalSpeed * Time.deltaTime);

        //checks if player is off the screen from the bottom and resets accordingly
        if (transform.position.y < _verticalBoundry.min)
        {
            Reset();
        }
    }

    void TrackPlayer()
    {
        //getting the players location
        Vector3 playerPosition = PlayerBehaviour.instance.transform.position;

        //get the direction to the player
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;

        //move towards the player accordingly
        transform.position += directionToPlayer * _speed * Time.deltaTime;

    }

    public IEnumerator DyingRoutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Reset();
    }
    public void DyingSequence()
    {
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    //resets the enemys position and speeds
    private void Reset()
    {
        _spriteRenderer.color = Color.green;//_colors[Random.Range(0, _colors.Length)];
        _spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        //transform.localScale = new Vector3(1f + Random.Range(-.3f, .3f), 1f + Random.Range(-.3f, .3f), 1f);
        _verticalSpeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalSpeed = Random.Range(_horizonalSpeedRange.min, _horizonalSpeedRange.max);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("The enemy hit the player!");
            Reset();
        }
    }
}
