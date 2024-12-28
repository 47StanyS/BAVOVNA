using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private int _currentPoint;
    [SerializeField] private float _speed;
    [Space]
    [SerializeField] private float _speedAttak;
    [SerializeField] private float _speedCuldown;
    [SerializeField] private float _waitTime;
    [Space]
    [SerializeField] private int _damage;
    [SerializeField] private float _culdownTime;

    public bool _culdown = false;
    private bool _canFly = true;

    private Rigidbody2D _rb2D;
    private SpriteRenderer _spriteRenderer;
    private Transform _spriteTransform;

    [SerializeField]private Transform _target;
    private Vector3 _targetPositions;

    private void Start()
    {
        _spriteTransform = transform.GetChild(0);

        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rb2D.velocity = Vector3.zero;



        if(_target != null)
        {
            if (_culdown == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speedAttak);
                _targetPositions = _target.position;
            }
            else
            {
                Vector3 culdownPosition = _target.position;
                culdownPosition.y += 4;
                culdownPosition.x += -2;
                transform.position = Vector3.MoveTowards(transform.position, culdownPosition, _speedCuldown);
                _targetPositions = culdownPosition;
            }
                
        }
        else
        {
            PointsMovement();
        }

    }

    private void Update()
    {
        if (transform.position.x > _targetPositions.x)
        {
            _spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if(transform.position.x < _targetPositions.x)
        {
            _spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void PointsMovement()
    {
        if (_canFly)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _speed);
            _targetPositions = _points[_currentPoint].position;
           
            if (transform.position == _points[_currentPoint].position)
            {
                _canFly = false;
                StartCoroutine(canFlyTimer());

                _currentPoint++;

                if (_currentPoint == _points.Length)
                {
                    _currentPoint = 0;
                }
            }
        }

    }


    private IEnumerator canFlyTimer()
    {
        yield return new WaitForSeconds(_waitTime);

        _canFly = true ;
    }
    private IEnumerator culdownTimer()
    {
        yield return new WaitForSeconds(_culdownTime);

        _culdown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            _target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            _target = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("<<< DAMAGE >>>");
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage/2);
            _culdown = true;
            StartCoroutine(culdownTimer());
        }
    }

}