using System.Collections;
using UnityEngine;

public class EnemySoldger : MonoBehaviour
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


    [Header("Reload Settings")]
    [SerializeField] private int shotsBeforeReload = 3;
    [SerializeField] private float _reloadDuration; 
    private int shotsFired = 0;
    private bool _isReloading = false;
    private void Start()
    {
        _spriteTransform = transform.GetChild(0);

        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _animator = transform.GetChild(0).GetComponent<Animator>();

        _fireTime = Time.time;
    }
    private void FixedUpdate()
    {
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
        if (_isReloading) return;

         float _distanceFromPlayer = Vector2.Distance(_target.position,transform.position);
        if (_distanceFromPlayer < _lineOfste && _distanceFromPlayer > _shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _target.position, _runSpeed);
            _targetPosition = _target.position;
        }
        else if (_distanceFromPlayer <= _shootingRange && _fireTime < Time.time) 
        {
            Attack();
        }
        else
        {
            _animator.SetInteger("State", 0);
        }

     }

    private void Attack()
    {
        if(_isReloading) return;

        _animator.Play("Attack");
        Instantiate(_bullet, _bulletPoint.transform.position, Quaternion.identity);
        _fireTime = Time.time + _fireRate;

        shotsFired++;

        if (shotsFired  >= shotsBeforeReload)
        {
            StartReloading();
        } 


    }

    private void StartReloading()
    {
        _isReloading = true;
        _animator.SetTrigger("Reload");

        StartCoroutine(ReloadCoroutine());
    }
    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(_reloadDuration);

        _isReloading = false;
        shotsFired = 0;

        _animator.SetInteger("State", 0);
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

