using UnityEngine;

public class BredgeTriger : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _rb2D.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, 1f);
        }
    }
}
