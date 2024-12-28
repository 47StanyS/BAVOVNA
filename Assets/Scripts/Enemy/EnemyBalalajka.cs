using System.Collections;
using UnityEngine;

public class EnemyBalalajka : MonoBehaviour
{
    [Header("Transform Poins")]
    [SerializeField] private Transform[] _points;
    [Header("Transform Target")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _lineOfste;
    [SerializeField] private float _shootingRange;
    private Vector3 _targetPosition;

    [Space]
    [Header("Moving Setings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private int _currentPoint;
    [SerializeField] private float _timer;
    [SerializeField] private bool _isMoving = true;
    [Header("Bullet")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletPoint;
    [SerializeField] private float _fireRate;
    private float _fireTime;

    private SpriteRenderer _spriteRenderer;
    private Transform _spriteTransform;
    private Animator _animator;
    private Rigidbody2D _rb2D;
    private void Start()
    {
        _spriteTransform = transform.GetChild(0);
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        _rb2D.velocity = Vector3.zero;

        if (_target != null) 
        {
            TargetFolow();
        }
        else
        {
            WalkOnPoints();
        }
        
    }
    private void Update()
    {
        if (transform.position.x > _targetPosition.x)
        {
            _spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < _targetPosition.x)
        {
            _spriteTransform.localScale = new Vector3(-1, 1, 1);
        } 
    }
    private void WalkOnPoints()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _speed);
            _targetPosition = _points[_currentPoint].position;
            _animator.SetInteger("State", 1);

        }


        if (transform.position == _points[_currentPoint].position)
        {
            _isMoving = false;
            _animator.SetInteger("State", 0);
            StartCoroutine(canMoveTimer());
            _currentPoint++;

            if (_currentPoint == _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
     private void TargetFolow()
     {
         float _distanceFromPlayer = Vector2.Distance(_target.position,transform.position);
        if (_distanceFromPlayer < _lineOfste && _distanceFromPlayer > _shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _target.position, _runSpeed);
            _targetPosition = _target.position;
        }
        else if (_distanceFromPlayer <= _shootingRange && _fireTime < Time.time) 
        {
            Instantiate(_bullet, _bulletPoint.transform.position, Quaternion.identity);
            _fireTime = Time.time + _fireRate;
        }

     }
      private void OnDrawGizmosSelected()
      {
          Gizmos.color = Color.yellow;
          Gizmos.DrawWireSphere(transform.position, _lineOfste);
          Gizmos.DrawWireSphere(transform.position, _shootingRange);
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

    private IEnumerator canMoveTimer()
    {
        yield return new WaitForSeconds(_timer);
        

        _isMoving = true;
    }
    
}

