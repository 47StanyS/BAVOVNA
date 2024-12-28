using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] Transform _targetFollow;
    [Space]
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [Space]
    [SerializeField] private float _speedAttak;
    [SerializeField] Transform _enemyTarget;
    private Vector3 _enemyPosition;

    //public bool _canFly = true;

    private Rigidbody2D _rb2D;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rb2D.velocity = Vector3.zero;
        
        if(_enemyTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemyTarget.position, _speedAttak);
            _enemyPosition = _enemyTarget.position;
        }
        else
        {
            FollowPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            _targetFollow = collision.transform;
        }
        if (collision.gameObject.CompareTag("Enemy") && !collision.isTrigger)
        {
            _enemyTarget = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
  
        if (collision.gameObject.CompareTag("Enemy") && !collision.isTrigger)
        {
            _enemyTarget = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("For a FRIEND!!!!");

           // collision.gameObject.GetComponent<Enemy>()._healthEnemy -= _damage;
        }
    }

    private void FollowPlayer()
    {
        if (_targetFollow)
        {
            Vector3 newPositionY = _targetFollow.position;
            newPositionY.y += 3;
            newPositionY.x += -1;
            transform.position = Vector3.Lerp(transform.position, newPositionY, _speed * Time.deltaTime);
        }
    }
}
