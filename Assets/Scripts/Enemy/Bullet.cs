using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] Rigidbody2D _rb2D;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");
        Vector2 _moveDir = (_target.transform.position - transform.position).normalized * _speed;
        _rb2D.velocity = new Vector2 (_moveDir.x, _moveDir.y);
        Destroy(this.gameObject, 4);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("<<< DAMAGE >>>");
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage / 2);
            Destroy(this.gameObject);
        }
    }
}
