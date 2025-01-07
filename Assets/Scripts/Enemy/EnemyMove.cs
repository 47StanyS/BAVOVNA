using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector]
    public EnemyBrain enemyBrain;

    [Header("Transform Poins")]
    [SerializeField] private Transform[] _points;
    [Header("Transform Target")]

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
    public void WalkOnPoints()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _speed);
            _targetPosition = _points[_currentPoint].position;
            _animator.SetInteger("State", 1);
            //_animator.SetBool("_isAttack", false);
        }


        if (transform.position == _points[_currentPoint].position)
        {
            _isMoving = false;
            _animator.SetInteger("State", 0);
            //_animator.SetBool("_isAttack", false);
            StartCoroutine(canMoveTimer());
            _currentPoint++;

            if (_currentPoint == _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
     public void TargetFolow()
     {
         float _distanceFromPlayer = Vector2.Distance(enemyBrain.target.position,transform.position);
         if (_distanceFromPlayer < _lineOfste && _distanceFromPlayer > _shootingRange)
         {
             transform.position = Vector2.MoveTowards(this.transform.position, enemyBrain.target.position, _runSpeed);
             _targetPosition = enemyBrain.target.position;
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

    private IEnumerator canMoveTimer()
    {
        yield return new WaitForSeconds(_timer);
        

        _isMoving = true;
    }
    
}

