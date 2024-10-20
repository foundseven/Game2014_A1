using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    //creating an instance
    public static PlayerBehaviour instance { get; set; }

    [SerializeField] 
    private float _speed;

    [SerializeField]
    private int _lives = 3;

    [SerializeField] 
    private Boundry _horizontalBoundry;

    [SerializeField] 
    private Boundry _verticalBoundry;

    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootingPoint;


    bool _isTestMobile;
    bool _isMobilePlatform = true;

    Camera _camera;
    Vector2 _destination;
    public SpriteRenderer _spriteRenderer;
    private ColorManager _colorManager;

    //getters n setters if needed
    public Color CurrentColor { get; private set; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorManager = FindObjectOfType<ColorManager>();

        if (!_isTestMobile)
        {
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                            Application.platform == RuntimePlatform.IPhonePlayer;
        }

        StartCoroutine(ChangeColorofPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMobilePlatform)
        {
            GetTouchInput();
            Debug.Log("Mobile working");
        }
        else
        {
           // Debug.Log("Trad working");
            GetTraditionalInput();
        }

        Move();
        CheckBoundaries();
        ShootWithSpacebar();
    }

    void Move()
    {
        transform.position = _destination;
    }

    //check what kind of input we need and use accordingly
    void GetTraditionalInput()
    {
        //init the movement
        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        //make the movement do its thing
        _destination = new Vector3(axisX + transform.position.x, axisY + transform.position.y, 0);
    }
    void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            _destination = _camera.ScreenToWorldPoint(touch.position);
            _destination = Vector2.Lerp(transform.position, _destination, _speed * Time.deltaTime);

            if(touch.phase == TouchPhase.Began)
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _shootingPoint.position, Quaternion.identity);

        Transform closestEnemy = FindClosestEnemy(); // Find the closest enemy

        if (closestEnemy != null)
        {
            projectile.GetComponent<Projectile>().MoveToEnemy(closestEnemy); // Pass the target to the projectile
        }
    }

    void ShootWithSpacebar()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    Transform FindClosestEnemy()
    {
        //make an array of enemies from enemy behaviour
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        Transform closestEnemy = null;

        float minDistance = Mathf.Infinity;

        //for each enemy in the enemy array
        foreach (EnemyBehaviour enemy in enemies)
        {
            //calc the POS
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            //if the distance is < than the min
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }


    void CheckBoundaries()
    {
        //check if player is going past the boundry
        //if they do switch side
        if (transform.position.x > _horizontalBoundry.max)
        {
            transform.position = new Vector3(_horizontalBoundry.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundry.min)
        {
            transform.position = new Vector3(_horizontalBoundry.max, transform.position.y, 0);
        }

        //create the boundry stopper here
        if (transform.position.y > _verticalBoundry.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundry.max, 0);
        }

        else if (transform.position.y < _verticalBoundry.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundry.min, 0);
        }
    }

    void SetRandomColor()
    {
        CurrentColor = _colorManager.GetRandomColor();
        _spriteRenderer.color = CurrentColor;
    }

    //Coroutine to change the players color
    IEnumerator ChangeColorofPlayer()
    {
        while(true) 
        {
            SetRandomColor();

            //waiting eloted seconds
            yield return new WaitForSeconds(5f);

        }
    }

    public void LoseLife()
    {
        Debug.Log($"Lives left: {_lives}");
        HeartSystem.instance.TakeHeartDamage(1);
    }
}
