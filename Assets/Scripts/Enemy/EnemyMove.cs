using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
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
    [SerializeField] private bool _isMoving = false;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
            _spriteRenderer.flipX = false;
        }
        else if (transform.position.x < _targetPosition.x)
        {
            _spriteRenderer.flipX = true;
        } 
    }
    private void WalkOnPoints()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _speed);
            _targetPosition = _points[_currentPoint].position;
            _animator.SetInteger("State", 1);
            _animator.SetBool("_isAttack", false);
        }


        if (transform.position == _points[_currentPoint].position)
        {
            _isMoving = false;
            _animator.SetInteger("State", 0);
            _animator.SetBool("_isAttack", false);
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
            _animator.SetInteger("State", 2);
            _animator.SetBool("_isAttack", false);
        }
        else
        {
           _animator.Play("Damage");
           _animator.SetBool("_isAttack",true);
           //_animator.SetInteger("State", 0);
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

